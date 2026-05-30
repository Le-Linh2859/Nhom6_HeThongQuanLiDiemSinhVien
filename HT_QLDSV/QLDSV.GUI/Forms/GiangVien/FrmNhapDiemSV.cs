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
        private string _pendingGridFocusMaSV;
        private string _highlightMaSV = "";
        private bool _cheDoNhapLai;
        private string _maSVTruocDo = "";
        private static readonly Color RowSavedHighlight = Color.LightGreen;


        public FrmNhapDiemSV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            dgvDiem.DataBindingComplete += DgvDiem_DataBindingComplete;
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
                    lblTongSV.Text = "Tổng số sinh viên nhập điểm: 0";
                    ClearInputs();
                    return;
                }

                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                DataTable dtSV = _bll.GetSinhVien(maLHP);
                lstSinhVien.SelectedIndexChanged -= LstSinhVien_SelectedIndexChanged;
                lstSinhVien.DataSource = dtSV;
                lstSinhVien.ValueMember = "MaSV";
                lstSinhVien.DisplayMember = "DisplayText";

                if (!string.IsNullOrEmpty(selectMaSV))
                    lstSinhVien.SelectedValue = selectMaSV;
                else
                {
                    lstSinhVien.SelectedIndex = -1;
                    ClearInputs();
                }

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

        private void RefreshAfterSave(string maSV)
        {
            _highlightMaSV = maSV;
            _pendingGridFocusMaSV = maSV;
            LoadClassData(maSV);
        }

        private void DgvDiem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyGridRowHighlight();
            if (string.IsNullOrEmpty(_pendingGridFocusMaSV))
                return;

            string maSV = _pendingGridFocusMaSV;
            _pendingGridFocusMaSV = null;
            FocusGridRow(maSV);
        }

        private void ApplyGridRowHighlight()
        {
            if (dgvDiem.Rows.Count == 0 || !dgvDiem.Columns.Contains("Mã SV"))
                return;

            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                if (row.IsNewRow) continue;

                string ma = row.Cells["Mã SV"].Value?.ToString();
                bool isSaved = !string.IsNullOrEmpty(_highlightMaSV)
                    && string.Equals(ma, _highlightMaSV, StringComparison.OrdinalIgnoreCase);

                if (isSaved)
                {
                    row.DefaultCellStyle.BackColor = RowSavedHighlight;
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(144, 238, 144);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                        ? Color.White
                        : Color.FromArgb(250, 250, 252);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 216, 242);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void FocusGridRow(string maSV)
        {
            if (string.IsNullOrEmpty(maSV) || !dgvDiem.Columns.Contains("Mã SV"))
                return;

            DataGridViewRow targetRow = null;
            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                if (row.IsNewRow) continue;
                string ma = row.Cells["Mã SV"].Value?.ToString();
                if (string.Equals(ma, maSV, StringComparison.OrdinalIgnoreCase))
                {
                    targetRow = row;
                    break;
                }
            }

            if (targetRow == null)
                return;

            dgvDiem.ClearSelection();
            targetRow.Selected = true;

            if (dgvDiem.Columns.Contains("Mã SV"))
                dgvDiem.CurrentCell = targetRow.Cells["Mã SV"];
            else if (dgvDiem.Columns.Count > 0)
                dgvDiem.CurrentCell = targetRow.Cells[0];

            int rowIndex = targetRow.Index;
            if (rowIndex >= 0)
            {
                int firstVisible = Math.Max(0, rowIndex - 2);
                if (firstVisible < dgvDiem.Rows.Count)
                    dgvDiem.FirstDisplayedScrollingRowIndex = firstVisible;
            }
        }

        private void LstSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSinhVien.SelectedValue == null || lstSinhVien.SelectedIndex < 0) return;

            try
            {
                string maSV = lstSinhVien.SelectedValue.ToString();
                string maLHP = cboLopHocPhan.SelectedValue.ToString();

                if (!string.Equals(maSV, _maSVTruocDo, StringComparison.OrdinalIgnoreCase))
                    _cheDoNhapLai = false;
                _maSVTruocDo = maSV;

                // Hiển thị thông tin sinh viên
                txtMaSV.Text = maSV;
                string[] parts = lstSinhVien.Text.Split(new[] { " - " }, StringSplitOptions.None);
                txtHoTen.Text = parts.Length > 1 ? parts[1] : "";

                if (_cheDoNhapLai)
                {
                    btnNhap.Enabled = true;
                    btnSua.Enabled  = false;
                    return;
                }

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

                if (!_cheDoNhapLai)
                    CapNhatTrangThaiNut(cc, kt1, kt2);
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

        private void CapNhatTrangThaiNut(decimal? cc, decimal? kt1, decimal? kt2)
        {
            bool hasStudent = !string.IsNullOrEmpty(txtMaSV.Text.Trim());
            bool daDu = cc.HasValue && kt1.HasValue && kt2.HasValue;

            if (_cheDoNhapLai)
            {
                btnNhap.Enabled = hasStudent;
                btnSua.Enabled  = false;
                return;
            }

            btnNhap.Enabled = hasStudent && !daDu;
            btnSua.Enabled  = hasStudent && daDu;
        }

        private bool ThuThapDiemTuForm(
            out decimal cc, out decimal kt1, out decimal kt2, out decimal ck,
            out bool hasCC, out bool hasKT1, out bool hasKT2, out bool hasCK)
        {
            cc = kt1 = kt2 = ck = 0;
            hasCC = hasKT1 = hasKT2 = hasCK = false;

            if (!ValidateGrade(txtDiemCC,  "chuyên cần",  out cc,  out hasCC))  return false;
            if (!ValidateGrade(txtDiemKT1, "kiểm tra 1",  out kt1, out hasKT1)) return false;
            if (!ValidateGrade(txtDiemKT2, "kiểm tra 2",  out kt2, out hasKT2)) return false;
            if (!ValidateGrade(txtDiemCK,  "thi cuối kỳ", out ck,  out hasCK))  return false;
            return true;
        }

        private bool KiemTraBatBuocBaDiemThanhPhan()
        {
            if (string.IsNullOrEmpty(txtDiemCC.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập điểm chuyên cần!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemCC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiemKT1.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập điểm kiểm tra 1!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemKT1.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiemKT2.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập điểm kiểm tra 2!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiemKT2.Focus();
                return false;
            }
            return true;
        }

        private void LuuBoDiemNhap(string maSV, string maLHP,
            decimal cc, decimal kt1, decimal kt2, decimal ck, bool hasCK)
        {
            _bll.LuuDiem(maSV, maLHP, "CC",  cc);
            _bll.LuuDiem(maSV, maLHP, "KT1", kt1);
            _bll.LuuDiem(maSV, maLHP, "KT2", kt2);
            if (hasCK)
                _bll.LuuDiem(maSV, maLHP, "CK", ck);
        }

        private void LuuDiemKhiSua(string maSV, string maLHP,
            decimal cc, decimal kt1, decimal kt2, decimal ck,
            bool hasCC, bool hasKT1, bool hasKT2, bool hasCK)
        {
            XuLyLoaiDiemKhiSua(maSV, maLHP, "CC",  _gocCC,  cc,  hasCC);
            XuLyLoaiDiemKhiSua(maSV, maLHP, "KT1", _gocKT1, kt1, hasKT1);
            XuLyLoaiDiemKhiSua(maSV, maLHP, "KT2", _gocKT2, kt2, hasKT2);
            XuLyLoaiDiemKhiSua(maSV, maLHP, "CK",  _gocCK,  ck,  hasCK);
        }

        private void XuLyLoaiDiemKhiSua(string maSV, string maLHP, string loai,
            decimal? goc, decimal moi, bool coGiaTriMoi)
        {
            if (coGiaTriMoi && (!goc.HasValue || goc.Value != moi))
                _bll.LuuDiem(maSV, maLHP, loai, moi);
            else if (!coGiaTriMoi && goc.HasValue)
                _bll.XoaDiem(maSV, maLHP, loai);
        }

        private bool CoThayDoiSoVoiGoc(
            decimal cc, decimal kt1, decimal kt2, decimal ck,
            bool hasCC, bool hasKT1, bool hasKT2, bool hasCK)
        {
            return LoaiDiemThayDoi(_gocCC,  cc,  hasCC)
                || LoaiDiemThayDoi(_gocKT1, kt1, hasKT1)
                || LoaiDiemThayDoi(_gocKT2, kt2, hasKT2)
                || LoaiDiemThayDoi(_gocCK,  ck,  hasCK);
        }

        private static bool LoaiDiemThayDoi(decimal? goc, decimal moi, bool coGiaTriMoi)
        {
            if (!coGiaTriMoi)
                return goc.HasValue;
            if (!goc.HasValue)
                return true;
            return goc.Value != moi;
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

            if (!_cheDoNhapLai && _bll.DaDuDiemThanhPhan(maSV, maLHP))
            {
                MessageBox.Show(
                    "Sinh viên đã có đủ điểm thành phần.\nVui lòng ấn Reset để nhập lại, hoặc dùng Sửa để chỉnh điểm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!KiemTraBatBuocBaDiemThanhPhan())
                return;

            decimal cc, kt1, kt2, ck;
            bool hasCC, hasKT1, hasKT2, hasCK;
            if (!ThuThapDiemTuForm(out cc, out kt1, out kt2, out ck, out hasCC, out hasKT1, out hasKT2, out hasCK))
                return;

            if (!hasCC || !hasKT1 || !hasKT2)
                return;

            try
            {
                if (_cheDoNhapLai)
                    _bll.XoaTatCaDiemSinhVienLop(maSV, maLHP);

                LuuBoDiemNhap(maSV, maLHP, cc, kt1, kt2, ck, hasCK);

                MessageBox.Show("Thêm điểm sinh viên thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _cheDoNhapLai = false;
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

            decimal cc, kt1, kt2, ck;
            bool hasCC, hasKT1, hasKT2, hasCK;
            if (!ThuThapDiemTuForm(out cc, out kt1, out kt2, out ck, out hasCC, out hasKT1, out hasKT2, out hasCK))
                return;

            if (!CoThayDoiSoVoiGoc(cc, kt1, kt2, ck, hasCC, hasKT1, hasKT2, hasCK))
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
                LuuDiemKhiSua(maSV, maLHP, cc, kt1, kt2, ck, hasCC, hasKT1, hasKT2, hasCK);

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

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            lstSinhVien.SelectedIndexChanged -= LstSinhVien_SelectedIndexChanged;
            lstSinhVien.SelectedIndex = -1;
            lstSinhVien.SelectedIndexChanged += LstSinhVien_SelectedIndexChanged;

            ClearInputs();
            XoaHighlightGrid();

            btnNhap.Enabled = true;
            btnSua.Enabled = false;

            lstSinhVien.Focus();
        }

        private void XoaHighlightGrid()
        {
            _highlightMaSV = "";
            _pendingGridFocusMaSV = null;
            dgvDiem.ClearSelection();
            ApplyGridRowHighlight();
        }


        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước khi Reset!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetGradeField(txtDiemCC,  null);
            SetGradeField(txtDiemKT1, null);
            SetGradeField(txtDiemKT2, null);
            SetGradeField(txtDiemCK,  null);

            _gocCC = _gocKT1 = _gocKT2 = _gocCK = null;
            _cheDoNhapLai = true;
            XoaHighlightGrid();

            btnNhap.Enabled = true;
            btnSua.Enabled  = false;

            txtDiemCC.Focus();
        }

        private void ClearInputs()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";

            SetGradeField(txtDiemCC,  null);
            SetGradeField(txtDiemKT1, null);
            SetGradeField(txtDiemKT2, null);
            SetGradeField(txtDiemCK,  null);

            _gocCC = _gocKT1 = _gocKT2 = _gocCK = null;
            _cheDoNhapLai = false;
            _maSVTruocDo = "";
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
