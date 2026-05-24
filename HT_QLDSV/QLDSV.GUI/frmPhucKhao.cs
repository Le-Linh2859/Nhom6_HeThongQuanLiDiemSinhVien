//using System;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;

//namespace QLDSV.GUI
//{
//    public partial class frmPhucKhao : Form
//    {
//        private bool detailPanelVisible = false;
//        private bool isEditMode = false;        // false = View, true = Add/Edit
//        private bool isAddingNew = false;       // true = Thêm mới, false = Xem/Sửa
//        private string currentMaPK = "";
//        private DataTable tblPhucKhao;

//        public frmPhucKhao()
//        {
//            InitializeComponent();
//            BuildSidebarMenu();

//            // Wire up event handlers programmatically
//            this.Load += frmPhucKhao_Load;
//            this.btnThem.Click += btnThem_Click;
//            this.btnDuyet.Click += btnDuyet_Click;
//            this.btnTuChoi.Click += btnTuChoi_Click;
//            this.btnXoa.Click += btnXoa_Click;
//            this.btnLamMoi.Click += btnLamMoi_Click;
//            this.btnCloseDetail.Click += btnCloseDetail_Click;
//            this.btnLuuDetail.Click += btnLuuDetail_Click;
//            this.btnHuyDetail.Click += btnHuyDetail_Click;
//            this.dataGridView.CellClick += dataGridView_CellClick;
//            this.cboFilterTrangThai.SelectedIndexChanged += cboFilterTrangThai_SelectedIndexChanged;
//            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

//            // Wire up Logout button in sidebar
//            if (this.guna2Button7 != null)
//            {
//                this.guna2Button7.Click += (s, e) =>
//                {
//                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
//                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//                    if (confirm == DialogResult.Yes)
//                    {
//                        var loginForm = new frmDangNhap();
//                        loginForm.Show();
//                        this.Hide();
//                    }
//                };
//            }

//            // Search text box placeholder styling and search on Enter
//            txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
//            txtTimKiem.ForeColor = Color.Gray;

//            txtTimKiem.Enter += (s, e) =>
//            {
//                if (txtTimKiem.Text == "Tìm tên SV / lớp học phần...")
//                {
//                    txtTimKiem.Text = "";
//                    txtTimKiem.ForeColor = Color.Black;
//                }
//            };

//            txtTimKiem.Leave += (s, e) =>
//            {
//                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
//                {
//                    txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
//                    txtTimKiem.ForeColor = Color.Gray;
//                }
//            };

//            txtTimKiem.KeyDown += (s, e) =>
//            {
//                if (e.KeyCode == Keys.Enter)
//                {
//                    LoadData();
//                    e.Handled = true;
//                    e.SuppressKeyPress = true;
//                }
//            };
//        }

//        private void BuildSidebarMenu()
//        {
//            string[] menuItems = {
//                "Tổng quan", "Giảng viên", "Sinh viên", "Môn học",
//                "Lớp niên chế", "Lớp học phần", "Đăng ký lớp",
//                "Nhập điểm", "Kết quả học tập", "Cảnh báo học vụ",
//                "Phúc khảo", "Báo cáo"
//            };
//            string[] icons = { "📊", "👨‍🏫", "🎓", "📚", "🏛", "📋", "📝", "✏️", "📈", "⚠️", "🔄", "📑" };
            
//            // Filter menu items based on logged-in user's role
//            var visibleItems = new System.Collections.Generic.List<string>();
//            var visibleIcons = new System.Collections.Generic.List<string>();
//            string role = SessionHelper.MaVaiTro;

//            for (int i = 0; i < menuItems.Length; i++)
//            {
//                bool isVisible = true;
//                if (role == "VT002") // Giảng viên
//                {
//                    if (menuItems[i] == "Tổng quan" || menuItems[i] == "Giảng viên" || menuItems[i] == "Môn học" || menuItems[i] == "Lớp niên chế")
//                    {
//                        isVisible = false;
//                    }
//                }
//                else if (role == "VT003") // Sinh viên
//                {
//                    if (menuItems[i] != "Đăng ký lớp" && menuItems[i] != "Kết quả học tập" && menuItems[i] != "Phúc khảo")
//                    {
//                        isVisible = false;
//                    }
//                }

