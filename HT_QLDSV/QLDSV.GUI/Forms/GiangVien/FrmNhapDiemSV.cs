using QLDSV.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.GiangVien
{
    public partial class FrmNhapDiemSV : Form
    {
        private string maGV = "";
        private bool isInitializing = true;
        private bool _isEditMode = false;



        public FrmNhapDiemSV()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);

            // Bind Form events
            this.Load += FrmNhapDiemSV_Load;

            // Bind control events
            this.cboNamHoc.SelectedIndexChanged += CboNamHoc_SelectedIndexChanged;
            this.cboHocKy.SelectedIndexChanged += CboHocKy_SelectedIndexChanged;
            this.cboLopHocPhan.SelectedIndexChanged += CboLopHocPhan_SelectedIndexChanged;
            this.lstSinhVien.SelectedIndexChanged += LstSinhVien_SelectedIndexChanged;

            this.btnNhap.Click += btnNhap_Click;
            this.btnSua.Click   += BtnSua_Click;

            this.btnReset.Click += BtnReset_Click;
        }

        private void FrmNhapDiemSV_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();

                RetrieveLecturerId();

                if (string.IsNullOrEmpty(maGV))
                {
                    MessageBox.Show("Không tìm thấy thông tin giảng viên cho tài khoản này!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                isInitializing = true;

                LoadNamHoc();
                LoadHocKy();

                isInitializing = false;

                PopulateClasses();
                SetFormState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo màn hình nhập điểm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RetrieveLecturerId()
        {
            try
            {
                // Retrieve MaGV linked to the login account
                string query = $"SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '{SessionHelper.MaTaiKhoan}'";
                maGV = FunctionQa.getfieldvalue(query);

                // Fallback using username if MaTaiKhoan query returned empty
                if (string.IsNullOrEmpty(maGV))
                {
                    query = $"SELECT MaGV FROM GiangVien WHERE MaGV = '{SessionHelper.TenDangNhap}'";
                    maGV = FunctionQa.getfieldvalue(query);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error retrieving Lecturer ID: " + ex.Message);
            }
        }

        private void LoadNamHoc()
        {
            try
            {
                string sql = "SELECT MaNamHoc, TenNamHoc FROM NamHoc";
                FunctionQa.fillcombo(sql, cboNamHoc, "MaNamHoc", "TenNamHoc");
                if (cboNamHoc.Items.Count > 0)
                {
                    cboNamHoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading School Years: " + ex.Message);
            }
        }

        private void LoadHocKy()
        {
            try
            {
                string sql = "SELECT MaLoaiHK, TenLoaiHK FROM LoaiHocKy";
                FunctionQa.fillcombo(sql, cboHocKy, "MaLoaiHK", "TenLoaiHK");
                if (cboHocKy.Items.Count > 0)
                {
                    cboHocKy.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading Semesters: " + ex.Message);
            }
        }

        private void PopulateClasses()
        {
            if (isInitializing) return;

            try
            {
                if (cboNamHoc.SelectedValue == null || cboHocKy.SelectedValue == null) return;

                string namHocVal = cboNamHoc.SelectedValue.ToString();
                string hocKyVal = cboHocKy.SelectedValue.ToString();

                // Load sections matching year, semester, and taught by this lecturer
                string sql = "SELECT lhp.MaLHP, (lhp.MaLHP + ' - ' + lhp.TenLopHocPhan) AS DisplayText " +
                             "FROM LopHocPhan lhp " +
                             "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                             $"WHERE hknh.MaNamHoc = '{namHocVal}' " +
                             $"  AND hknh.MaLoaiHK = '{hocKyVal}' " +
                             $"  AND lhp.MaGV = '{maGV}'";

                cboLopHocPhan.SelectedIndexChanged -= CboLopHocPhan_SelectedIndexChanged;
                FunctionQa.fillcombo(sql, cboLopHocPhan, "MaLHP", "DisplayText");
                
                if (cboLopHocPhan.Items.Count > 0)
                {
                    cboLopHocPhan.SelectedIndex = 0;
                }
                else
                {
                    cboLopHocPhan.SelectedIndex = -1;
                    cboLopHocPhan.DataSource = null;
                }
                cboLopHocPhan.SelectedIndexChanged += CboLopHocPhan_SelectedIndexChanged;

                // Load class data (even if empty)
                LoadClassData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading classes: " + ex.Message);
            }
        }

        private void LoadClassData()
        {
            try
            {
                if (cboLopHocPhan.SelectedValue == null)
                {
                    lstSinhVien.DataSource = null;
                    lstSinhVien.Items.Clear();
                    dgvDiem.DataSource = null;
                    lblTongSV.Text = "Tổng số sinh viên nhập điểm: 0";
                    ClearInputs();
                    return;
                }

                string lopHPVal = cboLopHocPhan.SelectedValue.ToString();

                // 1. Load left-side student list
                string sqlStudents = "SELECT dklh.MaSV, (dklh.MaSV + ' - ' + sv.HoTen) AS DisplayText " +
                                     "FROM DangKyLopHoc dklh " +
                                     "INNER JOIN SinhVien sv ON dklh.MaSV = sv.MaSV " +
                                     $"WHERE dklh.MaLHP = '{lopHPVal}'";

                DataTable dtStudents = FunctionQa.getdatatotable(sqlStudents);
                lstSinhVien.SelectedIndexChanged -= LstSinhVien_SelectedIndexChanged;
                lstSinhVien.DataSource = dtStudents;
                lstSinhVien.ValueMember = "MaSV";
                lstSinhVien.DisplayMember = "DisplayText";
                lstSinhVien.SelectedIndex = -1;
                lstSinhVien.SelectedIndexChanged += LstSinhVien_SelectedIndexChanged;

                // 2. Load bottom pivoted grades GridView
                string sqlGrades = "SELECT " +
                                   "  sv.MaSV AS [Mã SV], " +
                                   "  sv.HoTen AS [Họ Tên], " +
                                   "  MAX(CASE WHEN kq.MaLoaiDiem = 'CC' THEN kq.Diem END) AS [Điểm CC], " +
                                   "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) AS [Điểm KT1], " +
                                   "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) AS [Điểm KT2], " +
                                   "  MAX(CASE WHEN kq.MaLoaiDiem = 'CK' THEN kq.Diem END) AS [Điểm CK] " +
                                   "FROM DangKyLopHoc dklh " +
                                   "INNER JOIN SinhVien sv ON dklh.MaSV = sv.MaSV " +
                                   "LEFT JOIN KetQua kq ON dklh.MaSV = kq.MaSV AND dklh.MaLHP = kq.MaLHP " +
                                   $"WHERE dklh.MaLHP = '{lopHPVal}' " +
                                   "GROUP BY sv.MaSV, sv.HoTen";

                DataTable dtGrades = FunctionQa.getdatatotable(sqlGrades);
                dgvDiem.DataSource = dtGrades;

                lblTongSV.Text = $"Tổng số sinh viên nhập điểm: {dtGrades.Rows.Count}";

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lớp học phần: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LstSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSinhVien.SelectedValue == null || lstSinhVien.SelectedIndex < 0) return;

            try
            {
                string selectedMaSV = lstSinhVien.SelectedValue.ToString();
                string lopHPVal = cboLopHocPhan.SelectedValue.ToString();

                // Display selected student info
                txtMaSV.Text = selectedMaSV;
                string displayText = lstSinhVien.Text;
                string[] parts = displayText.Split(new string[] { " - " }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    txtHoTen.Text = parts[1];
                }
                else
                {
                    txtHoTen.Text = "";
                }

                // Query database for existing grades
                string sql = "SELECT MaLoaiDiem, Diem FROM KetQua " +
                             $"WHERE MaSV = '{selectedMaSV}' AND MaLHP = '{lopHPVal}'";
                
                DataTable dt = FunctionQa.getdatatotable(sql);

                decimal? cc = null;
                decimal? kt1 = null;
                decimal? kt2 = null;
                decimal? ck = null;

                foreach (DataRow row in dt.Rows)
                {
                    string type = row["MaLoaiDiem"].ToString().Trim();
                    decimal val = Convert.ToDecimal(row["Diem"]);
                    if (type == "CC") cc = val;
                    else if (type == "KT1") kt1 = val;
                    else if (type == "KT2") kt2 = val;
                    else if (type == "CK") ck = val;
                }

                // Process Grades Lock Logic: CC, KT1, KT2 can only be entered once (cannot be edited)
                ConfigureGradeInput(txtDiemCC, cc);
                ConfigureGradeInput(txtDiemKT1, kt1);
                ConfigureGradeInput(txtDiemKT2, kt2);

                // Final Exam: Can be entered later, always editable
                if (ck.HasValue)
                {
                    txtDiemCK.Text = ck.Value.ToString("0.#");
                }
                else
                {
                    txtDiemCK.Text = "";
                }
                txtDiemCK.ReadOnly = false;
                txtDiemCK.BackColor = Color.White;

                // Cập nhật trạng thái btnThem theo điểm thành phần của SV vừa chọn:
                // Nếu đã có đủ CC + KT1 + KT2 thì disable Thêm, enable Sửa
                // Nếu chưa đủ thì enable Thêm
                bool daCoCC  = cc.HasValue;
                bool daCoKT1 = kt1.HasValue;
                bool daCoKT2 = kt2.HasValue;
                bool daDuDiemThanhPhan = daCoCC && daCoKT1 && daCoKT2;

                btnNhap.Enabled = !daDuDiemThanhPhan;
                btnSua.Enabled  = daDuDiemThanhPhan;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin điểm sinh viên: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGradeInput(Guna.UI2.WinForms.Guna2TextBox txt, decimal? gradeValue)
        {
            if (gradeValue.HasValue)
            {
                txt.Text = gradeValue.Value.ToString("0.#");
                txt.ReadOnly = true;
                txt.BackColor = Color.FromArgb(230, 230, 230); // Disabled/Read-only xám
            }
            else
            {
                txt.Text = "";
                txt.ReadOnly = false;
                txt.BackColor = Color.White; // Hoạt động trắng
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đã chọn sinh viên chưa
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV) || cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên từ danh sách để nhập điểm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lopHPVal = cboLopHocPhan.SelectedValue.ToString();

            // 2. Kiểm tra sinh viên đã có đủ điểm thành phần (CC + KT1 + KT2) chưa
            //    Nếu đã đủ thì không cho thêm mới
            bool ccDaCo  = txtDiemCC.ReadOnly  && !string.IsNullOrEmpty(txtDiemCC.Text.Trim());
            bool kt1DaCo = txtDiemKT1.ReadOnly && !string.IsNullOrEmpty(txtDiemKT1.Text.Trim());
            bool kt2DaCo = txtDiemKT2.ReadOnly && !string.IsNullOrEmpty(txtDiemKT2.Text.Trim());

            if (ccDaCo && kt1DaCo && kt2DaCo)
            {
                MessageBox.Show("Sinh viên đã có điểm thành phần.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Bắt buộc nhập đủ 3 điểm thành phần (CC, KT1, KT2) khi thêm mới
            if (string.IsNullOrEmpty(txtDiemCC.Text.Trim()) && !txtDiemCC.ReadOnly)
            {
                MessageBox.Show("Vui lòng nhập điểm chuyên cần!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemCC.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDiemKT1.Text.Trim()) && !txtDiemKT1.ReadOnly)
            {
                MessageBox.Show("Vui lòng nhập điểm kiểm tra 1!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemKT1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDiemKT2.Text.Trim()) && !txtDiemKT2.ReadOnly)
            {
                MessageBox.Show("Vui lòng nhập điểm kiểm tra 2!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemKT2.Focus();
                return;
            }

            // 4. Validate giá trị các điểm (phải là số hợp lệ 0–10)
            decimal cc = 0, kt1 = 0, kt2 = 0, ck = 0;
            bool hasCC = false, hasKT1 = false, hasKT2 = false, hasCK = false;

            if (!ValidateGrade(txtDiemCC,  "chuyên cần",   out cc,  out hasCC))  return;
            if (!ValidateGrade(txtDiemKT1, "kiểm tra 1",   out kt1, out hasKT1)) return;
            if (!ValidateGrade(txtDiemKT2, "kiểm tra 2",   out kt2, out hasKT2)) return;
            if (!ValidateGrade(txtDiemCK,  "thi cuối kỳ",  out ck,  out hasCK))  return;

            try
            {
                // 5. Lưu điểm thành phần (chỉ lưu ô đang editable và có giá trị)
                SaveGradeIfApplicable(maSV, lopHPVal, "CC",  txtDiemCC,  cc,  hasCC);
                SaveGradeIfApplicable(maSV, lopHPVal, "KT1", txtDiemKT1, kt1, hasKT1);
                SaveGradeIfApplicable(maSV, lopHPVal, "KT2", txtDiemKT2, kt2, hasKT2);

                // 6. Lưu điểm thi nếu có nhập (không bắt buộc)
                if (hasCK)
                {
                    SaveOrUpdateGrade(maSV, lopHPVal, "CK", ck);
                }

                MessageBox.Show("Thêm điểm sinh viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. Reload danh sách và giữ nguyên selection sinh viên
                int selectedIndex = lstSinhVien.SelectedIndex;
                LoadClassData();
                if (selectedIndex >= 0 && selectedIndex < lstSinhVien.Items.Count)
                {
                    lstSinhVien.SelectedIndex = selectedIndex;
                }

                // 8. Sau khi thêm thành công: disable Thêm, enable Sửa
                btnNhap.Enabled = false;
                btnSua.Enabled  = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu điểm sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateGrade(Guna.UI2.WinForms.Guna2TextBox txt, string label, out decimal value, out bool hasValue)
        {
            value = 0;
            hasValue = false;
            string text = txt.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                return true; // Grades can be left empty initially
            }

            // Replace comma with dot to ensure correct decimal parsing in different locales
            text = text.Replace(',', '.');

            if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                MessageBox.Show($"Điểm {label} không hợp lệ! Vui lòng nhập số thực.", "Lỗi dữ liệu", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (value < 0 || value > 10)
            {
                MessageBox.Show($"Điểm {label} phải nằm trong khoảng từ 0 đến 10.", "Lỗi dữ liệu", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            hasValue = true;
            return true;
        }

        private void SaveGradeIfApplicable(string maSV, string maLHP, string loaiDiem, Guna.UI2.WinForms.Guna2TextBox txt, decimal value, bool hasValue)
        {
            // Only save if textbox was editable (not read-only) and a grade was entered
            if (!txt.ReadOnly && hasValue)
            {
                SaveOrUpdateGrade(maSV, maLHP, loaiDiem, value);
            }
        }

        private void SaveOrUpdateGrade(string maSV, string maLHP, string loaiDiem, decimal value)
        {
            string valueStr = value.ToString(CultureInfo.InvariantCulture);
            string checkQuery = $"SELECT 1 FROM KetQua WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{loaiDiem}'";
            
            if (FunctionQa.checkkey(checkQuery))
            {
                // Update
                string sql = $"UPDATE KetQua SET Diem = {valueStr} " +
                             $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{loaiDiem}'";
                FunctionQa.runsql(sql);
            }
            else
            {
                // Insert
                string sql = $"INSERT INTO KetQua (MaSV, MaLHP, MaLoaiDiem, Diem) " +
                             $"VALUES ('{maSV}', '{maLHP}', '{loaiDiem}', {valueStr})";
                Connection.RunSql(sql);
            }
        }

        private void SetFormState(bool isEditMode)
        {
            _isEditMode = isEditMode;
            if (isEditMode)
            {
                // Chế độ Sửa
                btnNhap.Enabled  = false;
                btnSua.Enabled   = false;
                btnReset.Enabled = false;
                btnHuy.Enabled   = true;

                // CC, KT1, KT2 readonly xám
                txtDiemCC.ReadOnly  = true;
                txtDiemCC.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT1.ReadOnly  = true;
                txtDiemKT1.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT2.ReadOnly  = true;
                txtDiemKT2.BackColor = Color.FromArgb(230, 230, 230);

                // CK editable trắng
                txtDiemCK.ReadOnly  = false;
                txtDiemCK.BackColor = Color.White;
            }
            else
            {
                // Chế độ Rảnh — reset về mặc định
                // btnNhap/btnSua sẽ được cập nhật lại bởi LstSinhVien_SelectedIndexChanged
                btnNhap.Enabled  = true;
                btnSua.Enabled   = false;  // chưa chọn SV, Sửa chưa có nghĩa
                btnReset.Enabled = true;
                btnHuy.Enabled   = true;
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đã chọn sinh viên chưa
            string maSV = txtMaSV.Text.Trim();

if (string.IsNullOrEmpty(maSV) || cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sinh viên cần sửa điểm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string maLHP = cboLopHocPhan.SelectedValue.ToString();

            // 2. Kiểm tra sinh viên đã có điểm thành phần chưa
            bool daCoCC = txtDiemCC.ReadOnly && !string.IsNullOrEmpty(txtDiemCC.Text.Trim());
            bool daCoKT1 = txtDiemKT1.ReadOnly && !string.IsNullOrEmpty(txtDiemKT1.Text.Trim());
            bool daCoKT2 = txtDiemKT2.ReadOnly && !string.IsNullOrEmpty(txtDiemKT2.Text.Trim());

            if (!(daCoCC && daCoKT1 && daCoKT2))
            {
                MessageBox.Show(
                    "Sinh viên chưa có đủ điểm thành phần để sửa điểm thi!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 3. Bắt buộc nhập điểm thi
            if (string.IsNullOrEmpty(txtDiemCK.Text.Trim()))
            {
                MessageBox.Show(
                    "Vui lòng nhập điểm thi cuối kỳ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtDiemCK.Focus();
                return;
            }

            // 4. Validate điểm thi
            decimal ck;
            bool hasCK;

            if (!ValidateGrade(txtDiemCK, "thi cuối kỳ", out ck, out hasCK))
            {
                return;
            }

            try
            {
                // 5. Chỉ lưu/update điểm CK
                SaveOrUpdateGrade(maSV, maLHP, "CK", ck);

                MessageBox.Show(
                    "Cập nhật điểm thi thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 6. Reload dữ liệu
                int selectedIndex = lstSinhVien.SelectedIndex;

                LoadClassData();

                if (selectedIndex >= 0 && selectedIndex < lstSinhVien.Items.Count)
                {
                    lstSinhVien.SelectedIndex = selectedIndex;
                }

                // 7. Giữ trạng thái khóa các điểm thành phần
                txtDiemCC.ReadOnly = true;
                txtDiemKT1.ReadOnly = true;
                txtDiemKT2.ReadOnly = true;

                txtDiemCC.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT1.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT2.BackColor = Color.FromArgb(230, 230, 230);

                // CK vẫn editable
                txtDiemCK.ReadOnly = false;
                txtDiemCK.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật điểm thi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            txtDiemCC.Text = "";
            txtDiemKT1.Text = "";
            txtDiemKT2.Text = "";
            txtDiemCK.Text = "";

            btnNhap.Enabled = true;
            btnSua.Enabled = false;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset chỉ các ô điểm, giữ nguyên MaSV và HoTen
            // Không reset ô đang readonly (điểm đã lưu vào DB)
            if (!txtDiemCC.ReadOnly)  { txtDiemCC.Text  = ""; }
            if (!txtDiemKT1.ReadOnly) { txtDiemKT1.Text = ""; }
            if (!txtDiemKT2.ReadOnly) { txtDiemKT2.Text = ""; }
            if (!txtDiemCK.ReadOnly)  { txtDiemCK.Text  = ""; }
            // txtMaSV và txtHoTen giữ nguyên
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            if (lstSinhVien.SelectedIndex >= 0)
            {
                lstSinhVien.SelectedIndex = -1;
            }
        }

        private void ClearInputs()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            txtDiemCC.Text = "";
            txtDiemCC.ReadOnly = false;
            txtDiemCC.BackColor = Color.White;

            txtDiemKT1.Text = "";
            txtDiemKT1.ReadOnly = false;
            txtDiemKT1.BackColor = Color.White;

            txtDiemKT2.Text = "";
            txtDiemKT2.ReadOnly = false;
            txtDiemKT2.BackColor = Color.White;

            txtDiemCK.Text = "";
            txtDiemCK.ReadOnly = false;
            txtDiemCK.BackColor = Color.White;
        }

        private void CboNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateClasses();
        }

        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateClasses();
        }

        private void CboLopHocPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClassData();
        }

    }
}
