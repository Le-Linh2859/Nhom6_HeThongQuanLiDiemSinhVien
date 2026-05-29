using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLDSV.BLL;

namespace QLDSV.GUI.Forms
{
    public class frmThongTinTaiKhoan : Form, IShellChildForm
    {
        // ─── Controls Giao Diện ──────────────────────────────────────────────
        private Guna2ShadowPanel pnlProfileCard;
        private Guna2CirclePictureBox pbAvatar;
        private Label lblFullName;
        private Label lblRole;

        private Guna2ShadowPanel pnlDetailsCard;
        private Guna2GroupBox gbPersonal;
        private TableLayoutPanel tlpPersonal;

        private Guna2GroupBox gbAccount;
        private TableLayoutPanel tlpAccount;

        // Các textbox nhập liệu chung
        private Guna2TextBox txtMaSo;
        private Guna2TextBox txtHoTen;
        private Guna2TextBox txtGioiTinh;
        private Guna2TextBox txtKhoaLop;
        private Guna2TextBox txtNienKhoa;
        private Guna2TextBox txtNgaySinh;
        private Guna2TextBox txtSdt;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtDiachi;

        private Guna2TextBox txtTenDangNhap;
        private Guna2TextBox txtMatKhauCu;
        private Guna2TextBox txtMatKhauMoi;
        private Guna2TextBox txtXacNhanMatKhau;

        private Guna2Button btnUpdate;
        private Guna2Button btnCancel;

        public frmThongTinTaiKhoan()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            
            this.Load += FrmThongTinTaiKhoan_Load;
            this.Resize += (s, e) => AdjustResponsiveLayout();
        }

