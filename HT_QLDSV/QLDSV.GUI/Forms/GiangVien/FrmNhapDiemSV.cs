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

        private readonly KetQuaBLL _bll = new KetQuaBLL();
        private string maGV = "";
        private bool isInitializing = true;
        private decimal? _gocCC, _gocKT1, _gocKT2, _gocCK;
        private string _activeMaSV = "";
        private bool _suppressGridSelectionEvents;
        private const string ColMaSV = "Mã SV";
        private static readonly Color GridHeaderBackColor = Color.FromArgb(56, 103, 158);
        private static readonly Color RowHighlightColor = Color.FromArgb(235, 228, 248);
        private static readonly Color RowNormalColorEven = Color.White;
        private static readonly Color RowNormalColorOdd = Color.FromArgb(250, 250, 252);


        public FrmNhapDiemSV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            ApplyGridHeaderStyle();
            dgvDiem.DataBindingComplete += DgvDiem_DataBindingComplete;
            dgvDiem.CellFormatting += DgvDiem_CellFormatting;
            dgvDiem.CellClick += DgvDiem_CellClick;
        }

        private void ApplyGridHeaderStyle()
        {
            dgvDiem.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBackColor;
            dgvDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeaderBackColor;
            dgvDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvDiem.ThemeStyle.HeaderStyle.BackColor = GridHeaderBackColor;
            dgvDiem.ThemeStyle.HeaderStyle.ForeColor = Color.White;
        }


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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo màn hình nhập điểm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadNamHoc()
        {
            try
            {
                DataTable dt = _bll.GetNamHocByGiangVien(maGV);
                cboNamHoc.DataSource = dt;
                cboNamHoc.ValueMember = "MaNamHoc";
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
                cboHocKy.DataSource = dt;
                cboHocKy.ValueMember = "MaLoaiHK";
                cboHocKy.DisplayMember = "TenLoaiHK";
                if (cboHocKy.Items.Count > 0) cboHocKy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadHocKy: " + ex.Message);
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

                DataTable dt = _bll.GetLopHocPhan(namHocVal, hocKyVal, maGV);

                cboLopHocPhan.SelectedIndexChanged -= CboLopHocPhan_SelectedIndexChanged;
                cboLopHocPhan.DataSource = dt.Rows.Count > 0 ? dt : null;
                cboLopHocPhan.ValueMember = "MaLHP";
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


        private void LoadClassData(string selectMaSV = null)
        {
            try
            {
                if (cboLopHocPhan.SelectedValue == null)
                {
                    lstSinhVien.DataSource = null;
                    lstSinhVien.Items.Clear();
                    dgvDiem.DataSource = null;
                    _activeMaSV = "";
                    lblTongSV.Text = "Tổng số sinh viên nhập điểm: 0";
                    ClearInputs();
                    return;
                }

                _activeMaSV = string.IsNullOrEmpty(selectMaSV) ? "" : selectMaSV;

                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                DataTable dtSV = _bll.GetSinhVien(maLHP);
                lstSinhVien.SelectedIndexChanged -= LstSinhVien_SelectedIndexChanged;
                lstSinhVien.DataSource = dtSV;
                lstSinhVien.ValueMember = "MaSV";
                lstSinhVien.DisplayMember = "DisplayText";



                lstSinhVien.SelectedIndexChanged += LstSinhVien_SelectedIndexChanged;

                BindBangDiemGrid(maLHP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lớp học phần: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindBangDiemGrid(string maLHP)
        {
            DataTable dtDiem = _bll.GetBangDiem(maLHP);
            if (dtDiem != null)
            {
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
        }

        private void RefreshAfterSave(string maSV) => LoadClassData(maSV);

        private void DgvDiem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            RefreshGridHighlight();
            if (!string.IsNullOrEmpty(_activeMaSV))
                SelectGridRowByMaSV(_activeMaSV);
        }

        private void DgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _suppressGridSelectionEvents)
                return;

            DataGridViewRow row = dgvDiem.Rows[e.RowIndex];
            if (row.IsNewRow || !dgvDiem.Columns.Contains(ColMaSV))
                return;

            string ma = row.Cells[ColMaSV].Value?.ToString();
            if (!string.IsNullOrEmpty(ma))
                SetActiveRow(ma);
        }

        private void DgvDiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dgvDiem.Columns.Contains(ColMaSV))
                return;

            DataGridViewRow row = dgvDiem.Rows[e.RowIndex];
            if (row.IsNewRow)
                return;

            string ma = row.Cells[ColMaSV].Value?.ToString();
            bool isActive = !string.IsNullOrEmpty(_activeMaSV)
                && string.Equals(ma, _activeMaSV, StringComparison.OrdinalIgnoreCase);

            Color bg = isActive ? RowHighlightColor : GetRowNormalBackColor(e.RowIndex);
            e.CellStyle.BackColor = bg;
            e.CellStyle.SelectionBackColor = bg;
            e.CellStyle.SelectionForeColor = Color.Black;
        }

        private static Color GetRowNormalBackColor(int rowIndex)
        {
            return rowIndex % 2 == 0 ? RowNormalColorEven : RowNormalColorOdd;
        }

        /// <summary>Chỉ một dòng active: cập nhật _activeMaSV và vẽ lại toàn bộ grid.</summary>
        private void SetActiveRow(string maSV)
        {
            string ma = maSV?.Trim() ?? "";
            if (string.Equals(ma, _activeMaSV, StringComparison.OrdinalIgnoreCase))
            {
                RefreshGridHighlight();
                return;
            }

            _activeMaSV = ma;
            RefreshGridHighlight();
        }

        private void RefreshGridHighlight() => dgvDiem.Invalidate();

        private void SelectGridRowByMaSV(string maSV)
        {
            if (string.IsNullOrEmpty(maSV) || !dgvDiem.Columns.Contains(ColMaSV))
                return;

            DataGridViewRow target = null;
            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                if (row.IsNewRow) continue;
                if (string.Equals(row.Cells[ColMaSV].Value?.ToString(), maSV, StringComparison.OrdinalIgnoreCase))
                {
                    target = row;
                    break;
                }
            }

            if (target == null)
                return;

            try
            {
                _suppressGridSelectionEvents = true;
                dgvDiem.ClearSelection();
                target.Selected = true;
                dgvDiem.CurrentCell = target.Cells[dgvDiem.Columns[ColMaSV].Index];
            }
            finally
            {
                _suppressGridSelectionEvents = false;
            }

            RefreshGridHighlight();
        }

        private void LstSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSinhVien.SelectedValue == null || lstSinhVien.SelectedIndex < 0) return;

            try
            {
                string maSV = lstSinhVien.SelectedValue.ToString();
                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                // Hiển thị thông tin sinh viên
                txtMaSV.Text = maSV;
                string[] parts = lstSinhVien.Text.Split(new[] { " - " }, StringSplitOptions.None);
                txtHoTen.Text = parts.Length > 1 ? parts[1] : "";


                DataTable dt = _bll.GetDiemSinhVien(maSV, maLHP);

                decimal? cc = null, kt1 = null, kt2 = null, ck = null;
                foreach (DataRow row in dt.Rows)
                {
                    string loai = row["MaLoaiDiem"].ToString().Trim();
                    decimal val = Convert.ToDecimal(row["Diem"]);
                    if (loai == "CC") cc = val;
                    else if (loai == "KT1") kt1 = val;
                    else if (loai == "KT2") kt2 = val;
                    else if (loai == "CK") ck = val;
                }

                _gocCC  = cc;
                _gocKT1 = kt1;
                _gocKT2 = kt2;
                _gocCK  = ck;

                SetGradeField(txtDiemCC,  cc);
                SetGradeField(txtDiemKT1, kt1);
                SetGradeField(txtDiemKT2, kt2);
                SetGradeField(txtDiemCK,  ck);

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

        private static void SetGradeField(Guna.UI2.WinForms.Guna2TextBox txt, decimal? gradeValue)
        {
            txt.Text = gradeValue.HasValue ? gradeValue.Value.ToString("0.#") : "";
            txt.ReadOnly = false;
            txt.BackColor = Color.White;
        }


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

            if (_bll.DaDuDiemThanhPhan(maSV, maLHP))
            {
                MessageBox.Show("Sinh viên đã có điểm thành phần.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtDiemCC.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm chuyên cần!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemCC.Focus(); return; }
            if (string.IsNullOrEmpty(txtDiemKT1.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT1.Focus(); return; }
            if (string.IsNullOrEmpty(txtDiemKT2.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 2!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT2.Focus(); return; }

            decimal cc, kt1, kt2, ck;
            bool hasCC, hasKT1, hasKT2, hasCK;
            if (!ValidateGrade(txtDiemCC, "chuyên cần", out cc, out hasCC)) return;
            if (!ValidateGrade(txtDiemKT1, "kiểm tra 1", out kt1, out hasKT1)) return;
            if (!ValidateGrade(txtDiemKT2, "kiểm tra 2", out kt2, out hasKT2)) return;
            if (!ValidateGrade(txtDiemCK, "thi cuối kỳ", out ck, out hasCK)) return;

            if (!hasCC || !hasKT1 || !hasKT2)
                return;

            try
            {
                _bll.LuuDiem(maSV, maLHP, "CC",  cc);
                _bll.LuuDiem(maSV, maLHP, "KT1", kt1);
                _bll.LuuDiem(maSV, maLHP, "KT2", kt2);
                if (hasCK)
                    _bll.LuuDiem(maSV, maLHP, "CK", ck);

                MessageBox.Show("Thêm điểm sinh viên thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshAfterSave(maSV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu điểm sinh viên: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnSua_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();

            if (string.IsNullOrEmpty(maSV) || cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sinh viên cần sửa điểm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string maLHP = cboLopHocPhan.SelectedValue.ToString();

            if (!_bll.DaDuDiemThanhPhan(maSV, maLHP))
            {
                MessageBox.Show(
                    "Sinh viên chưa có đủ điểm thành phần để sửa điểm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDiemCC.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm chuyên cần!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemCC.Focus(); return; }
            if (string.IsNullOrEmpty(txtDiemKT1.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT1.Focus(); return; }
            if (string.IsNullOrEmpty(txtDiemKT2.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập điểm kiểm tra 2!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDiemKT2.Focus(); return; }
            decimal cc, kt1, kt2, ck;
            bool hasCC, hasKT1, hasKT2, hasCK;
            if (!ValidateGrade(txtDiemCC, "chuyên cần", out cc, out hasCC)) return;
            if (!ValidateGrade(txtDiemKT1, "kiểm tra 1", out kt1, out hasKT1)) return;
            if (!ValidateGrade(txtDiemKT2, "kiểm tra 2", out kt2, out hasKT2)) return;
            if (!ValidateGrade(txtDiemCK, "thi cuối kỳ", out ck, out hasCK)) return;

            if (!hasCC || !hasKT1 || !hasKT2)
                return;

            bool coThayDoi =
                GradeChanged(_gocCC,  cc,  hasCC)  ||
                GradeChanged(_gocKT1, kt1, hasKT1) ||
                GradeChanged(_gocKT2, kt2, hasKT2) ||
                GradeChanged(_gocCK,  ck,  hasCK);

            if (!coThayDoi)
            {
                MessageBox.Show(
                    "Không có sự thay đổi điểm sinh viên",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (GradeChanged(_gocCC,  cc,  hasCC))  _bll.LuuDiem(maSV, maLHP, "CC",  cc);
                if (GradeChanged(_gocKT1, kt1, hasKT1)) _bll.LuuDiem(maSV, maLHP, "KT1", kt1);
                if (GradeChanged(_gocKT2, kt2, hasKT2)) _bll.LuuDiem(maSV, maLHP, "KT2", kt2);
                if (hasCK && GradeChanged(_gocCK, ck, hasCK))
                    _bll.LuuDiem(maSV, maLHP, "CK", ck);

                MessageBox.Show(
                    "Cập nhật điểm sinh viên thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshAfterSave(maSV);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật điểm sinh viên: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static bool GradeChanged(decimal? original, decimal current, bool hasCurrent)
        {
            if (!hasCurrent)
                return false;
            if (!original.HasValue)
                return true;
            return original.Value != current;
        }


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

            _gocCC = _gocKT1 = _gocKT2 = _gocCK = null;

            lstSinhVien.Focus();
        }


        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtDiemCC.Text  = "";
            txtDiemKT1.Text = "";
            txtDiemKT2.Text = "";
            txtDiemCK.Text  = "";
        }

        private void ClearInputs()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            txtDiemCC.Text = ""; txtDiemCC.ReadOnly = false;
            txtDiemKT1.Text = ""; txtDiemKT1.ReadOnly = false;
            txtDiemKT2.Text = ""; txtDiemKT2.ReadOnly = false;
            txtDiemCK.Text = ""; txtDiemCK.ReadOnly = false;

            _gocCC = _gocKT1 = _gocKT2 = _gocCK = null;
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

        private void CboNamHoc_SelectedIndexChanged(object sender, EventArgs e) => PopulateClasses();
        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e) => PopulateClasses();
        private void CboLopHocPhan_SelectedIndexChanged(object sender, EventArgs e) => LoadClassData();

    }
}
