using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QLDSV.BLL;

namespace QLDSV.GUI
{
    public partial class frmQuanLiTaiKhoan : Form, IShellChildForm
    {
        private int currentPage = 1;
        private int pageSize = 4; // Bốn bản ghi một trang để hiển thị vừa vặn chiều cao của grid gốc (109px)
        private int totalPages = 1;
        private DataTable tblTaiKhoan;
        private bool isAddingNew = false;

        public void OnEmbeddedInShell()
        {
            // Ẩn các nút tiêu đề, sidebar và ảnh đại diện khi được nhúng trong Shell chính (frmMain)
            string[] controlNames = { 
                "pnlSidebar", "guna2ImageButton1", "guna2HtmlLabel13", "guna2HtmlLabel14", 
                "guna2CirclePictureBox1", "guna2ImageButton2", "guna2ImageButton3", "label4", "label3" 
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
            var sidebarControls = this.Controls.Find("pnlSidebar", true);
            if (sidebarControls.Length > 0)
            {
                shiftX = sidebarControls[0].Width;
            }

            if (shiftX == 0) return;

            // Dịch chuyển các thành phần điều khiển sang trái để chiếm trọn không gian trống của sidebar cũ
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

            SetAnchorAllGrids(this.Controls);
            MakeLayoutResponsive();
        }

        private void SetAnchorAllGrids(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                }
                if (c.Controls.Count > 0)
                {
                    SetAnchorAllGrids(c.Controls);
                }
            }
        }

