using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.Admin
{
    public partial class frmPhucKhao_Admin : Form, IShellChildForm
    {
        private bool detailPanelVisible = false;
        private string currentMaPK = "";
        private DataTable tblPhucKhao;

        public void OnEmbeddedInShell()
        {
            // [UX IMPROVEMENT] Hide duplicate header and sidebar when embedded
            string[] controlNames = { 
                "pnlSidebar", "pnlHeader"
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

        public frmPhucKhao_Admin()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up event handlers programmatically
            this.Resize += (s, e) => MakeLayoutResponsive();
            this.Load += frmPhucKhao_Admin_Load;
            this.btnDuyet.Click += btnDuyet_Click;
            this.btnTuChoi.Click += btnTuChoi_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.btnCloseDetail.Click += btnCloseDetail_Click;
            this.btnHuyDetail.Click += btnHuyDetail_Click;
            this.dataGridView.CellClick += dataGridView_CellClick;
            this.cboFilterTrangThai.SelectedIndexChanged += cboFilterTrangThai_SelectedIndexChanged;
            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            // Wire up Logout button in sidebar
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

            // [SEARCH PALETTE] Placeholder styling
            txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm tên SV / lớp học phần...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };

            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TaiDuLieu();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
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

            int startY = 55;
            for (int i = 0; i < menuItems.Length; i++)
            {
                var btn = new Guna.UI2.WinForms.Guna2Button();
                btn.FillColor = Color.Transparent;
                btn.Font = new Font("Segoe UI", 9F);
                btn.ForeColor = Color.White;
                btn.Size = new Size(180, 32);
                btn.Location = new Point(8, startY + i * 34);
                btn.Text = "  " + icons[i] + "  " + menuItems[i];
                btn.TextAlign = HorizontalAlignment.Left;
                btn.Cursor = Cursors.Hand;
                if (menuItems[i] == "Phúc khảo")
                {
                    btn.FillColor = Color.FromArgb(224, 224, 224);
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    btn.BorderRadius = 5;
                }

                string itemText = menuItems[i];
                btn.Click += (s, ev) =>
                {
                    Form targetForm = null;
                    if (itemText == "Tổng quan")
                    {
                        targetForm = new frmKhoa();
                    }
                    else if (itemText == "Giảng viên")
                    {
                        targetForm = new frmGiangvien();
                    }
                    else if (itemText == "Môn học")
                    {
                        targetForm = new frmMonhoc();
                    }
                    else if (itemText == "Sinh viên")
                    {
                        targetForm = new frmQuanLiThongTinSinhVien();
                    }
                    else if (itemText == "Cảnh báo học vụ")
                    {
                        targetForm = new frmCanhBaoHocVu();
                    }
                    else if (itemText == "Lớp học phần")
                    {
                        targetForm = new frmLophocphan();
                    }
                    else if (itemText == "Đăng ký lớp")
                    {
                        targetForm = new frmDangKyHocPhan();
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
                            this.TaiDuLieu();
                        };
                        targetForm.Show();
                        this.Hide();
                    }
                };

                pnlSidebar.Controls.Add(btn);
            }
        }

        private void frmPhucKhao_Admin_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();

                // Setup state filter dropdown
                cboFilterTrangThai.Items.Clear();
                cboFilterTrangThai.Items.Add("--- Tất cả ---");
                cboFilterTrangThai.Items.Add("Chờ duyệt");
                cboFilterTrangThai.Items.Add("Đã duyệt");
                cboFilterTrangThai.Items.Add("Từ chối");
                cboFilterTrangThai.SelectedIndex = 0;

                LoadComboBoxes();
                TaiDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu hoặc khởi tạo form: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
        }

        private void LoadComboBoxes()
        {
            try
            {
                string sqlSV = "SELECT MaSV, MaSV + ' - ' + HoTen AS HienThi FROM SinhVien";
                FunctionQa.fillcombo(sqlSV, cboSinhVien, "MaSV", "HienThi");
                cboSinhVien.SelectedIndex = -1;

                string sqlLHP = "SELECT MaLHP, MaLHP + ' - ' + TenLopHocPhan AS HienThi FROM LopHocPhan";
                FunctionQa.fillcombo(sqlLHP, cboLopHocPhan, "MaLHP", "HienThi");
                cboLopHocPhan.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi tải ComboBox: " + ex.Message);
            }
        }

        private void TaiDuLieu()
        {
            try
            {
                // Admin xem tất cả đơn phúc khảo: Mã PK, Họ Tên SV, Mã SV, Lớp HP, Giảng Viên phụ trách, Ngày YC, Trạng Thái, Lý Do
                string sql = "SELECT pk.MaPhucKhao AS [Mã PK], " +
                             "sv.HoTen AS [Họ Tên SV], " +
                             "pk.MaSV AS [Mã SV], " +
                             "lhp.TenLopHocPhan AS [Lớp Học Phần], " +
                             "ISNULL(gv.HoTen, N'Chưa phân công') AS [Giảng Viên], " +
                             "CONVERT(varchar,pk.NgayYeuCau,103) AS [Ngày Yêu Cầu], " +
                             "CASE WHEN pk.TrangThai = 'ChoDuyet' OR pk.TrangThai = N'Chờ duyệt' THEN N'Chờ duyệt' " +
                             "     WHEN pk.TrangThai = 'DaDuyet' OR pk.TrangThai = N'Đã duyệt' OR pk.TrangThai = 'DangXuLy' THEN N'Đã duyệt' " +
                             "     WHEN pk.TrangThai = 'TuChoi' OR pk.TrangThai = N'Từ chối' THEN N'Từ chối' " +
                             "     ELSE pk.TrangThai END AS [Trạng Thái], " +
                             "pk.LyDo AS [Lý Do], " +
                             "pk.MaSV AS _MaSV, pk.MaLHP AS _MaLHP " +
                             "FROM PhucKhao pk " +
                             "JOIN SinhVien sv ON pk.MaSV = sv.MaSV " +
                             "JOIN LopHocPhan lhp ON pk.MaLHP = lhp.MaLHP " +
                             "LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV " +
                             "WHERE 1=1";

                string keyword = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm tên SV / lớp học phần...")
                {
                    sql += $" AND (sv.HoTen LIKE N'%{keyword}%' OR lhp.TenLopHocPhan LIKE N'%{keyword}%' OR pk.MaPhucKhao LIKE '%{keyword}%' OR pk.MaSV LIKE '%{keyword}%')";
                }

                if (cboFilterTrangThai.SelectedIndex > 0)
                {
                    string filterStatus = cboFilterTrangThai.SelectedItem.ToString();
                    if (filterStatus == "Chờ duyệt")
                    {
                        sql += " AND (pk.TrangThai = 'ChoDuyet' OR pk.TrangThai = N'Chờ duyệt')";
                    }
                    else if (filterStatus == "Đã duyệt")
                    {
                        sql += " AND (pk.TrangThai = 'DaDuyet' OR pk.TrangThai = N'Đã duyệt' OR pk.TrangThai = 'DangXuLy')";
                    }
                    else if (filterStatus == "Từ chối")
                    {
                        sql += " AND (pk.TrangThai = 'TuChoi' OR pk.TrangThai = N'Từ chối')";
                    }
                    else
                    {
                        sql += $" AND pk.TrangThai = N'{filterStatus}'";
                    }
                }

                sql += " ORDER BY pk.MaPhucKhao DESC";

                tblPhucKhao = FunctionQa.getdatatotable(sql);
                dataGridView.DataSource = tblPhucKhao;

                // Ẩn cột helper (không hiển thị ra ngoài)
                if (dataGridView.Columns.Contains("_MaSV"))
                    dataGridView.Columns["_MaSV"].Visible = false;
                if (dataGridView.Columns.Contains("_MaLHP"))
                    dataGridView.Columns["_MaLHP"].Visible = false;

                // Tô màu cột Trạng Thái
                ApplyStatusColoring();

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Căn chỉnh độ rộng cột
                if (dataGridView.Columns.Contains("Mã PK"))       dataGridView.Columns["Mã PK"].Width = 70;
                if (dataGridView.Columns.Contains("Họ Tên SV"))   dataGridView.Columns["Họ Tên SV"].Width = 130;
                if (dataGridView.Columns.Contains("Mã SV"))        dataGridView.Columns["Mã SV"].Width = 90;
                if (dataGridView.Columns.Contains("Lớp Học Phần")) dataGridView.Columns["Lớp Học Phần"].Width = 150;
                if (dataGridView.Columns.Contains("Giảng Viên"))   dataGridView.Columns["Giảng Viên"].Width = 130;
                if (dataGridView.Columns.Contains("Ngày Yêu Cầu")) dataGridView.Columns["Ngày Yêu Cầu"].Width = 100;
                if (dataGridView.Columns.Contains("Trạng Thái"))   dataGridView.Columns["Trạng Thái"].Width = 90;
                if (dataGridView.Columns.Contains("Lý Do"))        dataGridView.Columns["Lý Do"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                lblTongBanGhi.Text = $"Tổng: {tblPhucKhao.Rows.Count} bản ghi";
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStatusColoring()
        {
            dataGridView.CellFormatting -= DataGridView_CellFormatting;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Trạng Thái" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Chờ duyệt")
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(230, 119, 0);
                else if (status == "Đã duyệt")
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
                else if (status == "Từ chối")
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
            }
        }

        private void CapNhatThongKe()
        {
            if (tblPhucKhao == null)
            {
                lblStatTongVal.Text = "0";
                lblStatChoDuyetVal.Text = "0";
                lblStatDaDuyetVal.Text = "0";
                lblStatTuChoiVal.Text = "0";
                return;
            }

            int choDuyet = tblPhucKhao.Select("[Trạng Thái] = 'Chờ duyệt'").Length;
            int daDuyet  = tblPhucKhao.Select("[Trạng Thái] = 'Đã duyệt'").Length;
            int tuChoi   = tblPhucKhao.Select("[Trạng Thái] = 'Từ chối'").Length;
            int tong     = choDuyet + daDuyet + tuChoi;

            lblStatTongVal.Text     = tong.ToString();
            lblStatChoDuyetVal.Text = choDuyet.ToString();
            lblStatDaDuyetVal.Text  = daDuyet.ToString();
            lblStatTuChoiVal.Text   = tuChoi.ToString();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            string maPK       = row.Cells["Mã PK"].Value?.ToString();
            string ngayYCStr   = row.Cells["Ngày Yêu Cầu"].Value?.ToString();
            string trangThai   = row.Cells["Trạng Thái"].Value?.ToString();
            string maSV        = row.Cells["_MaSV"].Value?.ToString();
            string maLHP       = row.Cells["_MaLHP"].Value?.ToString();
            string lyDo        = row.Cells["Lý Do"].Value?.ToString();

            currentMaPK = maPK;

            // Kích hoạt nút duyệt/từ chối tùy trạng thái
            if (trangThai == "Chờ duyệt")
            {
                btnDuyet.Enabled = true;
                btnTuChoi.Enabled = true;
            }
            else
            {
                btnDuyet.Enabled = false;
                btnTuChoi.Enabled = false;
            }

            ShowViewMode(maPK, maSV, maLHP, ngayYCStr, trangThai, lyDo);
        }

        private void ShowViewMode(string maPK, string maSV, string maLHP, string ngayYCStr, string trangThai, string lyDo)
        {
            lblDetailHeader.Text = "📄  Chi tiết Yêu cầu";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            cboSinhVien.SelectedValue = maSV;
            cboLopHocPhan.SelectedValue = maLHP;

            if (DateTime.TryParse(ngayYCStr, out DateTime dt))
                dtpNgayYeuCau.Value = dt;

            txtTrangThai.Text = trangThai;
            txtLyDo.Text = lyDo;

            // Inputs disabled for Admin read-only detail view
            cboSinhVien.Enabled = false;
            cboLopHocPhan.Enabled = false;
            dtpNgayYeuCau.Enabled = false;
            txtTrangThai.ReadOnly = true;
            txtLyDo.ReadOnly = true;

            btnLuuDetail.Visible = false;
            btnHuyDetail.Visible = false;
            btnCloseDetail.Visible = true;

            lblDetailStatus.Text = $"🔑 Đang xem: {maPK}";

            OpenSidebar();
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
                    dataGridView.ClearSelection();
                }
            };
            t.Start();
        }

        private void btnCloseDetail_Click(object sender, EventArgs e)
        {
            CloseSidebar();
        }

        private void btnHuyDetail_Click(object sender, EventArgs e)
        {
            CloseSidebar();
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần duyệt.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            string maPK = row.Cells["Mã PK"].Value?.ToString();
            string trangThai = row.Cells["Trạng Thái"].Value?.ToString();

            if (trangThai == "Đã duyệt")
            {
                MessageBox.Show("Yêu cầu này đã được duyệt trước đó.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn DUYỆT yêu cầu '{maPK}'?", "Xác nhận duyệt",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string sql = $"UPDATE PhucKhao SET TrangThai = N'Đã duyệt' WHERE MaPhucKhao = '{maPK}'";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Duyệt yêu cầu phúc khảo thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CloseSidebar();
                    TaiDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi duyệt yêu cầu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần từ chối.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            string maPK = row.Cells["Mã PK"].Value?.ToString();
            string trangThai = row.Cells["Trạng Thái"].Value?.ToString();

            if (trangThai == "Từ chối")
            {
                MessageBox.Show("Yêu cầu này đã bị từ chối trước đó.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn TỪ CHỐI yêu cầu '{maPK}'?", "Xác nhận từ chối",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string sql = $"UPDATE PhucKhao SET TrangThai = N'Từ chối' WHERE MaPhucKhao = '{maPK}'";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Từ chối yêu cầu phúc khảo thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CloseSidebar();
                    TaiDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi từ chối yêu cầu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu phúc khảo cần xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            string maPK = row.Cells["Mã PK"].Value?.ToString();

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn XÓA yêu cầu '{maPK}'?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string sql = $"DELETE FROM PhucKhao WHERE MaPhucKhao = '{maPK}'";
                    FunctionQa.RunSqlDel(sql);
                    CloseSidebar();
                    TaiDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa yêu cầu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm tên SV / lớp học phần...";
            txtTimKiem.ForeColor = Color.Gray;
            cboFilterTrangThai.SelectedIndex = 0;
            CloseSidebar();
            TaiDuLieu();
        }

        private void cboFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            if (kw != "Tìm tên SV / lớp học phần...")
            {
                TaiDuLieu();
            }
        }

        private void MakeLayoutResponsive()
        {
            try
            {
                // 1. Căn lề phải cho cụm tìm kiếm/lọc trong pnlToolbar
                if (pnlToolbar != null)
                {
                    int rightMargin = 16;

                    // txtTimKiem ở ngoài cùng bên phải
                    if (txtTimKiem != null)
                    {
                        txtTimKiem.Left = pnlToolbar.Width - txtTimKiem.Width - rightMargin;
                    }

                    // cboFilterTrangThai ở bên trái txtTimKiem
                    if (cboFilterTrangThai != null && txtTimKiem != null)
                    {
                        cboFilterTrangThai.Left = txtTimKiem.Left - cboFilterTrangThai.Width - 12;
                    }

                    // lblFilterTrangThai ở bên trái cboFilterTrangThai
                    if (lblFilterTrangThai != null && cboFilterTrangThai != null)
                    {
                        lblFilterTrangThai.Left = cboFilterTrangThai.Left - lblFilterTrangThai.Width - 8;
                    }
                }

                // 2. Phân phối tỷ lệ động cho các panel thống kê trong pnlStats
                if (pnlStats != null)
                {
                    int startX = 100;
                    if (lblThongKeCaption != null)
                    {
                        startX = lblThongKeCaption.Right + 12;
                    }

                    Panel[] statPanels = { pnlStatTong, pnlStatChoDuyet, pnlStatDaDuyet, pnlStatTuChoi };
                    int activeCount = 0;
                    foreach (var p in statPanels)
                    {
                        if (p != null && p.Visible) activeCount++;
                    }

                    if (activeCount > 0)
                    {
                        int availableWidth = pnlStats.Width - startX - 16;
                        int panelWidth = 120; // Chiều rộng cơ bản
                        int gap = 16;

                        int totalNeeded = (panelWidth * activeCount) + (gap * (activeCount - 1));
                        if (availableWidth >= totalNeeded)
                        {
                            // Mở rộng nhẹ panel khi có nhiều khoảng trống (lên đến 160px)
                            panelWidth = Math.Min(160, availableWidth / activeCount - gap);
                            if (panelWidth < 120) panelWidth = 120;

                            gap = (availableWidth - (panelWidth * activeCount)) / (activeCount - 1);
                            if (gap > 40) gap = 40; // Giới hạn khoảng cách tối đa
                        }
                        else
                        {
                            // Màn hình hẹp, thu nhỏ ô và khoảng cách để không bị đè lấp
                            gap = 8;
                            panelWidth = (availableWidth - (gap * (activeCount - 1))) / activeCount;
                            if (panelWidth < 70) panelWidth = 70;
                        }

                        int currentX = startX;
                        foreach (var p in statPanels)
                        {
                            if (p != null && p.Visible)
                            {
                                p.Left = currentX;
                                p.Width = panelWidth;
                                currentX += panelWidth + gap;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi thực thi MakeLayoutResponsive: " + ex.Message);
            }
        }
    }
}
