using QLDSV.BLL;
using QLDSV.DAL;
using QLDSV.GUI.Forms.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;


    namespace QLDSV.GUI
{
    public partial class frmGiangvien : Form, IShellChildForm
    {
        private GiangVienBLL bll = new GiangVienBLL();
        private bool isAdding = false;
        private bool isEditing = false;
        private DataTable dtGiangVien;

        public void OnEmbeddedInShell()
        {
            // Ẩn tất cả các control Sidebar & Header trùng lặp bằng tìm kiếm động an toàn
            string[] controlNames = { 
                "pnlSidebar", "pnlHeader", "guna2ImageButton1", "label3", "label4", 
                "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13", 
                "guna2HtmlLabel14", "guna2ImageButton3" 
            };
            foreach (var name in controlNames)
            {
                var ct = this.Controls.Find(name, true);
                foreach (var c in ct)
                {
                    c.Visible = false;
                }
            }

            int shiftX = 0;
            if (pnlSidebar != null)
            {
                shiftX = pnlSidebar.Width;
            }

            if (shiftX == 0) return;

            foreach (Control ctrl in this.Controls)
            {
                bool isHiddenControl = false;
                foreach (var name in controlNames)
                {
                    if (ctrl.Name == name)
                    {
                        isHiddenControl = true;
                        break;
                    }
                }

                if (!isHiddenControl && ctrl.Left > 0)
                {
                    ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
                }
            }
        }
        public frmGiangvien()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.frmGiangvien_Load);

            ThemeHelper.ApplyTheme(this);

            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
            this.btnReset.Click += btnReset_Click;
            this.btnLammoi.Click += btnLammoi_Click;