//                if (isVisible)
//                {
//                    visibleItems.Add(menuItems[i]);
//                    visibleIcons.Add(icons[i]);
//                }
//            }

//            int startY = 55;
//            for (int i = 0; i < visibleItems.Count; i++)
//            {
//                var btn = new Guna.UI2.WinForms.Guna2Button();
//                btn.FillColor = System.Drawing.Color.Transparent;
//                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
//                btn.ForeColor = System.Drawing.Color.White;
//                btn.Size = new System.Drawing.Size(180, 32);
//                btn.Location = new System.Drawing.Point(8, startY + i * 34);
//                btn.Text = "  " + visibleIcons[i] + "  " + visibleItems[i];
//                btn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
//                btn.Cursor = System.Windows.Forms.Cursors.Hand;
//                if (visibleItems[i] == "Phúc khảo")
//                {
//                    btn.FillColor = System.Drawing.Color.FromArgb(224, 224, 224);
//                    btn.ForeColor = System.Drawing.Color.Black;
//                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//                    btn.BorderRadius = 5;
//                }

//                // Add click handler for navigation
//                string itemText = visibleItems[i];
//                btn.Click += (s, ev) =>
//                {
//                    Form targetForm = null;
//                    if (itemText == "Tổng quan")
//                    {
//                        targetForm = new frmKhoa();
//                    }
//                    else if (itemText == "Giảng viên")
//                    {
//                        targetForm = new frmGiangVien();
//                    }
//                    else if (itemText == "Môn học")
//                    {
//                        targetForm = new frmMonhoc();
//                    }
//                    else if (itemText == "Sinh viên")
//                    {
//                        targetForm = new frmQuanLiThongTinSinhVien();
//                    }
//                    else if (itemText == "Cảnh báo học vụ")
//                    {
//                        targetForm = new frmCanhBaoHocVu();
//                    }
//                    else if (itemText == "Lớp học phần")
//                    {
//                        targetForm = new frmLophocphan();
//                    }
//                    else if (itemText == "Đăng ký lớp")
//                    {
//                        targetForm = new frmDangKyHocPhan();
//                    }
//                    else if (itemText == "Báo cáo")
//                    {
//                        targetForm = new frmBaoCaoThongKe();
//                    }

//                    if (targetForm != null)
//                    {
//                        targetForm.FormClosed += (senderForm, eArg) =>
//                        {
//                            this.Show();
//                            // Refresh this form state on return
//                            this.LoadData();
//                        };
//                        targetForm.Show();
//                        this.Hide();
//                    }
//                };

//                pnlSidebar.Controls.Add(btn);
//            }
//        }

//        private void frmPhucKhao_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                FunctionQa.ketnoi();

//                // Setup state filter dropdown values
//                cboFilterTrangThai.Items.Clear();
//                cboFilterTrangThai.Items.Add("--- Tất cả ---");
//                cboFilterTrangThai.Items.Add("Chờ duyệt");
//                cboFilterTrangThai.Items.Add("Đã duyệt");
//                cboFilterTrangThai.Items.Add("Từ chối");
//                cboFilterTrangThai.SelectedIndex = 0;

//                LoadComboBoxes();
//                LoadData();
//                UpdateStatistics();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu hoặc khởi tạo form: " + ex.Message,
//                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            pnlDetail.Width = 0;
//            pnlDetail.Visible = false;
//        }

//        private void LoadComboBoxes()
//        {
//            try
//            {
//                string sqlSV = "SELECT MaSV, MaSV + ' - ' + HoTen AS HienThi FROM SinhVien";
//                FunctionQa.fillcombo(sqlSV, cboSinhVien, "MaSV", "HienThi");
//                cboSinhVien.SelectedIndex = -1;

//                string sqlLHP = "SELECT MaLHP, MaLHP + ' - ' + TenLopHocPhan AS HienThi FROM LopHocPhan";
//                FunctionQa.fillcombo(sqlLHP, cboLopHocPhan, "MaLHP", "HienThi");
//                cboLopHocPhan.SelectedIndex = -1;
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine("Lỗi tải ComboBox: " + ex.Message);
//            }
//        }

