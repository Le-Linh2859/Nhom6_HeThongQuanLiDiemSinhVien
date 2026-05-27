using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
// SessionHelper và FunctionQa nằm ở namespace QLDSV.GUI (tầng GUI root)
using QLDSV.GUI;

namespace QLDSV.GUI.Forms.GiangVien
{
    /// <summary>
    /// Form nhập điểm sinh viên dành cho Giảng viên (role VT002).
    /// Luồng: Chọn Học kỳ → Chọn Lớp HP (chỉ lớp GV đang dạy) → Danh sách SV hiện ra
    /// → Chọn SV → Nhập CC / KT1 / KT2 (và tùy chọn Điểm Thi) → Lưu.
    /// </summary>
    public partial class FrmNhapDiemSV : Form, IShellChildForm
    {
        // ── IShellChildForm ──────────────────────────────────────────────────────
        public void OnEmbeddedInShell()
        {
            // Form này không có sidebar/header nội bộ nên chỉ cần đảm bảo
            // DataGridView anchor đúng khi được nhúng vào Shell panel
            dgvDiem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                           | AnchorStyles.Left | AnchorStyles.Right;
        }
        // ── Trạng thái form ─────────────────────────────────────────────────────
        private bool _isAdding  = false;
        private bool _isEditing = false;

        // Mã GV đang đăng nhập (lấy từ SessionHelper qua MaTaiKhoan)
        private string _maGVHienTai = "";

        // Lớp học phần đang được chọn
        private string _maLHPHienTai = "";

        // DataTable cache cho danh sách điểm
        private DataTable _dtDiem;

        // ── Constructor ─────────────────────────────────────────────────────────
        public FrmNhapDiemSV()
        {
            InitializeComponent();

            // Wire up events
            this.btnThem.Click      += btnThem_Click;
            this.btnSua.Click       += btnSua_Click;
            this.btnLuu.Click       += btnLuu_Click;
            this.btnHuy.Click       += btnHuy_Click;
            this.btnReset.Click     += btnReset_Click;
            this.btnTimKiem.Click   += btnTimKiem_Click;
            this.btnLamMoi.Click    += btnLamMoi_Click;

            this.cboHocKy.SelectedIndexChanged      += cboHocKy_SelectedIndexChanged;
            this.cboLopHocPhan.SelectedIndexChanged += cboLopHocPhan_SelectedIndexChanged;
            this.cboSinhVien.SelectedIndexChanged   += cboSinhVien_SelectedIndexChanged;

            this.txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnTimKiem_Click(null, null); };

            this.dgvDiem.CellClick += dgvDiem_CellClick;
        }

        // ── Load ────────────────────────────────────────────────────────────────
        private void FrmNhapDiemSV_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();
                ResolveCurrentGiangVien();
                LoadHocKyCombo();
                ResetFormState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Lấy MaGV từ tài khoản đang đăng nhập ───────────────────────────────
        private void ResolveCurrentGiangVien()
        {
            string maTK = SessionHelper.MaTaiKhoan ?? "";
            if (string.IsNullOrEmpty(maTK)) return;

            _maGVHienTai = FunctionQa.getfieldvalue(
                $"SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '{maTK}'");

            // Fallback: một số DB dùng cột khác
            if (string.IsNullOrEmpty(_maGVHienTai))
            {
                _maGVHienTai = FunctionQa.getfieldvalue(
                    $"SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '{maTK}'");
            }
        }

        // ── Nạp ComboBox Học kỳ - Năm học ──────────────────────────────────────
        private void LoadHocKyCombo()
        {
            try
            {
                string sql = "SELECT MaHKNH, TenHKNH FROM HocKyNamHoc ORDER BY TenHKNH DESC";
                DataTable dt = FunctionQa.getdatatotable(sql);

                cboHocKy.SelectedIndexChanged -= cboHocKy_SelectedIndexChanged;
                cboHocKy.DataSource    = dt;
                cboHocKy.ValueMember   = "MaHKNH";
                cboHocKy.DisplayMember = "TenHKNH";
                cboHocKy.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
                cboHocKy.SelectedIndexChanged += cboHocKy_SelectedIndexChanged;

                if (dt.Rows.Count > 0)
                    LoadLopHocPhanCombo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp học kỳ: " + ex.Message);
            }
        }

