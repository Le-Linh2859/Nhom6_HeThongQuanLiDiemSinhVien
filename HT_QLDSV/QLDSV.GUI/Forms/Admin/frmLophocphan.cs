using DocumentFormat.OpenXml.Bibliography;
using QLDSV.BLL;
using QLDSV.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmLophocphan : Form, IShellChildForm
    {
        private LopHocPhanBLL bll = new LopHocPhanBLL();

        private DataTable tblLophocphan;

        private bool detailPanelVisible = false;
        private bool isEditMode = false;
        private bool isAddingNew = false;
        private string currentMaLHP = "";

        public frmLophocphan()
        {
            InitializeComponent();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTim.Click += btnTim_Click;

            cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;
            cboFilterMon.SelectedIndexChanged += cboFilterMon_SelectedIndexChanged;
            cboEditKhoa.SelectedIndexChanged += cboEditKhoa_SelectedIndexChanged;
        }

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        private void frmLophocphan_Load(object sender, EventArgs e)
        {
             try
            {
                Connection.KetNoi();
                LoadInitialComboBoxes();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
        }

        private void LoadInitialComboBoxes()
        {
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                DataRow rAll = dtKhoa.NewRow();

                rAll["MaKhoa"] = "ALL";
                rAll["TenKhoa"] = "--- Tất cả ---";

                dtKhoa.Rows.InsertAt(rAll, 0);

                cboFilterKhoa.DataSource = dtKhoa.Copy();
                cboFilterKhoa.ValueMember = "MaKhoa";
                cboFilterKhoa.DisplayMember = "TenKhoa";
                cboFilterKhoa.SelectedIndex = 0;

                cboEditKhoa.DataSource = dtKhoa;
                cboEditKhoa.ValueMember = "MaKhoa";
                cboEditKhoa.DisplayMember = "TenKhoa";
                cboEditKhoa.SelectedIndex = -1;

                // Load học kỳ
                DataTable dtHK = bll.GetHocKy();
                cboEdithocky.DataSource = dtHK;
                cboEdithocky.ValueMember = "MaLoaiHK";
                cboEdithocky.DisplayMember = "TenLoaiHK";
                cboEdithocky.SelectedIndex = -1;

                // Load năm học
                DataTable dtNH = bll.GetNamHoc();
                cboEditnamhoc.DataSource = dtNH;
                cboEditnamhoc.ValueMember = "MaNamHoc";
                cboEditnamhoc.DisplayMember = "TenNamhoc";
                cboEditnamhoc.SelectedIndex = -1;

                UpdateFilterMonComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateFilterMonComboBox()
        {
            try
            {
                string maKhoa =
                    cboFilterKhoa.SelectedValue?.ToString() ?? "ALL";

                DataTable dtMon =
                    bll.GetMonHocTheoKhoa(maKhoa);

                DataRow rAll = dtMon.NewRow();

                rAll["MaMon"] = "ALL";
                rAll["TenMon"] = "--- Tất cả ---";

                dtMon.Rows.InsertAt(rAll, 0);

                cboFilterMon.DataSource = dtMon;
                cboFilterMon.ValueMember = "MaMon";
                cboFilterMon.DisplayMember = "TenMon";
                cboFilterMon.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        private void LoadData()
        {
            try
            {
                tblLophocphan = bll.GetDanhSachLopHocPhan(
                    txtTimKiem.Text.Trim(),
                    cboFilterKhoa.SelectedValue?.ToString(),
                    cboFilterMon.SelectedValue?.ToString());

                dataGridView.DataSource = tblLophocphan;

                dataGridView.Columns["MaLHP"].HeaderText = "Mã LHP";
                dataGridView.Columns["TenLopHocPhan"].HeaderText = "Tên lớp";
                dataGridView.Columns["ThoiGianHoc"].HeaderText = "Thời gian";
                dataGridView.Columns["PhongHoc"].HeaderText = "Phòng";
                dataGridView.Columns["TenKhoa"].HeaderText = "Khoa";
                dataGridView.Columns["TenMon"].HeaderText = "Môn học";
                dataGridView.Columns["TenGiangVien"].HeaderText = "Giảng viên";
                dataGridView.Columns["TrangThai"].HeaderText = "Trạng thái";
                if (dataGridView.Columns.Contains("TenLoaiHK"))
                    dataGridView.Columns["TenLoaiHK"].HeaderText = "Học kỳ";
                if (dataGridView.Columns.Contains("TenNamhoc"))
                    dataGridView.Columns["TenNamhoc"].HeaderText = "Năm học";
                if (dataGridView.Columns.Contains("ThoiGianBD"))
                    dataGridView.Columns["ThoiGianBD"].HeaderText = "Ngày bắt đầu";
                if (dataGridView.Columns.Contains("ThoiGianKT"))
                    dataGridView.Columns["ThoiGianKT"].HeaderText = "Ngày kết thúc";
                dataGridView.Columns["MaKhoa"].Visible = false;
                dataGridView.Columns["MaMon"].Visible = false;
                dataGridView.Columns["MaGV"].Visible = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblLophocphan.Rows.Count} lớp học phần";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateEditCascadeCombos(string maKhoa)
        {
            try
            {
                DataTable dtMon =
                    bll.GetMonHocTheoKhoa(maKhoa);

                cboEditMon.DataSource = dtMon;
                cboEditMon.ValueMember = "MaMon";
                cboEditMon.DisplayMember = "TenMon";

                DataTable dtGV =
                    bll.GetGiangVienTheoKhoa(maKhoa);

                cboEditGiangVien.DataSource = dtGV;
                cboEditGiangVien.ValueMember = "MaGV";
                cboEditGiangVien.DisplayMember = "HoTen";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            dataGridView.ClearSelection();
            ShowEditMode();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            isAddingNew = false;
            
            string maLHP = row.Cells["MaLHP"].Value?.ToString();
            string tenLHP = row.Cells["TenLopHocPhan"].Value?.ToString();
            string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
            string phong = row.Cells["PhongHoc"].Value?.ToString();
            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string maGV = row.Cells["MaGV"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            bool isActive = trangThai == "DangMo";

            DateTime thoiGianBD = DateTime.Today;
            DateTime thoiGianKT = DateTime.Today;
            if (row.Cells["ThoiGianBD"].Value != null && row.Cells["ThoiGianBD"].Value != DBNull.Value)
                DateTime.TryParse(row.Cells["ThoiGianBD"].Value.ToString(), out thoiGianBD);
            if (row.Cells["ThoiGianKT"].Value != null && row.Cells["ThoiGianKT"].Value != DBNull.Value)
                DateTime.TryParse(row.Cells["ThoiGianKT"].Value.ToString(), out thoiGianKT);

            ShowEditMode(maLHP, tenLHP, thoiGian, phong, maKhoa, maMon, maGV, isActive, thoiGianBD, thoiGianKT);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = dataGridView.SelectedRows[0].Cells["MaLHP"].Value?.ToString();
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học phần '{ma}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bll.Xoa(ma);
                    CloseSidebar();
                    LoadData();
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";

            cboFilterKhoa.SelectedIndex = 0;

            UpdateFilterMonComboBox();

            LoadData();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboFilterKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilterMonComboBox();

            LoadData();
        }

        private void cboFilterMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEditKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEditKhoa.SelectedValue != null && isEditMode)
            {
                PopulateEditCascadeCombos(cboEditKhoa.SelectedValue.ToString());
            }
        }

        private void btnLuuDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtEditMaLHP.Text.Trim();
                string ten = txtEditTenLHP.Text.Trim();
                string phong = txtEditPhongHoc.Text.Trim();
                string thu = cboEditThu.Text;
                int caHoc = int.Parse(cboEditCaHoc.Text);
                string thoiGian = $"{thu} - Ca {caHoc}";
                if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(phong))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc (*).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboEditMon.SelectedValue == null || cboEditGiangVien.SelectedValue == null || cboEditKhoa.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Khoa, Môn học và Giảng viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboEdithocky.SelectedValue == null || cboEditnamhoc.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Học kỳ và Năm học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maMon = cboEditMon.SelectedValue.ToString();
                string maGV = cboEditGiangVien.SelectedValue.ToString();
                string maKhoa = cboEditKhoa.SelectedValue.ToString();
                string trangThai = chkEditActive.Checked ? "DangMo" : "DaDong";
                string maLoaiHK = cboEdithocky.SelectedValue.ToString();
                string maNamHoc = cboEditnamhoc.SelectedValue.ToString();
                DateTime thoiGianBD = dtpEditThoiGianBD.Value.Date;
                DateTime thoiGianKT = dtpEditThoiGianKT.Value.Date;

                if (thoiGianKT < thoiGianBD)
                {
                    MessageBox.Show("Thời gian kết thúc phải sau thời gian bắt đầu.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string message;
                bool result;

                if (isAddingNew)
                {
                    result = bll.Them(ma, ten, thu, caHoc, phong, trangThai, maMon, maGV, maLoaiHK, maNamHoc, thoiGianBD, thoiGianKT, out message);
                }
                else
                {
                    result = bll.Sua(ma, ten, thu, caHoc, phong, trangThai, maMon, maGV, maLoaiHK, maNamHoc, thoiGianBD, thoiGianKT, out message);
                }

                MessageBox.Show(message);

                if (result)
                {
                    LoadData();
                    detailPanelVisible = false;

                    string tenKhoa = cboEditKhoa.Text;
                    string tenMon = cboEditMon.Text;
                    string tenGV = cboEditGiangVien.Text;
                    string tenHK = cboEdithocky.Text;
                    string tenNH = cboEditnamhoc.Text;
                    ShowViewMode(ma, ten, thoiGian, phong, tenKhoa, tenMon, tenGV, maKhoa, maMon, maGV, trangThai, tenHK, tenNH, thoiGianBD, thoiGianKT);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuyDetail_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                CloseSidebar();
            }
            else
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    detailPanelVisible = true;
                    
                    string maLHP = row.Cells["MaLHP"].Value?.ToString();
                    string tenLHP = row.Cells["TenLopHocPhan"].Value?.ToString();
                    string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
                    string phong = row.Cells["PhongHoc"].Value?.ToString();
                    string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
                    string tenMon = row.Cells["TenMon"].Value?.ToString();
                    string tenGV = row.Cells["TenGiangVien"].Value?.ToString();
                    string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
                    string maMon = row.Cells["MaMon"].Value?.ToString();
                    string maGV = row.Cells["MaGV"].Value?.ToString();
                    string trangThai = row.Cells["TrangThai"].Value?.ToString();
                    string tenHK = dataGridView.Columns.Contains("TenLoaiHK") ? row.Cells["TenLoaiHK"].Value?.ToString() : "";
                    string tenNH = dataGridView.Columns.Contains("TenNamhoc") ? row.Cells["TenNamhoc"].Value?.ToString() : "";

                    DateTime thoiGianBD = DateTime.Today;
                    DateTime thoiGianKT = DateTime.Today;
                    if (dataGridView.Columns.Contains("ThoiGianBD") && row.Cells["ThoiGianBD"].Value != null && row.Cells["ThoiGianBD"].Value != DBNull.Value)
                        DateTime.TryParse(row.Cells["ThoiGianBD"].Value.ToString(), out thoiGianBD);
                    if (dataGridView.Columns.Contains("ThoiGianKT") && row.Cells["ThoiGianKT"].Value != null && row.Cells["ThoiGianKT"].Value != DBNull.Value)
                        DateTime.TryParse(row.Cells["ThoiGianKT"].Value.ToString(), out thoiGianKT);

                    ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, maKhoa, maMon, maGV, trangThai, tenHK, tenNH, thoiGianBD, thoiGianKT);
                }
                else
                {
                    CloseSidebar();
                }
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isEditMode) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            
            string maLHP = row.Cells["MaLHP"].Value?.ToString();
            string tenLHP = row.Cells["TenLopHocPhan"].Value?.ToString();
            string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
            string phong = row.Cells["PhongHoc"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string tenGV = row.Cells["TenGiangVien"].Value?.ToString();
            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string maGV = row.Cells["MaGV"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();
            string hocKy = dataGridView.Columns.Contains("TenLoaiHK") ? row.Cells["TenLoaiHK"].Value?.ToString() : "";
            string namHoc = dataGridView.Columns.Contains("TenNamhoc") ? row.Cells["TenNamhoc"].Value?.ToString() : "";

            DateTime thoiGianBD = DateTime.Today;
            DateTime thoiGianKT = DateTime.Today;
            if (dataGridView.Columns.Contains("ThoiGianBD") && row.Cells["ThoiGianBD"].Value != null && row.Cells["ThoiGianBD"].Value != DBNull.Value)
                DateTime.TryParse(row.Cells["ThoiGianBD"].Value.ToString(), out thoiGianBD);
            if (dataGridView.Columns.Contains("ThoiGianKT") && row.Cells["ThoiGianKT"].Value != null && row.Cells["ThoiGianKT"].Value != DBNull.Value)
                DateTime.TryParse(row.Cells["ThoiGianKT"].Value.ToString(), out thoiGianKT);

            ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, maKhoa, maMon, maGV, trangThai, hocKy, namHoc, thoiGianBD, thoiGianKT);
        }

        private void ShowViewMode(string maLHP, string tenLHP, string thoiGian, string phong, string tenKhoa, string tenMon, string tenGV, string maKhoa, string maMon, string maGV, string trangThai, 
            string tenHK = "", string tenNH = "", DateTime? thoiGianBD = null, DateTime? thoiGianKT = null)
        {
            isEditMode = false;
            isAddingNew = false;
            currentMaLHP = maLHP;

            lblDetailHeader.Text = "Chi tiết Lớp Học Phần";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Toggle Visibility — labels visible, inputs hidden
            lblDetailMaLHP.Visible = true;
            lblDetailTenLHP.Visible = true;
            lblDetailThoiGianHoc.Visible = true;
            lblDetailPhongHoc.Visible = true;
            lblDetailKhoa.Visible = true;
            lblDetailMon.Visible = true;
            lblDetailGiangVien.Visible = true;
            lblDetailHocKy.Visible = true;
            lblDetailNamHoc.Visible = true;
            lblDetailTrangThai.Visible = true;
            lblDetailThoiGianBD.Visible = true;
            lblDetailThoiGianKT.Visible = true;

            txtEditMaLHP.Visible = false;
            txtEditTenLHP.Visible = false;
            txtEditPhongHoc.Visible = false;
            cboEditThu.Visible = false;
            cboEditCaHoc.Visible = false;
            cboEditKhoa.Visible = false;
            cboEditMon.Visible = false;
            cboEditGiangVien.Visible = false;
            cboEdithocky.Visible = false;
            cboEditnamhoc.Visible = false;
            chkEditActive.Visible = false;
            dtpEditThoiGianBD.Visible = false;
            dtpEditThoiGianKT.Visible = false;
            btnLuuDetail.Visible = false;
            btnHuyDetail.Visible = false;
            btnCloseDetail.Visible = true;

            // Load Values
            lblDetailMaLHP.Text = maLHP ?? "";
            lblDetailTenLHP.Text = tenLHP ?? "";
            lblDetailThoiGianHoc.Text = thoiGian ?? "";
            lblDetailPhongHoc.Text = phong ?? "";
            lblDetailKhoa.Text = tenKhoa ?? "";
            lblDetailMon.Text = tenMon ?? "";
            lblDetailGiangVien.Text = tenGV ?? "";
            lblDetailHocKy.Text = tenHK ?? "";
            lblDetailNamHoc.Text = tenNH ?? "";
            lblDetailTrangThai.Text = trangThai == "DangMo" ? "Đang mở" : "Đã đóng";
            lblDetailThoiGianBD.Text = thoiGianBD.HasValue ? thoiGianBD.Value.ToString("dd/MM/yyyy") : "";
            lblDetailThoiGianKT.Text = thoiGianKT.HasValue ? thoiGianKT.Value.ToString("dd/MM/yyyy") : "";

            OpenSidebar();
        }

        private void ShowEditMode(string maLHP = "", string tenLHP = "", string thoiGian = "", string phong = "", string maKhoa = "", string maMon = "", string maGV = "", bool isActive = true, DateTime? thoiGianBD = null, DateTime? thoiGianKT = null)
        {
            isEditMode = true;
            currentMaLHP = maLHP;

            lblDetailHeader.Text = isAddingNew ? "✚  Thêm lớp học phần mới" : "✎  Chỉnh sửa lớp học phần";
            lblDetailHeader.ForeColor = isAddingNew ? Color.FromArgb(27, 120, 53) : Color.FromArgb(180, 80, 0);

            // Toggle Visibility — inputs visible, labels hidden
            lblDetailMaLHP.Visible = false;
            lblDetailTenLHP.Visible = false;
            lblDetailThoiGianHoc.Visible = false;
            lblDetailPhongHoc.Visible = false;
            lblDetailKhoa.Visible = false;
            lblDetailMon.Visible = false;
            lblDetailGiangVien.Visible = false;
            lblDetailTrangThai.Visible = false;
            lblDetailThoiGianBD.Visible = false;
            lblDetailThoiGianKT.Visible = false;

            txtEditMaLHP.Visible = true;
            txtEditTenLHP.Visible = true;
            txtEditPhongHoc.Visible = true;
            cboEditThu.Visible = true;
            cboEditCaHoc.Visible = true;
            cboEditKhoa.Visible = true;
            cboEditMon.Visible = true;
            cboEditGiangVien.Visible = true;
            cboEdithocky.Visible = true;
            cboEditnamhoc.Visible = true;
            chkEditActive.Visible = true;
            dtpEditThoiGianBD.Visible = true;
            dtpEditThoiGianKT.Visible = true;
            btnLuuDetail.Visible = true;
            btnHuyDetail.Visible = true;
            btnCloseDetail.Visible = false;

            // Load values to inputs
            txtEditMaLHP.Text = maLHP;
            txtEditTenLHP.Text = tenLHP;
            txtEditPhongHoc.Text = phong;
            chkEditActive.Checked = isActive;
            dtpEditThoiGianBD.Value = thoiGianBD ?? DateTime.Today;
            dtpEditThoiGianKT.Value = thoiGianKT ?? DateTime.Today.AddMonths(4);

            if (!string.IsNullOrEmpty(maKhoa))
            {
                cboEditKhoa.SelectedValue = maKhoa;
                if (!string.IsNullOrEmpty(maMon))
                    cboEditMon.SelectedValue = maMon;
                if (!string.IsNullOrEmpty(maGV))
                    cboEditGiangVien.SelectedValue = maGV;
            }
            else
            {
                cboEditKhoa.SelectedIndex = -1;
            }

            txtEditMaLHP.ReadOnly = !isAddingNew;
            txtEditMaLHP.BackColor = isAddingNew ? Color.White : Color.FromArgb(240, 240, 240);

            OpenSidebar();
            txtEditTenLHP.Focus();
        }

        private void OpenSidebar()
        {
            if (!detailPanelVisible)
            {
                pnlDetail.Visible = true;
                Timer t = new Timer { Interval = 10 };
                t.Tick += (s, ev) =>
                {
                    if (pnlDetail.Width < 290)
                        pnlDetail.Width += 20;
                    else
                    {
                        pnlDetail.Width = 290;
                        t.Stop();
                        t.Dispose();
                    }
                };
                t.Start();
                detailPanelVisible = true;
            }
        }

        private void CloseSidebar()
        {
            Timer t = new Timer { Interval = 10 };
            t.Tick += (s, ev) =>
            {
                if (pnlDetail.Width > 0)
                    pnlDetail.Width -= 20;
                else
                {
                    pnlDetail.Width = 0;
                    pnlDetail.Visible = false;
                    t.Stop();
                    t.Dispose();
                    detailPanelVisible = false;
                    isEditMode = false;
                    isAddingNew = false;
                    dataGridView.ClearSelection();
                }
            };
            t.Start();
        }
        private void btnCloseDetail_Click(object sender, EventArgs e)
        {
            CloseSidebar();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }

        private void cboFilterMon_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}