//        private void LoadData()
//        {
//            try
//            {
//                string sql = "SELECT pk.MaPhucKhao, sv.HoTen, lhp.TenLopHocPhan, pk.NgayYeuCau, pk.TrangThai, pk.MaSV, pk.MaLHP, pk.LyDo " +
//                             "FROM PhucKhao pk " +
//                             "JOIN SinhVien sv ON pk.MaSV = sv.MaSV " +
//                             "JOIN LopHocPhan lhp ON pk.MaLHP = lhp.MaLHP WHERE 1=1";

//                string keyword = txtTimKiem.Text.Trim();
//                if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm tên SV / lớp học phần...")
//                {
//                    sql += $" AND (sv.HoTen LIKE N'%{keyword}%' OR lhp.TenLopHocPhan LIKE N'%{keyword}%' OR pk.MaPhucKhao LIKE '%{keyword}%')";
//                }

//                if (cboFilterTrangThai.SelectedIndex > 0)
//                {
//                    string filterStatus = cboFilterTrangThai.SelectedItem.ToString();
//                    sql += $" AND pk.TrangThai = N'{filterStatus}'";
//                }

//                sql += " ORDER BY pk.MaPhucKhao DESC";

//                tblPhucKhao = FunctionQa.getdatatotable(sql);
//                dataGridView.DataSource = tblPhucKhao;

//                if (dataGridView.Columns.Count >= 8)
//                {
//                    dataGridView.Columns[0].HeaderText = "Mã PK";
//                    dataGridView.Columns[1].HeaderText = "Họ Tên SV";
//                    dataGridView.Columns[2].HeaderText = "Lớp Học Phần";
//                    dataGridView.Columns[3].HeaderText = "Ngày Yêu Cầu";
//                    dataGridView.Columns[4].HeaderText = "Trạng Thái";

//                    // Hide keys and multiline reasons from the main table grid
//                    dataGridView.Columns[5].Visible = false; // MaSV
//                    dataGridView.Columns[6].Visible = false; // MaLHP
//                    dataGridView.Columns[7].Visible = false; // LyDo
//                }

//                dataGridView.AllowUserToAddRows = false;
//                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
//                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

//                lblTongBanGhi.Text = $"Tổng: {tblPhucKhao.Rows.Count} bản ghi";
//                UpdateStatistics();
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine("Lỗi tải DataGridView: " + ex.Message);
//            }
//        }

//        private void UpdateStatistics()
//        {
//            try
//            {
//                string tong = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM PhucKhao");
//                string choDuyet = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM PhucKhao WHERE TrangThai = N'Chờ duyệt'");
//                string daDuyet = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM PhucKhao WHERE TrangThai = N'Đã duyệt'");
//                string tuChoi = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM PhucKhao WHERE TrangThai = N'Từ chối'");

//                lblStatTongVal.Text = string.IsNullOrEmpty(tong) ? "0" : tong;
//                lblStatChoDuyetVal.Text = string.IsNullOrEmpty(choDuyet) ? "0" : choDuyet;
//                lblStatDaDuyetVal.Text = string.IsNullOrEmpty(daDuyet) ? "0" : daDuyet;
//                lblStatTuChoiVal.Text = string.IsNullOrEmpty(tuChoi) ? "0" : tuChoi;
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine("Lỗi cập nhật Thống kê: " + ex.Message);
//            }
//        }

//        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0) return;
//            if (isEditMode) return; // Prevent row changing while editing

//            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
//            string maPK = row.Cells[0].Value?.ToString();
//            string ngayYCStr = row.Cells[3].Value?.ToString();
//            string trangThai = row.Cells[4].Value?.ToString();
//            string maSV = row.Cells[5].Value?.ToString();
//            string maLHP = row.Cells[6].Value?.ToString();
//            string lyDo = row.Cells[7].Value?.ToString();

//            currentMaPK = maPK;

//            // If request is still pending ("Chờ duyệt"), show in editable mode. Otherwise, read-only.
//            if (trangThai == "Chờ duyệt")
//            {
//                isAddingNew = false;
//                ShowEditMode(maPK, maSV, maLHP, ngayYCStr, trangThai, lyDo);
//            }
//            else
//            {
//                isAddingNew = false;
//                ShowViewMode(maPK, maSV, maLHP, ngayYCStr, trangThai, lyDo);
//            }
//        }