        public void OnEmbeddedInShell()
        {
            // Tự động ẩn các thành phần lặp
            string[] duplicateControls = {
                "pnlSidebar", "pnlHeader", "guna2ImageButton1",
                "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13",
                "guna2HtmlLabel14", "guna2ImageButton3"
            };
            foreach (var name in duplicateControls)
            {
                var found = this.Controls.Find(name, true);
                foreach (var c in found) c.Visible = false;
            }

            int shiftX = 0;
            var sidebar = this.Controls.Find("pnlSidebar", true);
            if (sidebar.Length > 0) shiftX = sidebar[0].Width;

            if (shiftX > 0)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl != pnlProfileCard && ctrl != pnlDetailsCard && ctrl.Left > 0)
                        ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
                }
            }

            AdjustResponsiveLayout();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  THIẾT KẾ GIAO DIỆN PROGRAMMATICALLY (WINFORMS CODE-BEHIND)
        // ═══════════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.Text = "THÔNG TIN TÀI KHOẢN";
            this.Size = new Size(950, 650);
            this.MinimumSize = new Size(800, 550);
            this.BackColor = Color.FromArgb(245, 246, 250);

            // 1. Cột bên trái: Profile Card
            pnlProfileCard = new Guna2ShadowPanel();
            pnlProfileCard.FillColor = Color.White;
            pnlProfileCard.Radius = 10;
            pnlProfileCard.ShadowColor = Color.FromArgb(100, 0, 0, 0);
            pnlProfileCard.ShadowStyle = Guna2ShadowPanel.ShadowMode.Dropped;
            pnlProfileCard.Size = new Size(240, 560);
            pnlProfileCard.Location = new Point(15, 15);
            this.Controls.Add(pnlProfileCard);

            pbAvatar = new Guna2CirclePictureBox();
            pbAvatar.Size = new Size(120, 120);
            pbAvatar.Location = new Point(60, 40);
            pbAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            pbAvatar.Image = SystemIcons.Shield.ToBitmap(); // Mặc định dùng biểu tượng lá chắn đẹp
            pbAvatar.BackColor = Color.Transparent;
            pnlProfileCard.Controls.Add(pbAvatar);

            lblFullName = new Label();
            lblFullName.Text = "Họ và Tên";
            lblFullName.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblFullName.ForeColor = Color.FromArgb(44, 62, 80);
            lblFullName.TextAlign = ContentAlignment.MiddleCenter;
            lblFullName.Size = new Size(220, 30);
            lblFullName.Location = new Point(10, 180);
            pnlProfileCard.Controls.Add(lblFullName);

            lblRole = new Label();
            lblRole.Text = "Vai trò";
            lblRole.Font = new Font("Segoe UI", 9.5f, FontStyle.Italic);
            lblRole.ForeColor = Color.FromArgb(100, 88, 255);
            lblRole.TextAlign = ContentAlignment.MiddleCenter;
            lblRole.Size = new Size(220, 20);
            lblRole.Location = new Point(10, 215);
            pnlProfileCard.Controls.Add(lblRole);

            // 2. Cột bên phải: Details Card
            pnlDetailsCard = new Guna2ShadowPanel();
            pnlDetailsCard.FillColor = Color.White;
            pnlDetailsCard.Radius = 10;
            pnlDetailsCard.ShadowColor = Color.FromArgb(100, 0, 0, 0);
            pnlDetailsCard.ShadowStyle = Guna2ShadowPanel.ShadowMode.Dropped;
            pnlDetailsCard.Location = new Point(270, 15);
            pnlDetailsCard.Size = new Size(650, 560);
            this.Controls.Add(pnlDetailsCard);

            // 2.1 GroupBox 1: Thông tin cá nhân
            gbPersonal = new Guna2GroupBox();
            gbPersonal.Text = "THÔNG TIN CÁ NHÂN";
            gbPersonal.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gbPersonal.ForeColor = Color.FromArgb(21, 101, 192);
            gbPersonal.BorderColor = Color.FromArgb(230, 230, 230);
            gbPersonal.Location = new Point(15, 15);
            gbPersonal.Size = new Size(620, 280);
            pnlDetailsCard.Controls.Add(gbPersonal);

            tlpPersonal = new TableLayoutPanel();
            tlpPersonal.ColumnCount = 4;
            tlpPersonal.RowCount = 5;
            tlpPersonal.Dock = DockStyle.Fill;
            tlpPersonal.Padding = new Padding(10, 45, 10, 10);
            tlpPersonal.BackColor = Color.White;
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            gbPersonal.Controls.Add(tlpPersonal);

            // Khởi tạo các Textbox với giao diện cực kỳ premium
            txtMaSo = CreateStyledTextBox(true);
            txtHoTen = CreateStyledTextBox(false);
            txtGioiTinh = CreateStyledTextBox(true);
            txtKhoaLop = CreateStyledTextBox(true);
            txtNienKhoa = CreateStyledTextBox(true);
            txtNgaySinh = CreateStyledTextBox(true);
            txtSdt = CreateStyledTextBox(false);
            txtEmail = CreateStyledTextBox(false);
            txtDiachi = CreateStyledTextBox(false);

            // 2.2 GroupBox 2: Tài khoản & Bảo mật
            gbAccount = new Guna2GroupBox();
            gbAccount.Text = "BẢO MẬT & ĐỔI MẬT KHẨU";
            gbAccount.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            gbAccount.ForeColor = Color.FromArgb(21, 101, 192);
            gbAccount.BorderColor = Color.FromArgb(230, 230, 230);
            gbAccount.Location = new Point(15, 305);
            gbAccount.Size = new Size(620, 185);
            pnlDetailsCard.Controls.Add(gbAccount);

            tlpAccount = new TableLayoutPanel();
            tlpAccount.ColumnCount = 4;
            tlpAccount.RowCount = 2;
            tlpAccount.Dock = DockStyle.Fill;
            tlpAccount.Padding = new Padding(10, 45, 10, 10);
            tlpAccount.BackColor = Color.White;
            tlpAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlpAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tlpAccount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            gbAccount.Controls.Add(tlpAccount);

            txtTenDangNhap = CreateStyledTextBox(false); // Cho phép thay đổi tên đăng nhập
            txtMatKhauCu = CreateStyledTextBox(false);
            txtMatKhauCu.PasswordChar = '●';
            txtMatKhauCu.PlaceholderText = "Nhập mật khẩu hiện tại";

            txtMatKhauMoi = CreateStyledTextBox(false);
            txtMatKhauMoi.PasswordChar = '●';
            txtMatKhauMoi.PlaceholderText = "Mật khẩu mới (bỏ trống nếu không đổi)";

            txtXacNhanMatKhau = CreateStyledTextBox(false);
            txtXacNhanMatKhau.PasswordChar = '●';
            txtXacNhanMatKhau.PlaceholderText = "Xác nhận mật khẩu mới";

            // Add fields to Account layout
            AddLabelToGrid(tlpAccount, "Tên Đăng Nhập", 0, 0);
            tlpAccount.Controls.Add(txtTenDangNhap, 1, 0);

            AddLabelToGrid(tlpAccount, "Mật Khẩu Cũ *", 2, 0);
            tlpAccount.Controls.Add(txtMatKhauCu, 3, 0);

            AddLabelToGrid(tlpAccount, "Mật Khẩu Mới", 0, 1);
            tlpAccount.Controls.Add(txtMatKhauMoi, 1, 1);

            AddLabelToGrid(tlpAccount, "Xác Nhận MK", 2, 1);
            tlpAccount.Controls.Add(txtXacNhanMatKhau, 3, 1);

            // 2.3 Action Buttons
            btnUpdate = new Guna2Button();
            btnUpdate.Text = "Lưu Cập Nhật";
            btnUpdate.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnUpdate.FillColor = Color.FromArgb(21, 101, 192);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.BorderRadius = 5;
            btnUpdate.Size = new Size(130, 36);
            btnUpdate.Location = new Point(505, 505);
            btnUpdate.Click += BtnUpdate_Click;
            pnlDetailsCard.Controls.Add(btnUpdate);

            btnCancel = new Guna2Button();
            btnCancel.Text = "Đặt Lại";
            btnCancel.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnCancel.FillColor = Color.FromArgb(231, 76, 60);
            btnCancel.ForeColor = Color.White;
            btnCancel.BorderRadius = 5;
            btnCancel.Size = new Size(100, 36);
            btnCancel.Location = new Point(395, 505);
            btnCancel.Click += BtnCancel_Click;
            pnlDetailsCard.Controls.Add(btnCancel);
        }

        private Guna2TextBox CreateStyledTextBox(bool isReadOnly)
        {
            var txt = new Guna2TextBox();
            txt.Font = new Font("Segoe UI", 9f, isReadOnly ? FontStyle.Italic : FontStyle.Regular);
            txt.ForeColor = isReadOnly ? Color.FromArgb(127, 140, 141) : Color.FromArgb(44, 62, 80);
            txt.BorderColor = Color.FromArgb(204, 204, 204);
            txt.FocusedState.BorderColor = Color.FromArgb(21, 101, 192);
            txt.ReadOnly = isReadOnly;
            if (isReadOnly) txt.FillColor = Color.FromArgb(245, 245, 245);
            txt.Height = 28;
            txt.Margin = new Padding(3, 3, 3, 8);
            txt.Dock = DockStyle.Fill;
            return txt;
        }

        private void AddLabelToGrid(TableLayoutPanel grid, string text, int col, int row)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            lbl.ForeColor = Color.FromArgb(100, 110, 120);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            grid.Controls.Add(lbl, col, row);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  DỰNG LAYOUT TƯƠNG THÍCH (RESPONSIVE RESIZING)
        // ═══════════════════════════════════════════════════════════════════════
        private void AdjustResponsiveLayout()
        {
            try
            {
                int cardHeight = this.ClientSize.Height - 30;
                pnlProfileCard.Height = cardHeight;
                pnlDetailsCard.Height = cardHeight;

                int detailsWidth = this.ClientSize.Width - pnlProfileCard.Width - 45;
                pnlDetailsCard.Width = detailsWidth;

                // Căn lại kích thước GroupBox con
                gbPersonal.Width = pnlDetailsCard.Width - 30;
                gbAccount.Width = pnlDetailsCard.Width - 30;

                // Căn lại vị trí nút Update/Cancel ở góc dưới bên phải của pnlDetailsCard
                btnUpdate.Location = new Point(pnlDetailsCard.Width - btnUpdate.Width - 15, pnlDetailsCard.Height - btnUpdate.Height - 15);
                btnCancel.Location = new Point(btnUpdate.Left - btnCancel.Width - 8, pnlDetailsCard.Height - btnCancel.Height - 15);
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  TẢI DỮ LIỆU ĐỘNG THEO TỪNG VAI TRÒ (LOAD PROFILE DATA)
        // ═══════════════════════════════════════════════════════════════════════
        private void FrmThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                FunctionQa.ketnoi();
                string maTaiKhoan = SessionHelper.MaTaiKhoan;
                string role = SessionHelper.MaVaiTro;

                // Reset các trường mật khẩu cũ
                txtMatKhauCu.Text = "";
                txtMatKhauMoi.Text = "";
                txtXacNhanMatKhau.Text = "";

                if (role == "VT001") // ADMIN
                {
                    lblRole.Text = "Quản trị viên hệ thống";
                    
                    // Khóa toàn bộ GroupBox 1 thông tin cá nhân vì Admin không có bảng riêng
                    gbPersonal.Enabled = false;
                    gbPersonal.Text = "THÔNG TIN CÁ NHÂN (Không khả dụng đối với Admin)";
                    
                    // Lấy thông tin tài khoản
                    DataTable dt = FunctionQa.getdatatotable($"SELECT MaTaiKhoan, TenDangNhap FROM TaiKhoan WHERE MaTaiKhoan = '{maTaiKhoan}'");
                    if (dt.Rows.Count > 0)
                    {
                        lblFullName.Text = "Administrator";
                        txtTenDangNhap.Text = dt.Rows[0]["TenDangNhap"].ToString();
                    }
                }
                else if (role == "VT002") // GIẢNG VIÊN
                {
                    lblRole.Text = "Giảng viên";
                    SetupPersonalLayoutForGiangVien();

                    string sql = $@"
                        SELECT gv.MaGV, gv.HoTen, gv.GioiTinh, gv.DiaChi, gv.Email, gv.SoDT, k.TenKhoa, tk.TenDangNhap 
                        FROM GiangVien gv 
                        INNER JOIN TaiKhoan tk ON gv.MaTaiKhoan = tk.MaTaiKhoan 
                        LEFT JOIN Khoa k ON gv.MaKhoa = k.MaKhoa 
                        WHERE gv.MaTaiKhoan = '{maTaiKhoan}'";
                    
                    DataTable dt = FunctionQa.getdatatotable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        lblFullName.Text = row["HoTen"].ToString();

                        txtMaSo.Text = row["MaGV"].ToString();
                        txtHoTen.Text = row["HoTen"].ToString();
                        txtGioiTinh.Text = Convert.ToBoolean(row["GioiTinh"]) ? "Nam" : "Nữ";
                        txtKhoaLop.Text = row["TenKhoa"].ToString();
                        txtSdt.Text = row["SoDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtDiachi.Text = row["DiaChi"].ToString();

                        txtTenDangNhap.Text = row["TenDangNhap"].ToString();
                    }
                }
                else if (role == "VT003") // SINH VIÊN
                {
                    lblRole.Text = "Sinh viên";
                    SetupPersonalLayoutForSinhVien();

                    string sql = $@"
                        SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.DiaChi, sv.SoDT, sv.Email, sv.NienKhoa, lnc.TenLop, k.TenKhoa, tk.TenDangNhap 
                        FROM SinhVien sv 
                        INNER JOIN TaiKhoan tk ON sv.MaTaiKhoan = tk.MaTaiKhoan 
                        LEFT JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe 
                        LEFT JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa 
                        WHERE sv.MaTaiKhoan = '{maTaiKhoan}'";

                    DataTable dt = FunctionQa.getdatatotable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        lblFullName.Text = row["HoTen"].ToString();

                        txtMaSo.Text = row["MaSV"].ToString();
                        txtHoTen.Text = row["HoTen"].ToString();
                        txtGioiTinh.Text = Convert.ToBoolean(row["GioiTinh"]) ? "Nam" : "Nữ";
                        txtKhoaLop.Text = row["TenLop"].ToString();
                        txtNienKhoa.Text = row["NienKhoa"].ToString();
                        txtNgaySinh.Text = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") : "";
                        txtSdt.Text = row["SoDT"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtDiachi.Text = row["DiaChi"].ToString();

                        txtTenDangNhap.Text = row["TenDangNhap"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  THIẾT LẬP DÒNG CỘT CỦA CÁC ĐỐI TƯỢNG (DYNAMIC PERSONAL SCHEMAS)
        // ═══════════════════════════════════════════════════════════════════════
        private void SetupPersonalLayoutForGiangVien()
        {
            tlpPersonal.Controls.Clear();
            tlpPersonal.RowCount = 4;
            
            AddLabelToGrid(tlpPersonal, "Mã Giảng Viên", 0, 0);
            tlpPersonal.Controls.Add(txtMaSo, 1, 0);

            AddLabelToGrid(tlpPersonal, "Họ và Tên", 2, 0);
            tlpPersonal.Controls.Add(txtHoTen, 3, 0);

            AddLabelToGrid(tlpPersonal, "Giới Tính", 0, 1);
            tlpPersonal.Controls.Add(txtGioiTinh, 1, 1);

            AddLabelToGrid(tlpPersonal, "Khoa", 2, 1);
            tlpPersonal.Controls.Add(txtKhoaLop, 3, 1);

            AddLabelToGrid(tlpPersonal, "Số Điện Thoại", 0, 2);
            tlpPersonal.Controls.Add(txtSdt, 1, 2);

            AddLabelToGrid(tlpPersonal, "Email", 2, 2);
            tlpPersonal.Controls.Add(txtEmail, 3, 2);

            AddLabelToGrid(tlpPersonal, "Địa Chỉ", 0, 3);
            tlpPersonal.Controls.Add(txtDiachi, 1, 3);
            tlpPersonal.SetColumnSpan(txtDiachi, 3);
        }

        private void SetupPersonalLayoutForSinhVien()
        {
            tlpPersonal.Controls.Clear();
            tlpPersonal.RowCount = 5;

            AddLabelToGrid(tlpPersonal, "Mã Sinh Viên", 0, 0);
            tlpPersonal.Controls.Add(txtMaSo, 1, 0);

            AddLabelToGrid(tlpPersonal, "Họ và Tên", 2, 0);
            tlpPersonal.Controls.Add(txtHoTen, 3, 0);

            AddLabelToGrid(tlpPersonal, "Ngày Sinh", 0, 1);
            tlpPersonal.Controls.Add(txtNgaySinh, 1, 1);

            AddLabelToGrid(tlpPersonal, "Giới Tính", 2, 1);
            tlpPersonal.Controls.Add(txtGioiTinh, 3, 1);

            AddLabelToGrid(tlpPersonal, "Lớp Niên Chế", 0, 2);
            tlpPersonal.Controls.Add(txtKhoaLop, 1, 2);

            AddLabelToGrid(tlpPersonal, "Niên Khóa", 2, 2);
            tlpPersonal.Controls.Add(txtNienKhoa, 3, 2);

            AddLabelToGrid(tlpPersonal, "Số Điện Thoại", 0, 3);
            tlpPersonal.Controls.Add(txtSdt, 1, 3);

            AddLabelToGrid(tlpPersonal, "Email", 2, 3);
            tlpPersonal.Controls.Add(txtEmail, 3, 3);

            AddLabelToGrid(tlpPersonal, "Địa Chỉ", 0, 4);
            tlpPersonal.Controls.Add(txtDiachi, 1, 4);
            tlpPersonal.SetColumnSpan(txtDiachi, 3);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  XỬ LÝ CẬP NHẬT TÀI KHOẢN & BẢO MẬT (CREDENTIAL EDIT LOGIC)
        // ═══════════════════════════════════════════════════════════════════════
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string maTaiKhoan = SessionHelper.MaTaiKhoan;
                string role = SessionHelper.MaVaiTro;

                // 1. Kiểm tra Mật khẩu cũ bắt buộc để xác thực bảo mật
                if (string.IsNullOrEmpty(txtMatKhauCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập 'Mật Khẩu Cũ' để xác nhận thay đổi thông tin!", "Yêu cầu xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                // 2. Kiểm tra mật khẩu cũ sử dụng BCrypt.Verify (an toàn)
                DataTable dtCurrent = FunctionQa.getdatatotable($"SELECT MatKhau FROM TaiKhoan WHERE MaTaiKhoan = '{maTaiKhoan}'");
                if (dtCurrent.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string storedHash = dtCurrent.Rows[0]["MatKhau"].ToString();
                if (!PasswordHelper.VerifyPassword(txtMatKhauCu.Text.Trim(), storedHash))
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác! Vui lòng kiểm tra lại.", "Xác thực thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Focus();
                    return;
                }

                // 3. Nếu thay đổi mật khẩu mới, kiểm tra tính hợp lệ
                string matKhauMoi = txtMatKhauMoi.Text.Trim();
                if (!string.IsNullOrEmpty(matKhauMoi))
                {
                    if (matKhauMoi != txtXacNhanMatKhau.Text.Trim())
                    {
                        MessageBox.Show("Mật khẩu mới và Nhập lại mật khẩu mới không khớp với nhau!", "Cảnh báo mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtXacNhanMatKhau.Focus();
                        return;
                    }
                    if (matKhauMoi.Length < 6)
                    {
                        MessageBox.Show("Mật khẩu mới phải từ 6 ký tự trở lên để đảm bảo an toàn!", "Mật khẩu quá ngắn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMatKhauMoi.Focus();
                        return;
                    }
                }

                // 4. Kiểm tra trùng lặp Tên đăng nhập mới
                string tenDangNhapMoi = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrEmpty(tenDangNhapMoi))
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                DataTable dtDup = FunctionQa.getdatatotable($"SELECT 1 FROM TaiKhoan WHERE TenDangNhap = '{tenDangNhapMoi}' AND MaTaiKhoan != '{maTaiKhoan}'");
                if (dtDup.Rows.Count > 0)
                {
                    MessageBox.Show("Tên đăng nhập này đã được sử dụng bởi tài khoản khác! Vui lòng chọn tên đăng nhập khác.", "Trùng tên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                // 5. Thực hiện cập nhật Transaction An Toàn
                using (var conn = new SqlConnection(FunctionQa.connstring))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 5.1 Cập nhật bảng TaiKhoan
                            string sqlTaiKhoan = @"
                                UPDATE TaiKhoan 
                                SET TenDangNhap = @TenDangNhap, 
                                    MatKhau = @MatKhau 
                                WHERE MaTaiKhoan = @MaTaiKhoan";

                            using (var cmd = new SqlCommand(sqlTaiKhoan, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhapMoi);

                                // Hash mật khẩu mới hoặc giữ nguyên hash cũ nếu không đổi mật khẩu
                                string matKhauLuu = !string.IsNullOrEmpty(matKhauMoi)
                                    ? PasswordHelper.HashPassword(matKhauMoi)
                                    : storedHash;
                                cmd.Parameters.AddWithValue("@MatKhau", matKhauLuu);
                                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                                cmd.ExecuteNonQuery();
                            }

                            // 5.2 Cập nhật bảng Chi tiết tương ứng (SinhVien / GiangVien)
                            if (role == "VT002") // Giảng viên
                            {
                                string sqlGiangVien = @"
                                    UPDATE GiangVien 
                                    SET HoTen = @HoTen, 
                                        SoDT = @SoDT, 
                                        Email = @Email, 
                                        DiaChi = @DiaChi 
                                    WHERE MaTaiKhoan = @MaTaiKhoan";

                                using (var cmd = new SqlCommand(sqlGiangVien, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                                    cmd.Parameters.AddWithValue("@SoDT", txtSdt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                                    cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (role == "VT003") // Sinh viên
                            {
                                string sqlSinhVien = @"
                                    UPDATE SinhVien 
                                    SET HoTen = @HoTen, 
                                        SoDT = @SoDT, 
                                        Email = @Email, 
                                        DiaChi = @DiaChi 
                                    WHERE MaTaiKhoan = @MaTaiKhoan";

                                using (var cmd = new SqlCommand(sqlSinhVien, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                                    cmd.Parameters.AddWithValue("@SoDT", txtSdt.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                                    cmd.Parameters.AddWithValue("@DiaChi", txtDiachi.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }

                // 6. Cập nhật lại session thành công
                SessionHelper.TenDangNhap = tenDangNhapMoi;

                // Cập nhật lại nhãn tên trên Header của Shell (frmMain)
                Form mainForm = Application.OpenForms["frmMain"];
                if (mainForm != null)
                {
                    var lblHeaderName = mainForm.Controls.Find("guna2HtmlLabel13", true);
                    if (lblHeaderName.Length > 0)
                    {
                        lblHeaderName[0].Text = tenDangNhapMoi;
                    }
                }

                MessageBox.Show("Cập nhật thông tin tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu thông tin: " + ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