        // ── Nạp ComboBox Lớp học phần (chỉ lớp GV đang dạy) ───────────────────
        private void LoadLopHocPhanCombo()
        {
            try
            {
                string maHKNH = cboHocKy.SelectedValue?.ToString() ?? "";
                if (string.IsNullOrEmpty(maHKNH)) return;

                // Nếu chưa resolve được MaGV thì lấy tất cả lớp trong HK (fallback)
                string sqlWhere = string.IsNullOrEmpty(_maGVHienTai)
                    ? $"WHERE lhp.MaHKNH = '{maHKNH}'"
                    : $"WHERE lhp.MaHKNH = '{maHKNH}' AND lhp.MaGV = '{_maGVHienTai}'";

                string sql = $@"
                    SELECT lhp.MaLHP,
                           lhp.TenLopHocPhan + ' (' + mh.TenMon + ')' AS TenHienThi
                    FROM LopHocPhan lhp
                    LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                    {sqlWhere}
                    ORDER BY lhp.TenLopHocPhan";

                DataTable dt = FunctionQa.getdatatotable(sql);

                cboLopHocPhan.SelectedIndexChanged -= cboLopHocPhan_SelectedIndexChanged;
                cboLopHocPhan.DataSource    = dt;
                cboLopHocPhan.ValueMember   = "MaLHP";
                cboLopHocPhan.DisplayMember = "TenHienThi";
                cboLopHocPhan.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
                cboLopHocPhan.SelectedIndexChanged += cboLopHocPhan_SelectedIndexChanged;

                if (dt.Rows.Count > 0)
                    LoadDanhSachDiem();
                else
                {
                    _maLHPHienTai = "";
                    ClearGrid();
                    LoadSinhVienCombo();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp lớp học phần: " + ex.Message);
            }
        }

        // ── Nạp ComboBox Sinh viên theo lớp HP đang chọn ───────────────────────
        private void LoadSinhVienCombo()
        {
            try
            {
                cboSinhVien.SelectedIndexChanged -= cboSinhVien_SelectedIndexChanged;
                cboSinhVien.DataSource = null;
                cboSinhVien.Items.Clear();

                if (string.IsNullOrEmpty(_maLHPHienTai))
                {
                    cboSinhVien.SelectedIndexChanged += cboSinhVien_SelectedIndexChanged;
                    return;
                }

                string sql = $@"
                    SELECT sv.MaSV, sv.HoTen
                    FROM SinhVien sv
                    JOIN DangKyLopHoc dk ON sv.MaSV = dk.MaSV
                    WHERE dk.MaLHP = '{_maLHPHienTai}'
                    ORDER BY sv.HoTen";

                DataTable dt = FunctionQa.getdatatotable(sql);
                cboSinhVien.DataSource    = dt;
                cboSinhVien.ValueMember   = "MaSV";
                cboSinhVien.DisplayMember = "MaSV";
                cboSinhVien.SelectedIndex = -1;
                cboSinhVien.SelectedIndexChanged += cboSinhVien_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp sinh viên: " + ex.Message);
            }
        }

        // ── Nạp DataGridView tổng kết điểm ─────────────────────────────────────
        private void LoadDanhSachDiem()
        {
            try
            {
                _maLHPHienTai = cboLopHocPhan.SelectedValue?.ToString() ?? "";
                if (string.IsNullOrEmpty(_maLHPHienTai)) { ClearGrid(); return; }

                string keyword = txtTimKiem.Text.Trim();
                string sqlSearch = "";
                if (!string.IsNullOrEmpty(keyword))
                {
                    string esc = keyword.Replace("'", "''");
                    sqlSearch = $" AND (sv.MaSV LIKE '%{esc}%' OR sv.HoTen LIKE N'%{esc}%')";
                }

                string sql = $@"
                    SELECT
                        sv.MaSV                                                          AS [Mã SV],
                        sv.HoTen                                                         AS [Họ tên],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END)           AS [Chuyên cần],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END)           AS [KT1],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END)           AS [KT2],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END)           AS [Điểm thi],
                        CASE
                            WHEN MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) IS NOT NULL
                             AND MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) IS NOT NULL
                             AND MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) IS NOT NULL
                             AND MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) IS NOT NULL
                            THEN ROUND(
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) * 0.1 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) * 0.6, 2)
                            ELSE NULL
                        END                                                              AS [Tổng kết],
                        CASE
                            WHEN MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) IS NULL
                              OR MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) IS NULL
                              OR MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) IS NULL
                              OR MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) IS NULL
                            THEN N'Chưa đủ điểm'
                            WHEN ROUND(
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) * 0.1 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) * 0.6, 2) >= 8.5 THEN 'A'
                            WHEN ROUND(
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) * 0.1 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) * 0.6, 2) >= 7.0 THEN 'B'
                            WHEN ROUND(
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) * 0.1 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) * 0.6, 2) >= 5.5 THEN 'C'
                            WHEN ROUND(
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) * 0.1 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) * 0.15 +
                                MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) * 0.6, 2) >= 4.0 THEN 'D'
                            ELSE 'F'
                        END                                                              AS [Xếp loại]
                    FROM SinhVien sv
                    JOIN DangKyLopHoc dk ON sv.MaSV = dk.MaSV
                    LEFT JOIN KetQua kq ON kq.MaSV = sv.MaSV AND kq.MaLHP = dk.MaLHP
                    WHERE dk.MaLHP = '{_maLHPHienTai}' {sqlSearch}
                    GROUP BY sv.MaSV, sv.HoTen
                    ORDER BY sv.HoTen";

                _dtDiem = FunctionQa.getdatatotable(sql);
                dgvDiem.DataSource = _dtDiem;
                StyleGrid();
                lblSoSV.Text = $"Tổng: {_dtDiem.Rows.Count} sinh viên";

                LoadSinhVienCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Style DataGridView ──────────────────────────────────────────────────
        private void StyleGrid()
        {
            if (dgvDiem.Columns.Count == 0) return;
            dgvDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 58, 178);
            dgvDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDiem.EnableHeadersVisualStyles = false;
            dgvDiem.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 196, 255);
            dgvDiem.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiem.RowHeadersVisible = false;

            // Tô màu cột Xếp loại
            if (dgvDiem.Columns.Contains("Xếp loại"))
            {
                dgvDiem.Columns["Xếp loại"].DefaultCellStyle.Font =
                    new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void ClearGrid()
        {
            dgvDiem.DataSource = null;
            _dtDiem = null;
            lblSoSV.Text = "Tổng: 0 sinh viên";
        }

        // ── Điền điểm hiện có của SV vào form nhập ─────────────────────────────
        private void LoadDiemHienCoVaoForm(string maSV)
        {
            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(_maLHPHienTai)) return;
            try
            {
                string sql = $@"
                    SELECT MaLoaiDiem, Diem FROM KetQua
                    WHERE MaSV = '{maSV}' AND MaLHP = '{_maLHPHienTai}'";
                DataTable dt = FunctionQa.getdatatotable(sql);

                txtDiemCC.Text  = "";
                txtDiemKT1.Text = "";
                txtDiemKT2.Text = "";
                txtDiemThi.Text = "";

                foreach (DataRow row in dt.Rows)
                {
                    string loai = row["MaLoaiDiem"].ToString().Trim().ToUpper();
                    string diem = row["Diem"] == DBNull.Value ? "" : row["Diem"].ToString();
                    switch (loai)
                    {
                        case "CC":  txtDiemCC.Text  = diem; break;
                        case "KT1": txtDiemKT1.Text = diem; break;
                        case "KT2": txtDiemKT2.Text = diem; break;
                        case "CK":  txtDiemThi.Text = diem; break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi load điểm hiện có: " + ex.Message);
            }
        }

        // ── Validate điểm (0-10, tối đa 1 chữ số thập phân) ───────────────────
        private bool TryParseScore(string input, string fieldName, bool required, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input))
            {
                if (required)
                {
                    MessageBox.Show($"{fieldName} không được để trống.", "Thiếu thông tin",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true; // optional field, skip
            }
            if (!double.TryParse(input.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out value)
                || value < 0 || value > 10)
            {
                MessageBox.Show($"{fieldName} phải là số từ 0 đến 10.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ── Upsert một loại điểm vào bảng KetQua ───────────────────────────────
        private void UpsertDiem(string maSV, string maLHP, string maLoaiDiem, double diem)
        {
            bool exists = FunctionQa.checkkey(
                $"SELECT 1 FROM KetQua WHERE MaSV='{maSV}' AND MaLHP='{maLHP}' AND MaLoaiDiem='{maLoaiDiem}'");

            if (exists)
                FunctionQa.runsql(
                    $"UPDATE KetQua SET Diem={diem.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)} " +
                    $"WHERE MaSV='{maSV}' AND MaLHP='{maLHP}' AND MaLoaiDiem='{maLoaiDiem}'");
            else
                FunctionQa.runsql(
                    $"INSERT INTO KetQua (MaSV, MaLHP, MaLoaiDiem, Diem) " +
                    $"VALUES ('{maSV}', '{maLHP}', '{maLoaiDiem}', " +
                    $"{diem.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)})");
        }

        // ── Trạng thái form ─────────────────────────────────────────────────────
        private void ResetFormState()
        {
            _isAdding  = false;
            _isEditing = false;

            // Khóa input
            cboSinhVien.Enabled  = false;
            txtDiemCC.Enabled    = false;
            txtDiemKT1.Enabled   = false;
            txtDiemKT2.Enabled   = false;
            txtDiemThi.Enabled   = false;

            // Nút
            btnThem.Enabled  = true;
            btnSua.Enabled   = true;
            btnLuu.Enabled   = false;
            btnHuy.Enabled   = false;
            btnReset.Enabled = false;

            ClearInputFields();
        }

        private void SetEditState(bool adding)
        {
            _isAdding  = adding;
            _isEditing = !adding;

            cboSinhVien.Enabled  = adding;   // Khi sửa, SV đã cố định
            txtDiemCC.Enabled    = true;
            txtDiemKT1.Enabled   = true;
            txtDiemKT2.Enabled   = true;
            txtDiemThi.Enabled   = true;

            btnThem.Enabled  = false;
            btnSua.Enabled   = false;
            btnLuu.Enabled   = true;
            btnHuy.Enabled   = true;
            btnReset.Enabled = true;
        }

        private void ClearInputFields()
        {
            cboSinhVien.SelectedIndex = -1;
            txtTenSV.Text    = "";
            txtDiemCC.Text   = "";
            txtDiemKT1.Text  = "";
            txtDiemKT2.Text  = "";
            txtDiemThi.Text  = "";
        }

        // ── Sự kiện ComboBox ────────────────────────────────────────────────────
        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLopHocPhanCombo();
            ResetFormState();
        }

        private void cboLopHocPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSachDiem();
            ResetFormState();
        }

        private void cboSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSinhVien.SelectedValue == null) { txtTenSV.Text = ""; return; }
            string maSV = cboSinhVien.SelectedValue.ToString();
            txtTenSV.Text = FunctionQa.getfieldvalue(
                $"SELECT HoTen FROM SinhVien WHERE MaSV = '{maSV}'");

            if (_isEditing)
                LoadDiemHienCoVaoForm(maSV);
        }

        // ── Sự kiện click dòng DataGridView ────────────────────────────────────
        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isAdding || _isEditing) return;
            DataGridViewRow row = dgvDiem.Rows[e.RowIndex];
            string maSV = row.Cells["Mã SV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;

            // Hiển thị thông tin SV lên form nhập (chỉ xem, chưa vào chế độ sửa)
            cboSinhVien.SelectedValue = maSV;
            txtTenSV.Text = row.Cells["Họ tên"].Value?.ToString() ?? "";
            txtDiemCC.Text  = row.Cells["Chuyên cần"].Value == DBNull.Value ? "" : row.Cells["Chuyên cần"].Value?.ToString() ?? "";
            txtDiemKT1.Text = row.Cells["KT1"].Value == DBNull.Value ? "" : row.Cells["KT1"].Value?.ToString() ?? "";
            txtDiemKT2.Text = row.Cells["KT2"].Value == DBNull.Value ? "" : row.Cells["KT2"].Value?.ToString() ?? "";
            txtDiemThi.Text = row.Cells["Điểm thi"].Value == DBNull.Value ? "" : row.Cells["Điểm thi"].Value?.ToString() ?? "";
        }

        // ── Nút Thêm ────────────────────────────────────────────────────────────
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maLHPHienTai))
            {
                MessageBox.Show("Vui lòng chọn Lớp học phần trước.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ClearInputFields();
            SetEditState(adding: true);
            cboSinhVien.Focus();
        }

        // ── Nút Sửa ─────────────────────────────────────────────────────────────
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDiem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa điểm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSV = dgvDiem.SelectedRows[0].Cells["Mã SV"].Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(maSV)) return;

            SetEditState(adding: false);
            cboSinhVien.SelectedValue = maSV;
            LoadDiemHienCoVaoForm(maSV);
            txtDiemCC.Focus();
        }

        // ── Nút Lưu ─────────────────────────────────────────────────────────────
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate SV
            if (cboSinhVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSinhVien.Focus();
                return;
            }
            if (string.IsNullOrEmpty(_maLHPHienTai))
            {
                MessageBox.Show("Vui lòng chọn Lớp học phần.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate điểm (CC, KT1, KT2 bắt buộc; Thi tùy chọn)
            double cc, kt1, kt2, thi = 0;
            if (!TryParseScore(txtDiemCC.Text,  "Điểm Chuyên cần", required: true,  out cc))  { txtDiemCC.Focus();  return; }
            if (!TryParseScore(txtDiemKT1.Text, "Điểm Kiểm tra 1", required: true,  out kt1)) { txtDiemKT1.Focus(); return; }
            if (!TryParseScore(txtDiemKT2.Text, "Điểm Kiểm tra 2", required: true,  out kt2)) { txtDiemKT2.Focus(); return; }
            bool hasThi = !string.IsNullOrWhiteSpace(txtDiemThi.Text);
            if (hasThi && !TryParseScore(txtDiemThi.Text, "Điểm thi", required: false, out thi)) { txtDiemThi.Focus(); return; }

            string maSV = cboSinhVien.SelectedValue.ToString();

            try
            {
                UpsertDiem(maSV, _maLHPHienTai, "CC",  cc);
                UpsertDiem(maSV, _maLHPHienTai, "KT1", kt1);
                UpsertDiem(maSV, _maLHPHienTai, "KT2", kt2);
                if (hasThi)
                    UpsertDiem(maSV, _maLHPHienTai, "CK", thi);

                MessageBox.Show("Lưu điểm thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhSachDiem();
                ResetFormState();

                // Chọn lại dòng SV vừa lưu
                foreach (DataGridViewRow row in dgvDiem.Rows)
                {
                    if (row.Cells["Mã SV"].Value?.ToString() == maSV)
                    {
                        row.Selected = true;
                        dgvDiem.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Nút Hủy ─────────────────────────────────────────────────────────────
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetFormState();
        }

        // ── Nút Reset ───────────────────────────────────────────────────────────
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_isAdding)
            {
                // Xóa trắng toàn bộ input
                ClearInputFields();
                cboSinhVien.Focus();
            }
            else if (_isEditing)
            {
                // Nạp lại điểm gốc từ DB
                string maSV = cboSinhVien.SelectedValue?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maSV))
                    LoadDiemHienCoVaoForm(maSV);
            }
        }

        // ── Nút Tìm kiếm ────────────────────────────────────────────────────────
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachDiem();
        }

        // ── Nút Làm mới ─────────────────────────────────────────────────────────
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadDanhSachDiem();
            ResetFormState();
        }
    }
}