//        private void ShowViewMode(string maPK, string maSV, string maLHP, string ngayYCStr, string trangThai, string lyDo)
//        {
//            isEditMode = false;

//            lblDetailHeader.Text = "📄  Chi tiết Yêu cầu";
//            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

//            // Populate form values
//            cboSinhVien.SelectedValue = maSV;
//            cboLopHocPhan.SelectedValue = maLHP;

//            if (DateTime.TryParse(ngayYCStr, out DateTime dt))
//                dtpNgayYeuCau.Value = dt;

//            txtTrangThai.Text = trangThai;
//            txtLyDo.Text = lyDo;

//            // Inputs disabled for read-only view mode
//            cboSinhVien.Enabled = false;
//            cboLopHocPhan.Enabled = false;
//            dtpNgayYeuCau.Enabled = false;
//            txtTrangThai.ReadOnly = true;
//            txtLyDo.ReadOnly = true;

//            btnLuuDetail.Visible = false;
//            btnHuyDetail.Visible = false;
//            btnCloseDetail.Visible = true;

//            lblDetailStatus.Text = $"🔑 Đang xem: {maPK}";

//            OpenSidebar();
//        }

//        private void ShowEditMode(string maPK = "", string maSV = "", string maLHP = "", string ngayYCStr = "", string trangThai = "", string lyDo = "")
//        {
//            isEditMode = true;

//            lblDetailHeader.Text = isAddingNew ? "✚  Thêm yêu cầu mới" : "✎  Chỉnh sửa Yêu cầu";
//            lblDetailHeader.ForeColor = isAddingNew
//                ? Color.FromArgb(46, 125, 50)
//                : Color.FromArgb(230, 81, 0);

//            if (isAddingNew)
//            {
//                cboSinhVien.SelectedIndex = -1;
//                cboLopHocPhan.SelectedIndex = -1;
//                dtpNgayYeuCau.Value = DateTime.Today;
//                txtTrangThai.Text = "Chờ duyệt";
//                txtLyDo.Text = "";
//                lblDetailStatus.Text = $"➕ Đang thêm mới... {maPK}";
//            }
//            else
//            {
//                cboSinhVien.SelectedValue = maSV;
//                cboLopHocPhan.SelectedValue = maLHP;

//                if (DateTime.TryParse(ngayYCStr, out DateTime dt))
//                    dtpNgayYeuCau.Value = dt;

//                txtTrangThai.Text = trangThai;
//                txtLyDo.Text = lyDo;
//                lblDetailStatus.Text = $"🔑 Đang sửa: {maPK}";
//            }

//            // Enable appropriate controls for editing
//            cboSinhVien.Enabled = isAddingNew; // Only allow selecting student/class if adding new request
//            cboLopHocPhan.Enabled = isAddingNew;
//            dtpNgayYeuCau.Enabled = true;
//            txtTrangThai.ReadOnly = true; // Auto calculated, not manually editable
//            txtLyDo.ReadOnly = false;

//            btnLuuDetail.Visible = true;
//            btnHuyDetail.Visible = true;
//            btnCloseDetail.Visible = false;

//            OpenSidebar();
//            cboSinhVien.Focus();
//        }

//        private void OpenSidebar()
//        {
//            if (!detailPanelVisible)
//            {
//                pnlDetail.Visible = true;
//                Timer t = new Timer { Interval = 10 };
//                t.Tick += (s, ev) =>
//                {
//                    if (pnlDetail.Width < 290)
//                        pnlDetail.Width += 20;
//                    else
//                    {
//                        pnlDetail.Width = 290;
//                        t.Stop();
//                        t.Dispose();
//                    }
//                };
//                t.Start();
//                detailPanelVisible = true;
//            }
//        }