        public frmQuanLiTaiKhoan()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);

            // Gán sự kiện load và co dãn giao diện
            this.Load += frmQuanLiTaiKhoan_Load;
            this.Resize += (s, e) => MakeLayoutResponsive();

            // Gán sự kiện click của danh sách tài khoản
            this.dataGridView1.CellClick += dataGridView1_CellClick;

            // Đăng ký sự kiện các nút phân trang
            this.guna2Button9.Click += guna2Button9_Click;
            this.guna2Button11.Click += guna2Button11_Click;

            // Đăng ký sự kiện chức năng CRUD
            this.guna2Button1.Click += guna2Button1_Click; // Thêm tài khoản
            this.guna2Button2.Click += guna2Button2_Click; // Sửa
            this.guna2Button3.Click += guna2Button3_Click; // Lưu
            this.guna2Button4.Click += guna2Button4_Click; // Hủy
            this.guna2Button5.Click += guna2Button5_Click; // Khóa / Mở khóa

            // Gán sự kiện cho các bộ lọc
            this.guna2ComboBox2.SelectedIndexChanged += (s, e) => { currentPage = 1; LoadData(); };
            this.guna2ComboBox1.SelectedIndexChanged += (s, e) => { currentPage = 1; LoadData(); };

            // Trình xử lý Placeholder cho Tìm kiếm
            guna2TextBox1.Text = "Tìm tên, tài khoản...";
            guna2TextBox1.ForeColor = Color.Gray;

            guna2TextBox1.Enter += (s, ev) =>
            {
                if (guna2TextBox1.Text == "Tìm tên, tài khoản...")
                {
                    guna2TextBox1.Text = "";
                    guna2TextBox1.ForeColor = Color.Black;
                }
            };

            guna2TextBox1.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                {
                    guna2TextBox1.Text = "Tìm tên, tài khoản...";
                    guna2TextBox1.ForeColor = Color.Gray;
                }
            };

            guna2TextBox1.TextChanged += (s, ev) =>
            {
                if (guna2TextBox1.Text != "Tìm tên, tài khoản...")
                {
                    currentPage = 1;
                    LoadData();
                }
            };
        }

        private void frmQuanLiTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();

                // Khởi tạo bộ lọc quyền hạn (guna2ComboBox2)
                guna2ComboBox2.Items.Clear();
                guna2ComboBox2.Items.Add("--- Tất cả vai trò ---");
                guna2ComboBox2.Items.Add("Quản trị viên");
                guna2ComboBox2.Items.Add("Giảng viên");
                guna2ComboBox2.Items.Add("Sinh viên");
                guna2ComboBox2.SelectedIndex = 0;

                // Khởi tạo bộ lọc trạng thái & liên kết (guna2ComboBox1)
                guna2ComboBox1.Items.Clear();
                guna2ComboBox1.Items.Add("--- Tất cả trạng thái ---");
                guna2ComboBox1.Items.Add("Hoạt động");
                guna2ComboBox1.Items.Add("Bị khóa");
                guna2ComboBox1.Items.Add("Chưa liên kết hồ sơ");
                guna2ComboBox1.Items.Add("Đã liên kết hồ sơ");
                guna2ComboBox1.SelectedIndex = 0;

                // Khởi tạo các Combobox chi tiết
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Quản trị viên");
                comboBox1.Items.Add("Giảng viên");
                comboBox1.Items.Add("Sinh viên");
                comboBox1.SelectedIndex = -1;

                comboBox4.Items.Clear();
                comboBox4.Items.Add("Hoạt động");
                comboBox4.Items.Add("Bị khóa");
                comboBox4.SelectedIndex = -1;

                // Load danh sách dữ liệu lần đầu
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo màn hình quản lý tài khoản: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Câu lệnh SQL lấy dữ liệu tài khoản kết hợp lấy họ tên người dùng
                string sql = @"
                    SELECT 
                        tk.MaTaiKhoan AS [Mã tài khoản],
                        tk.TenDangNhap AS [Tên đăng nhập],
                        tk.MatKhau AS [Mật khẩu],
                        CASE WHEN tk.TrangThai = 1 THEN N'Hoạt động' ELSE N'Bị khóa' END AS [Trạng thái],
                        vt.TenVaiTro AS [Quyền hạn],
                        CASE 
                            WHEN tk.MaVaiTro = 'VT001' THEN N'Quản trị viên'
                            WHEN tk.MaVaiTro = 'VT002' THEN (SELECT HoTen FROM GiangVien gv WHERE gv.MaTaiKhoan = tk.MaTaiKhoan)
                            WHEN tk.MaVaiTro = 'VT003' THEN (SELECT HoTen FROM SinhVien sv WHERE sv.MaTaiKhoan = tk.MaTaiKhoan)
                            ELSE N'Không xác định'
                        END AS [Họ và tên],
                        tk.MaVaiTro AS _MaVaiTro,
                        tk.TrangThai AS _TrangThai
                    FROM TaiKhoan tk
                    JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    WHERE 1=1";

                // Điều kiện Tìm kiếm từ khóa
                string keyword = guna2TextBox1.Text.Trim();
                if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm tên, tài khoản...")
                {
                    sql += $" AND (tk.TenDangNhap LIKE '%{keyword}%' OR tk.MaTaiKhoan LIKE '%{keyword}%' OR " +
                           $" tk.MaVaiTro = 'VT001' AND N'Quản trị viên' LIKE N'%{keyword}%' OR " +
                           $" tk.MaVaiTro = 'VT002' AND tk.MaTaiKhoan IN (SELECT MaTaiKhoan FROM GiangVien WHERE HoTen LIKE N'%{keyword}%') OR " +
                           $" tk.MaVaiTro = 'VT003' AND tk.MaTaiKhoan IN (SELECT MaTaiKhoan FROM SinhVien WHERE HoTen LIKE N'%{keyword}%'))";
                }

                // Bộ lọc Vai trò
                if (guna2ComboBox2.SelectedIndex > 0)
                {
                    string roleText = guna2ComboBox2.SelectedItem.ToString();
                    if (roleText == "Quản trị viên") sql += " AND tk.MaVaiTro = 'VT001'";
                    else if (roleText == "Giảng viên") sql += " AND tk.MaVaiTro = 'VT002'";
                    else if (roleText == "Sinh viên") sql += " AND tk.MaVaiTro = 'VT003'";
                }

                // Bộ lọc Trạng thái / Tình trạng liên kết
                if (guna2ComboBox1.SelectedIndex > 0)
                {
                    string filterText = guna2ComboBox1.SelectedItem.ToString();
                    if (filterText == "Hoạt động") sql += " AND tk.TrangThai = 1";
                    else if (filterText == "Bị khóa") sql += " AND tk.TrangThai = 0";
                    else if (filterText == "Chưa liên kết hồ sơ")
                    {
                        sql += @" AND (
                            (tk.MaVaiTro = 'VT002' AND tk.MaTaiKhoan NOT IN (SELECT DISTINCT MaTaiKhoan FROM GiangVien WHERE MaTaiKhoan IS NOT NULL))
                            OR
                            (tk.MaVaiTro = 'VT003' AND tk.MaTaiKhoan NOT IN (SELECT DISTINCT MaTaiKhoan FROM SinhVien WHERE MaTaiKhoan IS NOT NULL))
                        )";
                    }
                    else if (filterText == "Đã liên kết hồ sơ")
                    {
                        sql += @" AND (
                            (tk.MaVaiTro = 'VT002' AND tk.MaTaiKhoan IN (SELECT DISTINCT MaTaiKhoan FROM GiangVien))
                            OR
                            (tk.MaVaiTro = 'VT003' AND tk.MaTaiKhoan IN (SELECT DISTINCT MaTaiKhoan FROM SinhVien))
                        )";
                    }
                }

                sql += " ORDER BY tk.MaTaiKhoan ASC";

                tblTaiKhoan = FunctionQa.getdatatotable(sql);

                // Cập nhật số liệu hiển thị trên các Panel Thống kê
                UpdateStats();

                int totalRecords = tblTaiKhoan.Rows.Count;
                if (totalRecords == 0)
                {
                    dataGridView1.DataSource = null;
                    guna2HtmlLabel7.Text = "0/0";
                    return;
                }

                // Tính toán số lượng trang
                totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                if (currentPage > totalPages) currentPage = totalPages;
                if (currentPage < 1) currentPage = 1;

                guna2HtmlLabel7.Text = $"{currentPage}/{totalPages}";

                // Cắt dữ liệu hiển thị theo trang
                DataTable pageTable = tblTaiKhoan.Clone();
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, totalRecords);

                for (int i = startIndex; i < endIndex; i++)
                {
                    pageTable.ImportRow(tblTaiKhoan.Rows[i]);
                }

                dataGridView1.DataSource = pageTable;

                // Cấu hình hiển thị cột cho DataGridView
                if (dataGridView1.Columns.Contains("_MaVaiTro")) dataGridView1.Columns["_MaVaiTro"].Visible = false;
                if (dataGridView1.Columns.Contains("_TrangThai")) dataGridView1.Columns["_TrangThai"].Visible = false;

                if (dataGridView1.Columns.Contains("Mã tài khoản")) dataGridView1.Columns["Mã tài khoản"].Width = 90;
                if (dataGridView1.Columns.Contains("Tên đăng nhập")) dataGridView1.Columns["Tên đăng nhập"].Width = 110;
                if (dataGridView1.Columns.Contains("Mật khẩu")) dataGridView1.Columns["Mật khẩu"].Width = 90;
                if (dataGridView1.Columns.Contains("Trạng thái")) dataGridView1.Columns["Trạng thái"].Width = 90;
                if (dataGridView1.Columns.Contains("Quyền hạn")) dataGridView1.Columns["Quyền hạn"].Width = 100;
                if (dataGridView1.Columns.Contains("Họ và tên")) dataGridView1.Columns["Họ và tên"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Định dạng màu trạng thái
                dataGridView1.CellFormatting -= DataGridView1_CellFormatting;
                dataGridView1.CellFormatting += DataGridView1_CellFormatting;

                dataGridView1.ClearSelection();
                ResetInputs();
                SetEditingState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tài khoản: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Trạng thái" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Hoạt động")
                    e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50); // Xanh lá
                else if (status == "Bị khóa")
                    e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40); // Đỏ
            }
        }

        private void UpdateStats()
        {
            try
            {
                string countAll = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM TaiKhoan");
                string countGV = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM TaiKhoan WHERE MaVaiTro = 'VT002'");
                string countSV = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM TaiKhoan WHERE MaVaiTro = 'VT003'");

                label5.Text = $"TỔNG TÀI KHOẢN\n{countAll}";
                label6.Text = $"GIẢNG VIÊN\n{countGV}";
                label7.Text = $"SINH VIÊN\n{countSV}";
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isAddingNew) return; // Không đổi dòng khi đang thực hiện thêm mới tài khoản

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["Mã tài khoản"].Value?.ToString();
            textBox2.Text = row.Cells["Tên đăng nhập"].Value?.ToString();
            textBox3.Text = row.Cells["Họ và tên"].Value?.ToString();
            textBox4.Text = row.Cells["Mật khẩu"].Value?.ToString();
            textBox5.Text = row.Cells["Mật khẩu"].Value?.ToString();

            string roleText = row.Cells["Quyền hạn"].Value?.ToString();
            comboBox1.SelectedItem = roleText;

            string statusText = row.Cells["Trạng thái"].Value?.ToString();
            comboBox4.SelectedItem = statusText;

            // Cập nhật văn bản nút Khóa/Mở khóa dựa vào trạng thái hiện tại
            if (statusText == "Hoạt động")
            {
                guna2Button5.Text = "Khóa";
                guna2Button5.FillColor = Color.FromArgb(235, 87, 87);
            }
            else
            {
                guna2Button5.Text = "Mở khóa";
                guna2Button5.FillColor = Color.FromArgb(46, 125, 50);
            }

            SetEditingState(false);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ResetInputs();
            SetEditingState(true);

            // Tự sinh mã tài khoản mới TKxxx
            string nextMaTK = GenerateNewMaTaiKhoan();
            textBox1.Text = nextMaTK;
            textBox3.Text = "Chưa liên kết hồ sơ";

            comboBox1.SelectedIndex = 2; // Mặc định là Sinh viên
            comboBox4.SelectedIndex = 0; // Mặc định là Hoạt động

            textBox2.Focus();
        }

        private string GenerateNewMaTaiKhoan()
        {
            try
            {
                string sql = "SELECT MaTaiKhoan FROM TaiKhoan ORDER BY MaTaiKhoan DESC";
                DataTable dt = FunctionQa.getdatatotable(sql);
                if (dt.Rows.Count == 0) return "TK001";

                int maxVal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["MaTaiKhoan"].ToString().Trim();
                    if (id.StartsWith("TK") && int.TryParse(id.Substring(2), out int val))
                    {
                        if (val > maxVal) maxVal = val;
                    }
                }
                return "TK" + (maxVal + 1).ToString("D3");
            }
            catch
            {
                return "TK001";
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản muốn sửa trong danh sách phía trên.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAddingNew = false;
            SetEditingState(true);
            textBox2.Focus();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            isAddingNew = false;
            SetEditingState(false);
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idx = dataGridView1.SelectedRows[0].Index;
                dataGridView1_CellClick(null, new DataGridViewCellEventArgs(0, idx));
            }
            else
            {
                ResetInputs();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string maTK = textBox1.Text.Trim();
            string tenDN = textBox2.Text.Trim();
            string mk = textBox4.Text.Trim();
            string confirmMk = textBox5.Text.Trim();

            if (string.IsNullOrEmpty(tenDN))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đăng nhập.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

            if (mk != confirmMk)
            {
                MessageBox.Show("Mật khẩu xác nhận nhập lại không trùng khớp.", "Mật khẩu sai", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return;
            }

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn quyền hạn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox4.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái tài khoản.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roleCode = "VT003";
            string selectedRole = comboBox1.SelectedItem.ToString();
            if (selectedRole == "Quản trị viên") roleCode = "VT001";
            else if (selectedRole == "Giảng viên") roleCode = "VT002";

            int statusVal = comboBox4.SelectedItem.ToString() == "Hoạt động" ? 1 : 0;

            try
            {
                // Kiểm tra tên đăng nhập có bị trùng lặp với tài khoản khác không
                string dupQuery = $"SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = '{tenDN}' AND MaTaiKhoan != '{maTK}'";
                int countDup = int.Parse(FunctionQa.getfieldvalue(dupQuery));
                if (countDup > 0)
                {
                    MessageBox.Show("Tên đăng nhập này đã được sử dụng bởi một tài khoản khác.", "Trùng lặp tên đăng nhập", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                // Hash mật khẩu bằng BCrypt trước khi lưu vào cơ sở dữ liệu
                string hashedMk = PasswordHelper.HashPassword(mk);

                if (isAddingNew)
                {
                    string insertSql = $"INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, TrangThai, MaVaiTro) " +
                                       $"VALUES ('{maTK}', '{tenDN}', '{hashedMk}', {statusVal}, '{roleCode}')";
                    FunctionQa.runsql(insertSql);
                    MessageBox.Show("Thêm mới tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string updateSql = $"UPDATE TaiKhoan SET TenDangNhap = '{tenDN}', MatKhau = '{hashedMk}', " +
                                       $"TrangThai = {statusVal}, MaVaiTro = '{roleCode}' WHERE MaTaiKhoan = '{maTK}'";
                    FunctionQa.runsql(updateSql);
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                isAddingNew = false;
                SetEditingState(false);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string maTK = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(maTK))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần thay đổi trạng thái trong danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = comboBox4.SelectedItem?.ToString();
            int newStatus = currentStatus == "Hoạt động" ? 0 : 1;
            string confirmMsg = currentStatus == "Hoạt động" ? "KHÓA" : "MỞ KHÓA / KÍCH HOẠT";

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn thực hiện {confirmMsg} tài khoản '{maTK}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string sql = $"UPDATE TaiKhoan SET TrangThai = {newStatus} WHERE MaTaiKhoan = '{maTK}'";
                    FunctionQa.runsql(sql);
                    MessageBox.Show($"Thay đổi trạng thái tài khoản thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thay đổi trạng thái tài khoản: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e) // Prev Page
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e) // Next Page
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void SetEditingState(bool isEditing)
        {
            textBox2.ReadOnly = !isEditing; // Tên đăng nhập
            textBox4.ReadOnly = !isEditing; // Mật khẩu
            textBox5.ReadOnly = !isEditing; // Nhập lại mật khẩu
            comboBox1.Enabled = isEditing;  // Vai trò combobox
            comboBox4.Enabled = isEditing;  // Trạng thái combobox

            // Nút Lưu và Hủy chỉ hiển thị ở chế độ sửa
            guna2Button3.Visible = isEditing;
            guna2Button4.Visible = isEditing;

            // Khóa/Kích hoạt các phím CRUD khi ở chế độ tương ứng
            guna2Button1.Enabled = !isEditing;
            guna2Button2.Enabled = !isEditing && !string.IsNullOrEmpty(textBox1.Text);
            guna2Button5.Enabled = !isEditing && !string.IsNullOrEmpty(textBox1.Text);
            dataGridView1.Enabled = !isEditing;
        }

        private void ResetInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }

        private void MakeLayoutResponsive()
        {
            try
            {
                // Căn chỉnh thanh tìm kiếm/lọc tableLayoutPanel2
                if (tableLayoutPanel2 != null)
                {
                    tableLayoutPanel2.Width = this.Width - tableLayoutPanel2.Left - 16;
                }

                // Căn rộng GroupBox Danh sách
                if (guna2GroupBox2 != null)
                {
                    guna2GroupBox2.Width = this.Width - guna2GroupBox2.Left - 16;
                    
                    // Căn chỉnh chiều rộng DataGridView
                    if (dataGridView1 != null)
                    {
                        dataGridView1.Width = guna2GroupBox2.Width - 6;
                    }

                    // Căn chỉnh vị trí lướt trang phân trang sát lề phải Groupbox
                    int rightEdge = guna2GroupBox2.Width;
                    if (guna2Button11 != null) guna2Button11.Left = rightEdge - guna2Button11.Width - 12;
                    if (guna2HtmlLabel7 != null && guna2Button11 != null) guna2HtmlLabel7.Left = guna2Button11.Left - guna2HtmlLabel7.Width - 8;
                    if (guna2Button9 != null && guna2HtmlLabel7 != null) guna2Button9.Left = guna2HtmlLabel7.Left - guna2Button9.Width - 8;
                }

                // Căn rộng GroupBox Chi tiết
                if (guna2GroupBox1 != null)
                {
                    guna2GroupBox1.Width = this.Width - guna2GroupBox1.Left - 16;
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