            this.DataGridViewGV.CellClick += DataGridViewGV_CellClick;

            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên ...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm kiếm theo mã, tên giảng viên ...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };

            // Wire up Logout / Close button if it exists
            if (this.btnSignout != null)
            {
                this.btnSignout.Click += (s, e) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        MessageBox.Show("Đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                };
            }
        }
        private void frmGiangvien_Resize(object sender, EventArgs e)
        {
            guna2Panel1.Width = this.ClientSize.Width - 210;
            groupBox1.Width = this.ClientSize.Width - 210;
        }
        private void frmGiangvien_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();

                LoadComboboxFilters();
                LoadComboboxDetails();
                LoadData();
                ResetFormState();
                WireSidebarEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DataGridViewGV.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            DataGridViewGV.ThemeStyle.HeaderStyle.ForeColor = Color.White;
        }

        // 1. Tải danh sách lên GridView
        private void LoadData()
        {
            try
            {
                dtGiangVien = bll.GetGiangVien();
                DataGridViewGV.DataSource = dtGiangVien;

                if (DataGridViewGV.Columns.Count > 0)
                {
                    DataGridViewGV.Columns["MaGV"].HeaderText = "Mã giảng viên";
                    DataGridViewGV.Columns["HoTen"].HeaderText = "Họ tên";
                    DataGridViewGV.Columns["GioiTinh"].HeaderText = "Giới tính";
                    DataGridViewGV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    DataGridViewGV.Columns["SoDT"].HeaderText = "Số điện thoại";
                    DataGridViewGV.Columns["Email"].HeaderText = "Email";

                    // DataGridViewGV.Columns["MaKhoa"].HeaderText = "Mã khoa";
                    DataGridViewGV.Columns["MaKhoa"].Visible = false;

                    // Ẩn nếu chưa dùng
                    DataGridViewGV.Columns["MaTaiKhoan"].Visible = false;
                }

                DataGridViewGV.AllowUserToAddRows = false;
                DataGridViewGV.EditMode = DataGridViewEditMode.EditProgrammatically;
                DataGridViewGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (DataGridViewGV.Rows.Count > 0)
                {
                    DataGridViewGV.ClearSelection();
                    DataGridViewGV.Rows[0].Selected = true;

                    DataGridViewGV_CellClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // 2. Nạp dữ liệu các bộ lọc Khoa 
        private void LoadComboboxFilters()
        {
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                DataTable dtFilter = dtKhoa.Copy();

                DataRow row = dtFilter.NewRow();

                row["MaKhoa"] = "ALL";
                row["TenKhoa"] = "Tất cả";

                dtFilter.Rows.InsertAt(row, 0);

                cboKhoa.DataSource = dtFilter;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";

                cboKhoa.SelectedIndex = 0;

                cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 3. Nạp dữ liệu các Combobox nhập liệu chi tiết
        private void LoadComboboxDetails()
        {
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                cboKhoa2.DataSource = dtKhoa;
                cboKhoa2.DisplayMember = "TenKhoa";
                cboKhoa2.ValueMember = "MaKhoa";

                cboKhoa2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 4. Khôi phục trạng thái xem ban đầu của Form
        private void ResetFormState()
        {
            isAdding = false;
            isEditing = false;

            // Khóa nhập liệu
            cboMaGV.Enabled = false;
            cboTenGV.Enabled = false;
            cboDiaChi.Enabled = false;
            cboEmail.Enabled = false;

            mskSoDienThoai.Enabled = false;

            rdoNam.Enabled = false;
            rdoNu.Enabled = false;

            cboKhoa2.Enabled = false;

            cboMatKhau.Enabled = false;
            cboMatKhau1.Enabled = false;

            // Ẩn mật khẩu
            cboMatKhau.Visible = false;
            cboMatKhau1.Visible = false;

            lblMatKhau.Visible = false;
            lblNhapLaiMK.Visible = false;

            // Nút
            btnThem.Enabled = true;
            btnSua.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnReset.Enabled = false;

            // Load lại dòng đang chọn
            if (DataGridViewGV.SelectedRows.Count > 0)
            {
                DataGridViewGV_CellClick(null, null);
            }
        }

        private void DataGridViewGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewGV.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = DataGridViewGV.SelectedRows[0];

            cboMaGV.Text = row.Cells["MaGV"].Value?.ToString();
            cboTenGV.Text = row.Cells["HoTen"].Value?.ToString();

            cboDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();

            cboEmail.Text = row.Cells["Email"].Value?.ToString();

            mskSoDienThoai.Text = row.Cells["SoDT"].Value?.ToString();

            cboKhoa2.SelectedValue = row.Cells["MaKhoa"].Value?.ToString();

            bool gioiTinh =
                Convert.ToBoolean(row.Cells["GioiTinh"].Value);

            if (gioiTinh)
                rdoNam.Checked = true;
            else
                rdoNu.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            // Xóa trắng dữ liệu
            cboMaGV.Text = "";
            cboTenGV.Text = "";
            cboDiaChi.Text = "";
            cboEmail.Text = "";
            mskSoDienThoai.Text = "";

            rdoNam.Checked = true;

            cboKhoa2.SelectedIndex = -1;

            // Hiện ô mật khẩu khi thêm
            cboMatKhau.Visible = true;
            cboMatKhau1.Visible = true;

            lblMatKhau.Visible = true;
            lblNhapLaiMK.Visible = true;

            cboMatKhau.Text = "";
            cboMatKhau1.Text = "";

            // Mở khóa nhập liệu
            cboMaGV.Enabled = true;
            cboTenGV.Enabled = true;
            cboDiaChi.Enabled = true;
            cboEmail.Enabled = true;
            mskSoDienThoai.Enabled = true;

            rdoNam.Enabled = true;
            rdoNu.Enabled = true;

            cboKhoa2.Enabled = true;

            cboMatKhau.Enabled = true;
            cboMatKhau1.Enabled = true;

            // Trạng thái nút
            btnThem.Enabled = false;
            btnSua.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboMaGV.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearDetailInputs();
            ResetFormState();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            {
                // =========================
                // Nếu đang THÊM
                // =========================
                if (isAdding)
                {
                    cboMaGV.Text = "";
                    cboTenGV.Text = "";
                    cboDiaChi.Text = "";
                    cboEmail.Text = "";

                    mskSoDienThoai.Text = "";

                    rdoNam.Checked = true;

                    cboKhoa2.SelectedIndex = -1;

                    cboMatKhau.Text = "";
                    cboMatKhau1.Text = "";

                    cboMaGV.Focus();
                }

                // =========================
                // Nếu đang SỬA
                // =========================
                else if (isEditing)
                {
                    if (DataGridViewGV.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = DataGridViewGV.SelectedRows[0];

                        cboMaGV.Text =
                            row.Cells["MaGV"].Value?.ToString() ?? "";

                        cboTenGV.Text =
                            row.Cells["HoTen"].Value?.ToString() ?? "";

                        cboDiaChi.Text =
                            row.Cells["DiaChi"].Value?.ToString() ?? "";

                        cboEmail.Text =
                            row.Cells["Email"].Value?.ToString() ?? "";

                        mskSoDienThoai.Text =
                            row.Cells["SoDT"].Value?.ToString() ?? "";

                        bool gioiTinh =
                            Convert.ToBoolean(row.Cells["GioiTinh"].Value);

                        if (gioiTinh)
                            rdoNam.Checked = true;
                        else
                            rdoNu.Checked = true;

                        string maKhoa =
                            row.Cells["MaKhoa"].Value?.ToString().Trim() ?? "";

                        if (!string.IsNullOrEmpty(maKhoa))
                            cboKhoa2.SelectedValue = maKhoa;
                        else
                            cboKhoa2.SelectedIndex = -1;
                    }
                }
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            // Tạm ngắt sự kiện filter
            cboKhoa.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;

            // Reset tìm kiếm
            txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên ...";
            txtTimKiem.ForeColor = Color.Gray;

            // Reset combobox khoa
            cboKhoa.SelectedIndex = 0;

            // Gắn lại sự kiện
            cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // Load lại dữ liệu
            LoadData();

            // Reset form
            ClearDetailInputs();

            ResetFormState();
        }
        private void ApplyFilter()
        {
            if (dtGiangVien == null)
                return;

            string filter = "1=1";

            // =========================
            // Lọc theo khoa
            // =========================
            if (cboKhoa.SelectedValue != null &&
                cboKhoa.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaKhoa = '{cboKhoa.SelectedValue}'";
            }

            // =========================
            // Tìm kiếm mã + tên GV
            // =========================
            string kw = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(kw) &&
                kw != "Tìm kiếm theo mã, tên giảng viên ...")
            {
                string escapedKw = kw.Replace("'", "''");

                filter +=
                    $" AND (MaGV LIKE '%{escapedKw}%' " +
                    $"OR HoTen LIKE '%{escapedKw}%')";
            }

            // =========================
            // Áp dụng filter
            // =========================
            dtGiangVien.DefaultView.RowFilter = filter;

            // =========================
            // Nếu không có dữ liệu
            // =========================
            if (DataGridViewGV.Rows.Count == 0)
            {
                ClearDetailInputs();
            }
            else
            {
                DataGridViewGV.ClearSelection();

                DataGridViewGV.CurrentCell =
                    DataGridViewGV.Rows[0].Cells["MaGV"];

                DataGridViewGV.Rows[0].Selected = true;

                DataGridViewGV_CellClick(null, null);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maGV = cboMaGV.Text.Trim();
            string hoTen = cboTenGV.Text.Trim();
            string diaChi = cboDiaChi.Text.Trim();
            string email = cboEmail.Text.Trim();
            string soDT = mskSoDienThoai.Text
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(" ", "")
                .Trim();

            bool gioiTinh = rdoNam.Checked;

            // Validate dữ liệu
            if (string.IsNullOrEmpty(maGV))
            {
                MessageBox.Show("Mã giảng viên không được để trống.");
                cboMaGV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Tên giảng viên không được để trống.");
                cboTenGV.Focus();
                return;
            }

            if (cboKhoa2.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa.");
                cboKhoa2.Focus();
                return;
            }

            string maKhoa = cboKhoa2.SelectedValue.ToString();

            try
            {
                // =====================================
                // THÊM
                // =====================================
                if (isAdding)
                {
                    string matKhau = cboMatKhau.Text.Trim();
                    string nhapLaiMK = cboMatKhau1.Text.Trim();

                    // Kiểm tra mật khẩu
                    if (string.IsNullOrEmpty(matKhau))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu.");
                        cboMatKhau.Focus();
                        return;
                    }

                    if (matKhau != nhapLaiMK)
                    {
                        MessageBox.Show("Mật khẩu nhập lại không khớp.");
                        cboMatKhau1.Focus();
                        return;
                    }

                    // Kiểm tra mã GV tồn tại
                    if (bll.CheckKeyExist(maGV))
                    {
                        MessageBox.Show(
                            "Mã giảng viên đã tồn tại.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        cboMaGV.Focus();
                        return;
                    }

                    // ==========================
                    // Tạo mã tài khoản
                    // ==========================

                    string maTK = bll.TaoMaTaiKhoanMoi();

                    // ==========================
                    // Insert bảng TaiKhoan
                    // ==========================

                    // Hash mật khẩu trước khi lưu vào Database
                    string hashedMatKhau = PasswordHelper.HashPassword(matKhau);

                    bll.InsertTaiKhoan(
                        maTK,
                        "VT002",
                        maGV,
                        hashedMatKhau,
                        1
                    );

                    // ==========================
                    // Insert bảng GiangVien
                    // ==========================

                    bll.InsertGiangVien(
                        soDT,
                        hoTen,
                        gioiTinh,
                        diaChi,
                        maGV,
                        email,
                        maKhoa,
                        maTK
                    );

                    MessageBox.Show(
                        "Thêm giảng viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // =====================================
                // SỬA
                // =====================================
                else if (isEditing)
                {
                    bll.UpdateGiangVien(
                        soDT,
                        hoTen,
                        gioiTinh,
                        diaChi,
                        maGV,
                        email,
                        maKhoa
                    );

                    MessageBox.Show(
                        "Cập nhật giảng viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // =====================================
                // Reload dữ liệu
                // =====================================

                LoadData();

                ResetFormState();

                // Ẩn password
                cboMatKhau.Visible = false;
                cboMatKhau1.Visible = false;

                lblMatKhau.Visible = false;
                lblNhapLaiMK.Visible = false;

                // Chọn lại dòng vừa lưu
                foreach (DataGridViewRow row in DataGridViewGV.Rows)
                {
                    if (row.Cells["MaGV"].Value?.ToString().Trim() == maGV)
                    {
                        row.Selected = true;

                        DataGridViewGV.CurrentCell = row.Cells["MaGV"];

                        DataGridViewGV_CellClick(null, null);

                        break;
                    }
                }
                if (!string.IsNullOrEmpty(email) && !email.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    cboEmail.Focus();
                    return;
                }
                if (soDT.Length < 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.");
                    mskSoDienThoai.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lưu dữ liệu: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DataGridViewGV.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn giảng viên cần chỉnh sửa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            isAdding = false;
            isEditing = true;

            // Không cho sửa mã giảng viên
            cboMaGV.Enabled = false;

            // Cho phép sửa thông tin
            cboTenGV.Enabled = true;
            cboDiaChi.Enabled = true;
            cboEmail.Enabled = true;
            mskSoDienThoai.Enabled = true;

            rdoNam.Enabled = true;
            rdoNu.Enabled = true;

            cboKhoa2.Enabled = true;

            // Ẩn phần mật khẩu khi sửa
            cboMatKhau.Visible = false;
            cboMatKhau1.Visible = false;

            lblMatKhau.Visible = false;
            lblNhapLaiMK.Visible = false;

            cboMatKhau.Enabled = false;
            cboMatKhau1.Enabled = false;

            // Trạng thái nút
            btnThem.Enabled = false;
            btnSua.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboTenGV.Focus();
        }
    
        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void ClearDetailInputs()
        {
            cboMaGV.Text = "";
            cboTenGV.Text = "";
            cboDiaChi.Text = "";
            cboEmail.Text = "";

            mskSoDienThoai.Text = "";

            rdoNam.Checked = true;

            cboKhoa2.SelectedIndex = -1;

            cboMatKhau.Text = "";
            cboMatKhau1.Text = "";
        }
        // 14. Thiết lập sự kiện điều hướng Sidebar

        private void OpenForm(Form targetForm)
        {
            if (targetForm != null)
            {
                targetForm.FormClosed += (s, ev) =>
                {
                    this.Show();

                    // Load lại dữ liệu giảng viên
                    LoadData();
                };

                targetForm.Show();

                this.Hide();
            }
        }

        private void WireSidebarEvents()
        {
            // =========================
            // Điều hướng các Form
            // =========================

            // Đang ở form Giảng viên
            btnGiangvien.Click += (s, e) => { };

            btnCanhbao.Click +=
                (s, e) => OpenForm(new frmCanhBaoHocVu());

            btnLopnc.Click +=
                (s, e) => OpenForm(new FrmLopNienChe());

            btnSinhvien.Click +=
                (s, e) => OpenForm(new frmQuanLiThongTinSinhVien());

            btnMon.Click +=
                (s, e) => OpenForm(new frmMonhoc());

            btnLophp.Click +=
                (s, e) => OpenForm(new frmLophocphan());

            btnKetqua.Click +=
                (s, e) => OpenForm(new frmTheoDoiDiem());

            btnPhuckhao.Click += (s, e) =>
            {
                if (SessionHelper.MaVaiTro == "VT001")
                    OpenForm(new QLDSV.GUI.Forms.Admin.frmPhucKhao_Admin());
                else if (SessionHelper.MaVaiTro == "VT002")
                    OpenForm(new QLDSV.GUI.Forms.GiangVien.frmPhucKhao_GV());
                else if (SessionHelper.MaVaiTro == "VT003")
                    OpenForm(new QLDSV.GUI.Forms.SinhVien.frmPhucKhao_SV());
            };

            btnBaocao.Click +=
                (s, e) => OpenForm(new frmBaoCaoThongKe());

            btnTongquan.Click +=
                (s, e) => OpenForm(new frmTongQuan());

            // =========================
            // Chức năng đang phát triển
            // =========================

            btnDangky.Click += (s, e) =>
                MessageBox.Show(
                    "Tính năng Đăng ký lớp đang được phát triển!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            btnDiem.Click += (s, e) =>
                MessageBox.Show(
                    "Tính năng Nhập điểm đang được phát triển!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            // =========================
            // Highlight menu Giảng viên
            // =========================

            btnGiangvien.FillColor =
                Color.FromArgb(224, 224, 224);

            btnGiangvien.ForeColor = Color.Black;

            btnGiangvien.Font =
                new Font(btnGiangvien.Font, FontStyle.Bold);
        }

        private void cboKhoa2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
