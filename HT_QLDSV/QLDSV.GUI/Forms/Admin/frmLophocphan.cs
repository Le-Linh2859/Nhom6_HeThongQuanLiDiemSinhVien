using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmLophocphan : Form, IShellChildForm
    {
        private bool detailPanelVisible = false;
        private bool isEditMode = false;        // false = View, true = Add/Edit
        private bool isAddingNew = false;       // true = Thêm mới, false = Sửa

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        public frmLophocphan()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up toolbar events
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.btnTim.Click += btnTim_Click;
            
            // Wire up cascade events
            this.cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;
            this.cboFilterMon.SelectedIndexChanged += cboFilterMon_SelectedIndexChanged;
            this.cboEditKhoa.SelectedIndexChanged += cboEditKhoa_SelectedIndexChanged;

            // Wire up Logout button
            if (this.guna2Button7 != null)
            {
                this.guna2Button7.Click += (s, e) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        var loginForm = new frmDangNhap();
                        loginForm.Show();
                        this.Hide();
                    }
                };
            }
        }

        private void BuildSidebarMenu()
        {
            string[] menuItems = {
                "Tổng quan", "Giảng viên", "Sinh viên", "Môn học",
                "Lớp niên chế", "Lớp học phần", "Đăng ký lớp",
                "Nhập điểm", "Kết quả học tập", "Cảnh báo học vụ",
                "Phúc khảo", "Báo cáo"
            };
            string[] icons = { "📊", "👨‍🏫", "🎓", "📚", "🏛", "📋", "📝", "✏️", "📈", "⚠️", "🔄", "📑" };
            
            var visibleItems = new System.Collections.Generic.List<string>();
            var visibleIcons = new System.Collections.Generic.List<string>();
            string role = SessionHelper.MaVaiTro;

            for (int i = 0; i < menuItems.Length; i++)
            {
                bool isVisible = true;
                if (role == "VT002") // Giảng viên
                {
                    if (menuItems[i] == "Tổng quan" || menuItems[i] == "Giảng viên" || menuItems[i] == "Môn học" || menuItems[i] == "Lớp niên chế")
                    {
                        isVisible = false;
                    }
                }
                else if (role == "VT003") // Sinh viên
                {
                    if (menuItems[i] != "Đăng ký lớp" && menuItems[i] != "Kết quả học tập" && menuItems[i] != "Phúc khảo")
                    {
                        isVisible = false;
                    }
                }

                if (isVisible)
                {
                    visibleItems.Add(menuItems[i]);
                    visibleIcons.Add(icons[i]);
                }
            }

            int startY = 55;
            for (int i = 0; i < visibleItems.Count; i++)
            {
                var btn = new Guna.UI2.WinForms.Guna2Button();
                btn.FillColor = System.Drawing.Color.Transparent;
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Size = new System.Drawing.Size(180, 32);
                btn.Location = new System.Drawing.Point(8, startY + i * 34);
                btn.Text = "  " + visibleIcons[i] + "  " + visibleItems[i];
                btn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                
                if (visibleItems[i] == "Lớp học phần")
                {
                    btn.FillColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                    btn.BorderRadius = 5;
                }
                
                string itemText = visibleItems[i];
                btn.Click += (s, ev) =>
                {
                    Form targetForm = null;
                    if (itemText == "Giảng viên")
                    {
                        targetForm = new frmGiangVien();
                    }
                    else if (itemText == "Tổng quan")
                    {
                        targetForm = new frmTongQuan();
                    }
                    else if (itemText == "Sinh viên")
                    {
                        targetForm = new frmQuanLiThongTinSinhVien();
                    }
                    else if (itemText == "Môn học")
                    {
                        targetForm = new frmMonhoc();
                    }
                    else if (itemText == "Lớp niên chế")
                    {
                        targetForm = new FrmLopNienChe();
                    }
                    else if (itemText == "Cảnh báo học vụ")
                    {
                        targetForm = new frmCanhBaoHocVu();
                    }
                    else if (itemText == "Đăng ký lớp")
                    {
                        targetForm = new frmDangKyHocPhan();
                    }
                    else if (itemText == "Phúc khảo")
                    {
                        targetForm = new frmPhucKhao();
                    }
                    else if (itemText == "Báo cáo")
                    {
                        targetForm = new frmBaoCaoThongKe();
                    }

                    if (targetForm != null)
                    {
                        targetForm.FormClosed += (senderForm, eArg) =>
                        {
                            this.Show();
                        };
                        targetForm.Show();
                        this.Hide();
                    }
                };

                pnlSidebar.Controls.Add(btn);
            }
        }

        private void frmLophocphan_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();
                
                // Muted cascade triggers during initialization
                cboFilterKhoa.SelectedIndexChanged -= cboFilterKhoa_SelectedIndexChanged;
                cboEditKhoa.SelectedIndexChanged -= cboEditKhoa_SelectedIndexChanged;

                LoadInitialComboBoxes();
                
                cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;
                cboEditKhoa.SelectedIndexChanged += cboEditKhoa_SelectedIndexChanged;

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu hoặc tải Form: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
        }

        private DataTable tblLophocphan;

        private void LoadInitialComboBoxes()
        {
            try
            {
                // 1. Khoa filter combo
                string sqlKhoaFilter = "SELECT MaKhoa, TenKhoa FROM Khoa";
                DataTable dtKhoaFilter = FunctionQa.getdatatotable(sqlKhoaFilter);
                DataRow rAll = dtKhoaFilter.NewRow();
                rAll["MaKhoa"] = "ALL";
                rAll["TenKhoa"] = "--- Tất cả ---";
                dtKhoaFilter.Rows.InsertAt(rAll, 0);

                cboFilterKhoa.DataSource = dtKhoaFilter;
                cboFilterKhoa.ValueMember = "MaKhoa";
                cboFilterKhoa.DisplayMember = "TenKhoa";
                cboFilterKhoa.SelectedIndex = 0;

                // 2. Môn filter combo (Initially all, or filtered based on cboFilterKhoa)
                UpdateFilterMonComboBox();

                // 3. Khoa edit combo
                string sqlKhoaEdit = "SELECT MaKhoa, TenKhoa FROM Khoa";
                FunctionQa.fillcombo(sqlKhoaEdit, cboEditKhoa, "MaKhoa", "TenKhoa");
                cboEditKhoa.SelectedIndex = -1;

                // 4. Môn edit combo (Empty initially, populated when Khoa selected)
                cboEditMon.DataSource = null;
                cboEditMon.Items.Clear();

                // 5. Giảng viên edit combo (Empty initially, populated when Khoa selected)
                cboEditGiangVien.DataSource = null;
                cboEditGiangVien.Items.Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi tải Comboboxes ban đầu: " + ex.Message);
            }
        }

        private void UpdateFilterMonComboBox()
        {
            try
            {
                string khoaVal = cboFilterKhoa.SelectedValue?.ToString() ?? "ALL";
                string sqlMonFilter = "SELECT MaMon, TenMon FROM MonHoc";
                if (khoaVal != "ALL")
                {
                    sqlMonFilter += $" WHERE MaKhoa = '{khoaVal}'";
                }

                DataTable dtMonFilter = FunctionQa.getdatatotable(sqlMonFilter);
                DataRow rAllMon = dtMonFilter.NewRow();
                rAllMon["MaMon"] = "ALL";
                rAllMon["TenMon"] = "--- Tất cả ---";
                dtMonFilter.Rows.InsertAt(rAllMon, 0);

                cboFilterMon.SelectedIndexChanged -= cboFilterMon_SelectedIndexChanged;
                cboFilterMon.DataSource = dtMonFilter;
                cboFilterMon.ValueMember = "MaMon";
                cboFilterMon.DisplayMember = "TenMon";
                cboFilterMon.SelectedIndex = 0;
                cboFilterMon.SelectedIndexChanged += cboFilterMon_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp Môn Học Filter: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT lhp.MaLHP, lhp.TenLopHocPhan, " +
                             "lhp.Thu + ' (Ca ' + CAST(lhp.CaHoc AS varchar) + ')' AS ThoiGianHoc, " +
                             "lhp.PhongHoc, k.TenKhoa, mh.TenMon, gv.HoTen AS TenGiangVien, " +
                             "CASE WHEN lhp.TrangThai = 'DangMo' THEN N'Hoạt động' ELSE N'Khóa' END AS TrangThai, " +
                             "mh.MaKhoa, lhp.MaMon, lhp.MaGV " +
                             "FROM LopHocPhan lhp " +
                             "LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                             "LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa " +
                             "LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV WHERE 1=1";

                string keyword = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += $" AND (lhp.MaLHP LIKE '%{keyword}%' OR lhp.TenLopHocPhan LIKE N'%{keyword}%')";
                }

                if (cboFilterKhoa.SelectedValue != null && cboFilterKhoa.SelectedValue.ToString() != "ALL")
                {
                    sql += $" AND mh.MaKhoa = '{cboFilterKhoa.SelectedValue}'";
                }

                if (cboFilterMon.SelectedValue != null && cboFilterMon.SelectedValue.ToString() != "ALL")
                {
                    sql += $" AND lhp.MaMon = '{cboFilterMon.SelectedValue}'";
                }

                tblLophocphan = FunctionQa.getdatatotable(sql);
                dataGridView.DataSource = tblLophocphan;

                if (dataGridView.Columns.Count >= 11)
                {
                    dataGridView.Columns["MaLHP"].HeaderText = "Mã Lớp HP";
                    dataGridView.Columns["TenLopHocPhan"].HeaderText = "Tên Lớp Học Phần";
                    dataGridView.Columns["ThoiGianHoc"].HeaderText = "Thời Gian Học";
                    dataGridView.Columns["PhongHoc"].HeaderText = "Phòng Học";
                    dataGridView.Columns["TenKhoa"].HeaderText = "Khoa";
                    dataGridView.Columns["TenMon"].HeaderText = "Môn Học";
                    dataGridView.Columns["TenGiangVien"].HeaderText = "Giảng Viên";
                    dataGridView.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    
                    dataGridView.Columns["MaKhoa"].Visible = false;
                    dataGridView.Columns["MaMon"].Visible = false;
                    dataGridView.Columns["MaGV"].Visible = false;
                }

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblLophocphan.Rows.Count} lớp học phần";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu lớp học phần: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isEditMode) return; // Khóa row click khi đang chỉnh sửa

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            string maLHP = row.Cells["MaLHP"].Value?.ToString();
            string tenLHP = row.Cells["TenLopHocPhan"].Value?.ToString();
            string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
            string phong = row.Cells["PhongHoc"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string tenGV = row.Cells["TenGiangVien"].Value?.ToString();
            string activeStr = row.Cells["TrangThai"].Value?.ToString();

            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string maGV = row.Cells["MaGV"].Value?.ToString();

            ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, activeStr, maKhoa, maMon, maGV);
        }

        private void ShowViewMode(string maLHP, string tenLHP, string thoiGian, string phong, string tenKhoa, string tenMon, string tenGV, string activeStr, string maKhoa, string maMon, string maGV)
        {
            isEditMode = false;
            isAddingNew = false;

            lblDetailHeader.Text = "Chi tiết Lớp HP";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Toggle visibility
            lblDetailMaLHP.Visible = true;
            lblDetailTenLHP.Visible = true;
            lblDetailThoiGianHoc.Visible = true;
            lblDetailPhongHoc.Visible = true;
            lblDetailKhoa.Visible = true;
            lblDetailMon.Visible = true;
            lblDetailGiangVien.Visible = true;
            lblDetailTrangThai.Visible = true;

            txtEditMaLHP.Visible = false;
            txtEditTenLHP.Visible = false;
            txtEditThoiGianHoc.Visible = false;
            txtEditPhongHoc.Visible = false;
            cboEditKhoa.Visible = false;
            cboEditMon.Visible = false;
            cboEditGiangVien.Visible = false;
            chkEditActive.Visible = false;

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
            lblDetailTrangThai.Text = activeStr ?? "Hoạt động";

            OpenSidebar();
        }

        private void ShowEditMode(string maLHP = "", string tenLHP = "", string thoiGian = "", string phong = "", string maKhoa = "", string maMon = "", string maGV = "", bool isActive = true)
        {
            isEditMode = true;

            lblDetailHeader.Text = isAddingNew ? "✚  Thêm Lớp HP mới" : "✎  Chỉnh sửa Lớp HP";
            lblDetailHeader.ForeColor = isAddingNew ? Color.FromArgb(27, 120, 53) : Color.FromArgb(180, 80, 0);

            // Toggle visibility
            lblDetailMaLHP.Visible = false;
            lblDetailTenLHP.Visible = false;
            lblDetailThoiGianHoc.Visible = false;
            lblDetailPhongHoc.Visible = false;
            lblDetailKhoa.Visible = false;
            lblDetailMon.Visible = false;
            lblDetailGiangVien.Visible = false;
            lblDetailTrangThai.Visible = false;

            txtEditMaLHP.Visible = true;
            txtEditTenLHP.Visible = true;
            txtEditThoiGianHoc.Visible = true;
            txtEditPhongHoc.Visible = true;
            cboEditKhoa.Visible = true;
            cboEditMon.Visible = true;
            cboEditGiangVien.Visible = true;
            chkEditActive.Visible = true;

            btnLuuDetail.Visible = true;
            btnHuyDetail.Visible = true;
            btnCloseDetail.Visible = false;

            // Load Inputs
            txtEditMaLHP.Text = maLHP;
            txtEditTenLHP.Text = tenLHP;
            txtEditThoiGianHoc.Text = thoiGian;
            txtEditPhongHoc.Text = phong;
            chkEditActive.Checked = isActive;

            // Muted trigger to avoid premature clearing
            cboEditKhoa.SelectedIndexChanged -= cboEditKhoa_SelectedIndexChanged;

            if (!string.IsNullOrEmpty(maKhoa))
            {
                cboEditKhoa.SelectedValue = maKhoa;
                // Dynamically populate cascade comboboxes based on this khoa
                PopulateEditCascadeCombos(maKhoa);
                
                if (!string.IsNullOrEmpty(maMon))
                    cboEditMon.SelectedValue = maMon;
                else
                    cboEditMon.SelectedIndex = -1;

                if (!string.IsNullOrEmpty(maGV))
                    cboEditGiangVien.SelectedValue = maGV;
                else
                    cboEditGiangVien.SelectedIndex = -1;
            }
            else
            {
                cboEditKhoa.SelectedIndex = -1;
                cboEditMon.DataSource = null;
                cboEditGiangVien.DataSource = null;
            }

            cboEditKhoa.SelectedIndexChanged += cboEditKhoa_SelectedIndexChanged;

            txtEditMaLHP.ReadOnly = !isAddingNew;
            txtEditMaLHP.BackColor = isAddingNew ? Color.White : Color.FromArgb(240, 240, 240);

            OpenSidebar();
            txtEditTenLHP.Focus();
        }

        private void PopulateEditCascadeCombos(string maKhoa)
        {
            try
            {
                // Cascade subjects (MonHoc) in selected department (Khoa)
                string sqlMon = $"SELECT MaMon, TenMon FROM MonHoc WHERE MaKhoa = '{maKhoa}'";
                FunctionQa.fillcombo(sqlMon, cboEditMon, "MaMon", "TenMon");
                cboEditMon.SelectedIndex = -1;

                // Cascade lecturers (GiangVien) in selected department (Khoa)
                string sqlGV = $"SELECT MaGV, HoTen FROM GiangVien WHERE MaKhoa = '{maKhoa}'";
                FunctionQa.fillcombo(sqlGV, cboEditGiangVien, "MaGV", "HoTen");
                cboEditGiangVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp cascade detail: " + ex.Message);
            }
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
            string activeStr = row.Cells["TrangThai"].Value?.ToString();
            bool isActive = (activeStr == "Hoạt động");

            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string maGV = row.Cells["MaGV"].Value?.ToString();

            ShowEditMode(maLHP, tenLHP, thoiGian, phong, maKhoa, maMon, maGV, isActive);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLHP = dataGridView.SelectedRows[0].Cells["MaLHP"].Value?.ToString();
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học phần '{maLHP}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                FunctionQa.RunSqlDel($"DELETE FROM LopHocPhan WHERE MaLHP = '{maLHP}'");
                CloseSidebar();
                LoadData();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            
            cboFilterKhoa.SelectedIndexChanged -= cboFilterKhoa_SelectedIndexChanged;
            cboFilterKhoa.SelectedIndex = 0;
            cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;

            UpdateFilterMonComboBox();
            LoadData();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTim_Click(null, null);
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
            if (cboEditKhoa.SelectedValue != null)
            {
                string maKhoa = cboEditKhoa.SelectedValue.ToString();
                PopulateEditCascadeCombos(maKhoa);
            }
            else
            {
                cboEditMon.DataSource = null;
                cboEditGiangVien.DataSource = null;
            }
        }

        private void btnLuuDetail_Click(object sender, EventArgs e)
        {
            string ma = txtEditMaLHP.Text.Trim();
            string ten = txtEditTenLHP.Text.Trim();
            string thoiGian = txtEditThoiGianHoc.Text.Trim();
            string phong = txtEditPhongHoc.Text.Trim();
            int active = chkEditActive.Checked ? 1 : 0;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(thoiGian) || string.IsNullOrEmpty(phong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường bắt buộc (*).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboEditKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEditKhoa.Focus();
                return;
            }

            if (cboEditMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Môn học cho lớp này.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEditMon.Focus();
                return;
            }

            if (cboEditGiangVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Giảng viên phụ trách giảng dạy.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEditGiangVien.Focus();
                return;
            }

            string maKhoa = cboEditKhoa.SelectedValue.ToString();
            string maMon = cboEditMon.SelectedValue.ToString();
            string maGV = cboEditGiangVien.SelectedValue.ToString();

            string thu = "Thứ 2";
            int caHoc = 1;
            if (!string.IsNullOrEmpty(thoiGian))
            {
                if (thoiGian.Contains("Thứ 2") || thoiGian.Contains("T2")) thu = "Thứ 2";
                else if (thoiGian.Contains("Thứ 3") || thoiGian.Contains("T3")) thu = "Thứ 3";
                else if (thoiGian.Contains("Thứ 4") || thoiGian.Contains("T4")) thu = "Thứ 4";
                else if (thoiGian.Contains("Thứ 5") || thoiGian.Contains("T5")) thu = "Thứ 5";
                else if (thoiGian.Contains("Thứ 6") || thoiGian.Contains("T6")) thu = "Thứ 6";
                else if (thoiGian.Contains("Thứ 7") || thoiGian.Contains("T7")) thu = "Thứ 7";
                else if (thoiGian.Contains("Chủ nhật") || thoiGian.Contains("CN")) thu = "Chủ nhật";

                var match = System.Text.RegularExpressions.Regex.Match(thoiGian, @"\d+");
                if (match.Success)
                {
                    int.TryParse(match.Value, out caHoc);
                }
            }
            string trangThai = (active == 1) ? "DangMo" : "DaDong";
            string maHKNH = "HK007"; // default semester linking correctly to seed data

            try
            {
                if (isAddingNew)
                {
                    // Check key exist
                    if (FunctionQa.checkkey($"SELECT 1 FROM LopHocPhan WHERE MaLHP = '{ma}'"))
                    {
                        MessageBox.Show($"Mã lớp học phần '{ma}' đã tồn tại. Vui lòng sử dụng mã khác.", "Trùng khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEditMaLHP.Focus();
                        return;
                    }

                    string sql = $"INSERT INTO LopHocPhan (MaLHP, TenLopHocPhan, CaHoc, Thu, PhongHoc, ThoiGianBD, ThoiGianKT, SoLuongToiDa, TrangThai, MaMon, MaGV, MaHKNH) " +
                                 $"VALUES ('{ma}', N'{ten}', {caHoc}, N'{thu}', N'{phong}', '2026-08-15', '2026-12-15', 50, N'{trangThai}', '{maMon}', '{maGV}', '{maHKNH}')";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Thêm lớp học phần mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = $"UPDATE LopHocPhan SET TenLopHocPhan = N'{ten}', CaHoc = {caHoc}, Thu = N'{thu}', PhongHoc = N'{phong}', " +
                                 $"TrangThai = N'{trangThai}', MaMon = '{maMon}', MaGV = '{maGV}' " +
                                 $"WHERE MaLHP = '{ma}'";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Cập nhật thông tin lớp học phần thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                detailPanelVisible = false;

                // Transition back to View Mode with saved values
                string activeStr = (active == 1) ? "Hoạt động" : "Khóa";
                string tenKhoa = cboEditKhoa.Text;
                string tenMon = cboEditMon.Text;
                string tenGV = cboEditGiangVien.Text;
                ShowViewMode(ma, ten, thoiGian, phong, tenKhoa, tenMon, tenGV, activeStr, maKhoa, maMon, maGV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu lớp học phần: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string activeStr = row.Cells["TrangThai"].Value?.ToString();

                    string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
                    string maMon = row.Cells["MaMon"].Value?.ToString();
                    string maGV = row.Cells["MaGV"].Value?.ToString();

                    ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, activeStr, maKhoa, maMon, maGV);
                }
                else
                {
                    CloseSidebar();
                }
            }
        }
    }
}