//        private void CloseSidebar()
//        {
//            Timer t = new Timer { Interval = 10 };
//            t.Tick += (s, ev) =>
//            {
//                if (pnlDetail.Width > 0)
//                    pnlDetail.Width -= 20;
//                else
//                {
//                    pnlDetail.Width = 0;
//                    pnlDetail.Visible = false;
//                    t.Stop();
//                    t.Dispose();
//                    detailPanelVisible = false;
//                    isEditMode = false;
//                    isAddingNew = false;
//                    dataGridView.ClearSelection();
//                }
//            };
//            t.Start();
//        }

//        private void btnCloseDetail_Click(object sender, EventArgs e)
//        {
//            CloseSidebar();
//        }

//        private void btnThem_Click(object sender, EventArgs e)
//        {
//            isAddingNew = true;
//            dataGridView.ClearSelection();
//            string nextMaPK = GenerateNewMaPhucKhao();
//            currentMaPK = nextMaPK;
//            ShowEditMode(nextMaPK);
//        }

//        private string GenerateNewMaPhucKhao()
//        {
//            try
//            {
//                string sql = "SELECT MaPhucKhao FROM PhucKhao ORDER BY MaPhucKhao DESC";
//                DataTable dt = FunctionQa.getdatatotable(sql);
//                if (dt.Rows.Count == 0)
//                {
//                    return "PK001";
//                }

//                int maxVal = 0;
//                foreach (DataRow row in dt.Rows)
//                {
//                    string id = row["MaPhucKhao"].ToString().Trim();
//                    if (id.StartsWith("PK") && int.TryParse(id.Substring(2), out int val))
//                    {
//                        if (val > maxVal)
//                        {
//                            maxVal = val;
//                        }
//                    }
//                }
//                return "PK" + (maxVal + 1).ToString("D3");
//            }
//            catch
//            {
//                return "PK001";
//            }
//        }

//        private void btnDuyet_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần duyệt.", "Thông báo",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            DataGridViewRow row = dataGridView.SelectedRows[0];
//            string maPK = row.Cells[0].Value?.ToString();
//            string trangThai = row.Cells[4].Value?.ToString();

//            if (trangThai == "Đã duyệt")
//            {
//                MessageBox.Show("Yêu cầu này đã được duyệt trước đó.", "Thông báo",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn DUYỆT yêu cầu '{maPK}'?", "Xác nhận duyệt",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                try
//                {
//                    string sql = $"UPDATE PhucKhao SET TrangThai = N'Đã duyệt' WHERE MaPhucKhao = '{maPK}'";
//                    FunctionQa.runsql(sql);
//                    MessageBox.Show("Duyệt yêu cầu phúc khảo thành công!", "Thành công",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    CloseSidebar();
//                    LoadData();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi khi duyệt yêu cầu: " + ex.Message, "Lỗi",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnTuChoi_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần từ chối.", "Thông báo",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            DataGridViewRow row = dataGridView.SelectedRows[0];
//            string maPK = row.Cells[0].Value?.ToString();
//            string trangThai = row.Cells[4].Value?.ToString();

//            if (trangThai == "Từ chối")
//            {
//                MessageBox.Show("Yêu cầu này đã bị từ chối trước đó.", "Thông báo",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }

//            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn TỪ CHỐI yêu cầu '{maPK}'?", "Xác nhận từ chối",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                try
//                {
//                    string sql = $"UPDATE PhucKhao SET TrangThai = N'Từ chối' WHERE MaPhucKhao = '{maPK}'";
//                    FunctionQa.runsql(sql);
//                    MessageBox.Show("Từ chối yêu cầu phúc khảo thành công!", "Thành công",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    CloseSidebar();
//                    LoadData();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi khi từ chối yêu cầu: " + ex.Message, "Lỗi",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnXoa_Click(object sender, EventArgs e)
//        {
//            if (dataGridView.SelectedRows.Count == 0)
//            {
//                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần xóa.", "Thông báo",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            DataGridViewRow row = dataGridView.SelectedRows[0];
//            string maPK = row.Cells[0].Value?.ToString();

//            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn XÓA yêu cầu '{maPK}'?", "Xác nhận xóa",
//                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (confirm == DialogResult.Yes)
//            {
//                try
//                {
//                    string sql = $"DELETE FROM PhucKhao WHERE MaPhucKhao = '{maPK}'";
//                    FunctionQa.RunSqlDel(sql);
//                    CloseSidebar();
//                    LoadData();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi khi xóa yêu cầu: " + ex.Message, "Lỗi",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnLamMoi_Click(object sender, EventArgs e)
//        {
//            txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
//            txtTimKiem.ForeColor = Color.Gray;
//            cboFilterTrangThai.SelectedIndex = 0;
//            CloseSidebar();
//            LoadData();
//        }

