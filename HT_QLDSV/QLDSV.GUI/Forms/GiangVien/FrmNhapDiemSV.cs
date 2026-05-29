using QLDSV.BLL;
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
        // ─── Fields ───────────────────────────────────────────────────────────────
        private readonly KetQuaBLL _bll = new KetQuaBLL();
        private string maGV = "";
        private bool isInitializing = true;
        private bool _isEditMode = false;

        // ─── Constructor ──────────────────────────────────────────────────────────
        public FrmNhapDiemSV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // ─── Load ─────────────────────────────────────────────────────────────────
        private void FrmNhapDiemSV_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();

                maGV = _bll.GetMaGV(SessionHelper.MaTaiKhoan, SessionHelper.TenDangNhap);

                if (string.IsNullOrEmpty(maGV))
                {
                    MessageBox.Show("Không tìm thấy thông tin giảng viên cho tài khoản này!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khởi tạo màn hình nhập điểm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Load danh mục ────────────────────────────────────────────────────────
        private void LoadNamHoc()
        {
            try
            {
                DataTable dt = _bll.GetNamHoc();
                cboNamHoc.DataSource    = dt;
                cboNamHoc.ValueMember   = "MaNamHoc";
                cboNamHoc.DisplayMember = "TenNamHoc";
                if (cboNamHoc.Items.Count > 0) cboNamHoc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadNamHoc: " + ex.Message);
            }
        }

        private void LoadHocKy()
        {
            try
            {
                DataTable dt = _bll.GetLoaiHocKy();
                cboHocKy.DataSource    = dt;
                cboHocKy.ValueMember   = "MaLoaiHK";
                cboHocKy.DisplayMember = "TenLoaiHK";
                if (cboHocKy.Items.Count > 0) cboHocKy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadHocKy: " + ex.Message);
            }
        }

        // ─── Load lớp học phần ────────────────────────────────────────────────────
        private void PopulateClasses()
        {
            if (isInitializing) return;
            try
            {
                if (cboNamHoc.SelectedValue == null || cboHocKy.SelectedValue == null) return;

                string namHocVal = cboNamHoc.SelectedValue.ToString();
                string hocKyVal  = cboHocKy.SelectedValue.ToString();

                DataTable dt = _bll.GetLopHocPhan(namHocVal, hocKyVal, maGV);

                cboLopHocPhan.SelectedIndexChanged -= CboLopHocPhan_SelectedIndexChanged;
                cboLopHocPhan.DataSource    = dt.Rows.Count > 0 ? dt : null;
                cboLopHocPhan.ValueMember   = "MaLHP";
                cboLopHocPhan.DisplayMember = "DisplayText";
                if (dt.Rows.Count > 0)
                    cboLopHocPhan.SelectedIndex = 0;
                else
                    cboLopHocPhan.SelectedIndex = -1;
                cboLopHocPhan.SelectedIndexChanged += CboLopHocPhan_SelectedIndexChanged;

                LoadClassData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PopulateClasses: " + ex.Message);
            }
        }

        // ─── Load dữ liệu lớp học phần ───────────────────────────────────────────
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

                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                // Danh sách sinh viên bên trái
                DataTable dtSV = _bll.GetSinhVien(maLHP);
                lstSinhVien.SelectedIndexChanged -= LstSinhVien_SelectedIndexChanged;
                lstSinhVien.DataSource    = dtSV;
                lstSinhVien.ValueMember   = "MaSV";
                lstSinhVien.DisplayMember = "DisplayText";
                lstSinhVien.SelectedIndex = -1;
                lstSinhVien.SelectedIndexChanged += LstSinhVien_SelectedIndexChanged;

                // Bảng điểm tổng hợp bên dưới
                DataTable dtDiem = _bll.GetBangDiem(maLHP);
                if (dtDiem != null)
                {
                    // Chỉ hiển thị sinh viên đã có ít nhất 1 điểm
                    dtDiem.DefaultView.RowFilter =
                        "([Điểm CC] IS NOT NULL) OR ([Điểm KT1] IS NOT NULL) OR ([Điểm KT2] IS NOT NULL) OR ([Điểm CK] IS NOT NULL)";
                    dgvDiem.DataSource = dtDiem.DefaultView;
                    lblTongSV.Text = $"Tổng số sinh viên nhập điểm: {dtDiem.DefaultView.Count}";
                }
                else
                {
                    dgvDiem.DataSource = null;
                    lblTongSV.Text = "Tổng số sinh viên nhập điểm: 0";
                }

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lớp học phần: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Chọn sinh viên ───────────────────────────────────────────────────────
        private void LstSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSinhVien.SelectedValue == null || lstSinhVien.SelectedIndex < 0) return;

            try
            {
                string maSV  = lstSinhVien.SelectedValue.ToString();
                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                // Hiển thị thông tin sinh viên
                txtMaSV.Text = maSV;
                string[] parts = lstSinhVien.Text.Split(new[] { " - " }, StringSplitOptions.None);
                txtHoTen.Text = parts.Length > 1 ? parts[1] : "";

                // Lấy điểm hiện có từ BLL
                DataTable dt = _bll.GetDiemSinhVien(maSV, maLHP);

                decimal? cc = null, kt1 = null, kt2 = null, ck = null;
                foreach (DataRow row in dt.Rows)
                {
                    string loai = row["MaLoaiDiem"].ToString().Trim();
                    decimal val = Convert.ToDecimal(row["Diem"]);
                    if      (loai == "CC")  cc  = val;
                    else if (loai == "KT1") kt1 = val;
                    else if (loai == "KT2") kt2 = val;
                    else if (loai == "CK")  ck  = val;
                }

                // CC, KT1, KT2: chỉ nhập 1 lần
                ConfigureGradeInput(txtDiemCC,  cc);
                ConfigureGradeInput(txtDiemKT1, kt1);
                ConfigureGradeInput(txtDiemKT2, kt2);

                // CK: luôn editable
                txtDiemCK.Text      = ck.HasValue ? ck.Value.ToString("0.#") : "";
                txtDiemCK.ReadOnly  = false;
                txtDiemCK.BackColor = Color.White;

                // Cập nhật trạng thái btnNhap / btnSua theo điểm thành phần
                bool daDu = cc.HasValue && kt1.HasValue && kt2.HasValue;
                btnNhap.Enabled = !daDu;
                btnSua.Enabled  = daDu;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin điểm sinh viên: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGradeInput(Guna.UI2.WinForms.Guna2TextBox txt, decimal? gradeValue)
        {
            if (gradeValue.HasValue)
            {
                txt.Text      = gradeValue.Value.ToString("0.#");
                txt.ReadOnly  = true;
                txt.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                txt.Text      = "";
                txt.ReadOnly  = false;
                txt.BackColor = Color.White;
            }
        }

        // ─── Button Nhập ──────────────────────────────────────────────────────────
        private void BtnNhap_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrEmpty(maSV) || cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên từ danh sách để nhập điểm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = cboLopHocPhan.SelectedValue.ToString();

            // Kiểm tra đã có đủ điểm thành phần chưa (qua BLL)
            if (_bll.DaDuDiemThanhPhan(maSV, maLHP))
            {
                MessageBox.Show("Sinh viên đã có điểm thành phần.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Bắt buộc nhập đủ 3 điểm thành phần
            if (!txtDiemCC.ReadOnly  && string.IsNullOrEmpty(txtDiemCC.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm chuyên cần!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemCC.Focus(); return; }
            if (!txtDiemKT1.ReadOnly && string.IsNullOrEmpty(txtDiemKT1.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT1.Focus(); return; }
            if (!txtDiemKT2.ReadOnly && string.IsNullOrEmpty(txtDiemKT2.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 2!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT2.Focus(); return; }

            // Validate giá trị
            decimal cc = 0, kt1 = 0, kt2 = 0, ck = 0;
            bool hasCC, hasKT1, hasKT2, hasCK;
            if (!ValidateGrade(txtDiemCC,  "chuyên cần",  out cc,  out hasCC))  return;
            if (!ValidateGrade(txtDiemKT1, "kiểm tra 1",  out kt1, out hasKT1)) return;
            if (!ValidateGrade(txtDiemKT2, "kiểm tra 2",  out kt2, out hasKT2)) return;
            if (!ValidateGrade(txtDiemCK,  "thi cuối kỳ", out ck,  out hasCK))  return;

            try
            {
                // Lưu qua BLL
                if (!txtDiemCC.ReadOnly  && hasCC)  _bll.LuuDiem(maSV, maLHP, "CC",  cc);
                if (!txtDiemKT1.ReadOnly && hasKT1) _bll.LuuDiem(maSV, maLHP, "KT1", kt1);
                if (!txtDiemKT2.ReadOnly && hasKT2) _bll.LuuDiem(maSV, maLHP, "KT2", kt2);
                if (hasCK)                          _bll.LuuDiem(maSV, maLHP, "CK",  ck);

                MessageBox.Show("Thêm điểm sinh viên thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int idx = lstSinhVien.SelectedIndex;
                LoadClassData();
                if (idx >= 0 && idx < lstSinhVien.Items.Count)
                    lstSinhVien.SelectedIndex = idx;

                btnNhap.Enabled = false;
                btnSua.Enabled  = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu điểm sinh viên: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Button Sửa ───────────────────────────────────────────────────────────
        // ─── Button Sửa ───────────────────────────────────────────────────────────
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

            // 2. Chỉ cho sửa khi đã có đủ điểm thành phần
            bool daDuDiemThanhPhan = _bll.DaDuDiemThanhPhan(maSV, maLHP);

            if (!daDuDiemThanhPhan)
            {
                MessageBox.Show(
                    "Sinh viên chưa có đủ điểm thành phần để sửa điểm thi!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 3. Bắt buộc nhập điểm CK
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

            // 4. Validate điểm CK
            decimal diemCK;
            bool hasCK;

            if (!ValidateGrade(
                    txtDiemCK,
                    "thi cuối kỳ",
                    out diemCK,
                    out hasCK))
            {
                return;
            }

            try
            {
                // 5. Chỉ update điểm CK
                _bll.LuuDiem(maSV, maLHP, "CK", diemCK);

                MessageBox.Show(
                    "Cập nhật điểm thi cuối kỳ thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 6. Reload dữ liệu
                int selectedIndex = lstSinhVien.SelectedIndex;

                LoadClassData();

                if (selectedIndex >= 0 &&
                    selectedIndex < lstSinhVien.Items.Count)
                {
                    lstSinhVien.SelectedIndex = selectedIndex;
                }

                // 7. Giữ khóa điểm thành phần
                txtDiemCC.ReadOnly = true;
                txtDiemKT1.ReadOnly = true;
                txtDiemKT2.ReadOnly = true;

                txtDiemCC.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT1.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT2.BackColor = Color.FromArgb(230, 230, 230);

                // 8. CK vẫn cho sửa tiếp
                txtDiemCK.ReadOnly = false;
                txtDiemCK.BackColor = Color.White;

                // 9. Trạng thái button
                btnNhap.Enabled = false;
                btnSua.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật điểm thi cuối kỳ: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ─── Button Hủy ───────────────────────────────────────────────────────────
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            txtDiemCC.Text = "";
            txtDiemKT1.Text = "";
            txtDiemKT2.Text = "";
            txtDiemCK.Text = "";


            btnNhap.Enabled = true;
            btnSua.Enabled = false;
            btnReset.Enabled = true;
            btnHuy.Enabled = true;

            _isEditMode = false;

            lstSinhVien.Focus();
        }

        // ─── Button Reset ─────────────────────────────────────────────────────────
        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Chỉ xóa ô điểm đang editable, giữ nguyên MaSV và HoTen
            if (!txtDiemCC.ReadOnly)  txtDiemCC.Text  = "";
            if (!txtDiemKT1.ReadOnly) txtDiemKT1.Text = "";
            if (!txtDiemKT2.ReadOnly) txtDiemKT2.Text = "";
            if (!txtDiemCK.ReadOnly)  txtDiemCK.Text  = "";
        }

        // ─── Quản lý trạng thái form ──────────────────────────────────────────────
        private void SetFormState(bool isEditMode)
        {
            _isEditMode = isEditMode;
            if (isEditMode)
            {
                // Chế độ Sửa: chỉ CK được nhập
                btnNhap.Enabled  = false;
                btnSua.Enabled   = false;
                btnReset.Enabled = false;
                btnHuy.Enabled   = true;

                txtDiemCC.ReadOnly  = true;  txtDiemCC.BackColor  = Color.FromArgb(230, 230, 230);
                txtDiemKT1.ReadOnly = true;  txtDiemKT1.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemKT2.ReadOnly = true;  txtDiemKT2.BackColor = Color.FromArgb(230, 230, 230);
                txtDiemCK.ReadOnly  = false; txtDiemCK.BackColor  = Color.White;
            }
            else
            {
                // Chế độ Rảnh: btnNhap/btnSua sẽ được cập nhật khi chọn SV
                btnNhap.Enabled  = true;
                btnSua.Enabled   = false;
                btnReset.Enabled = true;
                btnHuy.Enabled   = true;
            }
        }

        // ─── Helpers ──────────────────────────────────────────────────────────────
        private void ClearInputs()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            txtDiemCC.Text  = ""; txtDiemCC.ReadOnly  = false; txtDiemCC.BackColor  = Color.White;
            txtDiemKT1.Text = ""; txtDiemKT1.ReadOnly = false; txtDiemKT1.BackColor = Color.White;
            txtDiemKT2.Text = ""; txtDiemKT2.ReadOnly = false; txtDiemKT2.BackColor = Color.White;
            txtDiemCK.Text  = ""; txtDiemCK.ReadOnly  = false; txtDiemCK.BackColor  = Color.White;
        }

        private bool ValidateGrade(Guna.UI2.WinForms.Guna2TextBox txt, string label,
            out decimal value, out bool hasValue)
        {
            value = 0; hasValue = false;
            string text = txt.Text.Trim();
            if (string.IsNullOrEmpty(text)) return true;

            text = text.Replace(',', '.');
            if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                MessageBox.Show($"Điểm {label} không hợp lệ! Vui lòng nhập số thực.",
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (value < 0 || value > 10)
            {
                MessageBox.Show($"Điểm {label} phải nằm trong khoảng từ 0 đến 10.",
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            hasValue = true;
            return true;
        }

        private void CboNamHoc_SelectedIndexChanged(object sender, EventArgs e)      => PopulateClasses();
        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)       => PopulateClasses();
        private void CboLopHocPhan_SelectedIndexChanged(object sender, EventArgs e)  => LoadClassData();
    }
}