//        private void btnLuuDetail_Click(object sender, EventArgs e)
//        {
//            if (cboSinhVien.SelectedValue == null)
//            {
//                MessageBox.Show("Vui lòng chọn sinh viên.", "Thiếu thông tin",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                cboSinhVien.Focus();
//                return;
//            }

//            if (cboLopHocPhan.SelectedValue == null)
//            {
//                MessageBox.Show("Vui lòng chọn lớp học phần.", "Thiếu thông tin",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                cboLopHocPhan.Focus();
//                return;
//            }

//            string lyDo = txtLyDo.Text.Trim();
//            if (string.IsNullOrEmpty(lyDo))
//            {
//                MessageBox.Show("Vui lòng nhập lý do phúc khảo.", "Thiếu thông tin",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                txtLyDo.Focus();
//                return;
//            }

//            string maSV = cboSinhVien.SelectedValue.ToString();
//            string maLHP = cboLopHocPhan.SelectedValue.ToString();
//            string ngayYC = dtpNgayYeuCau.Value.ToString("yyyy-MM-dd");
//            string trangThai = txtTrangThai.Text;

//            try
//            {
//                if (isAddingNew)
//                {
//                    // Generate new ID to ensure no duplicate keys
//                    string maPK = GenerateNewMaPhucKhao();

//                    string sql = $"INSERT INTO PhucKhao (MaPhucKhao, MaSV, MaLHP, NgayYeuCau, TrangThai, LyDo) " +
//                                 $"VALUES ('{maPK}', '{maSV}', '{maLHP}', '{ngayYC}', N'{trangThai}', N'{lyDo}')";
//                    FunctionQa.runsql(sql);
//                    MessageBox.Show("Thêm yêu cầu phúc khảo mới thành công!", "Thành công",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    LoadData();
//                    ShowViewMode(maPK, maSV, maLHP, ngayYC, trangThai, lyDo);
//                }
//                else
//                {
//                    string sql = $"UPDATE PhucKhao SET " +
//                                 $"NgayYeuCau = '{ngayYC}', " +
//                                 $"LyDo = N'{lyDo}' " +
//                                 $"WHERE MaPhucKhao = '{currentMaPK}'";
//                    FunctionQa.runsql(sql);
//                    MessageBox.Show("Cập nhật yêu cầu phúc khảo thành công!", "Thành công",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    LoadData();
//                    ShowViewMode(currentMaPK, maSV, maLHP, ngayYC, trangThai, lyDo);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi khi lưu yêu cầu phúc khảo: " + ex.Message, "Lỗi",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnHuyDetail_Click(object sender, EventArgs e)
//        {
//            if (isAddingNew)
//            {
//                CloseSidebar();
//            }
//            else
//            {
//                if (dataGridView.SelectedRows.Count > 0)
//                {
//                    DataGridViewRow row = dataGridView.SelectedRows[0];
//                    string maPK = row.Cells[0].Value?.ToString();
//                    string ngayYCStr = row.Cells[3].Value?.ToString();
//                    string trangThai = row.Cells[4].Value?.ToString();
//                    string maSV = row.Cells[5].Value?.ToString();
//                    string maLHP = row.Cells[6].Value?.ToString();
//                    string lyDo = row.Cells[7].Value?.ToString();

//                    ShowViewMode(maPK, maSV, maLHP, ngayYCStr, trangThai, lyDo);
//                }
//                else
//                {
//                    CloseSidebar();
//                }
//            }
//        }

//        private void cboFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            LoadData();
//        }

//        private void txtTimKiem_TextChanged(object sender, EventArgs e)
//        {
//            // Only perform dynamic search if user is typing a real search query (not the placeholder)
//            string kw = txtTimKiem.Text.Trim();
//            if (kw != "Tìm tên SV / lớp học phần...")
//            {
//                LoadData();
//            }
//        }
//    }
//}
