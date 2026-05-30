using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class frmPhucKhao_SV : Form, IShellChildForm
    {
        private bool detailPanelVisible = false;
        private bool isAddingNew = false;
        private string currentMaPK = "";
        private DataTable tblPhucKhao;
        private string maSVHienTai = "";

        public void OnEmbeddedInShell()
        {
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

        public frmPhucKhao_SV()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up event handlers programmatically
            this.Resize += (s, e) => MakeLayoutResponsive();
            this.Load += frmPhucKhao_SV_Load;
            this.btnThem.Click += btnThem_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.btnCloseDetail.Click += btnCloseDetail_Click;
            this.btnLuuDetail.Click += btnLuuDetail_Click;
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

            // Search text box placeholder
            txtTimKiem.Text = "Tìm lớp học phần...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm lớp học phần...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm lớp học phần...";
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
                "Đăng ký lớp", "Kết quả học tập", "Phúc khảo"
            };
            string[] icons = { "📝", "📈", "🔄" };

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
                    if (itemText == "Đăng ký lớp")
                    {
                        targetForm = new frmDangKyHocPhan();
                    }
                    else if (itemText == "Kết quả học tập")
                    {
                        targetForm = new QLDSV.GUI.Forms.SinhVien.KetQuaHocTap();
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

        private void frmPhucKhao_SV_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();

                // Get MaSV associated with this account
                maSVHienTai = FunctionQa.getfieldvalue(
                    $"SELECT MaSV FROM SinhVien WHERE MaTaiKhoan = '{SessionHelper.MaTaiKhoan}'");
                
                if (string.IsNullOrEmpty(maSVHienTai))
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên liên kết với tài khoản này.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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
                MessageBox.Show("Lỗi khởi tạo màn hình sinh viên: " + ex.Message,
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
                if (string.IsNullOrEmpty(maSVHienTai))
                {
                    dataGridView.DataSource = null;
                    CapNhatThongKe();
                    return;
                }

                // Sinh viên xem đơn phúc khảo của chính mình
                // Cột: Mã PK, Lớp HP, Tên Giảng Viên phụ trách, Ngày YC, Trạng Thái, Lý Do
                string sql = "SELECT pk.MaPhucKhao AS [Mã PK], " +
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
                             $"WHERE pk.MaSV = '{maSVHienTai}'";

                string keyword = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm lớp học phần...")
                {
                    sql += $" AND (lhp.TenLopHocPhan LIKE N'%{keyword}%' OR pk.MaPhucKhao LIKE '%{keyword}%')";
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

                // Ẩn cột helper
                if (dataGridView.Columns.Contains("_MaSV"))
                    dataGridView.Columns["_MaSV"].Visible = false;
                if (dataGridView.Columns.Contains("_MaLHP"))
                    dataGridView.Columns["_MaLHP"].Visible = false;

                // Tô màu trạng thái
                dataGridView.CellFormatting -= DataGridView_CellFormatting;
                dataGridView.CellFormatting += DataGridView_CellFormatting;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Căn chỉnh độ rộng cột
                if (dataGridView.Columns.Contains("Mã PK"))        dataGridView.Columns["Mã PK"].Width = 70;
                if (dataGridView.Columns.Contains("Lớp Học Phần")) dataGridView.Columns["Lớp Học Phần"].Width = 180;
                if (dataGridView.Columns.Contains("Giảng Viên"))    dataGridView.Columns["Giảng Viên"].Width = 140;
                if (dataGridView.Columns.Contains("Ngày Yêu Cầu")) dataGridView.Columns["Ngày Yêu Cầu"].Width = 100;
                if (dataGridView.Columns.Contains("Trạng Thái"))    dataGridView.Columns["Trạng Thái"].Width = 90;
                if (dataGridView.Columns.Contains("Lý Do"))         dataGridView.Columns["Lý Do"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                lblTongBanGhi.Text = $"Tổng đơn: {tblPhucKhao.Rows.Count} bản ghi";
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu cá nhân: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (isAddingNew) return; // Prevent row changing while adding

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            string maPK       = row.Cells["Mã PK"].Value?.ToString();
            string ngayYCStr   = row.Cells["Ngày Yêu Cầu"].Value?.ToString();
            string trangThai   = row.Cells["Trạng Thái"].Value?.ToString();
            string maSV        = row.Cells["_MaSV"].Value?.ToString();
            string maLHP       = row.Cells["_MaLHP"].Value?.ToString();
            string lyDo        = row.Cells["Lý Do"].Value?.ToString();

            currentMaPK = maPK;
            ShowViewMode(maPK, maSV, maLHP, ngayYCStr, trangThai, lyDo);
        }

        private void ShowViewMode(string maPK, string maSV, string maLHP, string ngayYCStr, string trangThai, string lyDo)
        {
            isAddingNew = false;

            lblDetailHeader.Text = "📄  Chi tiết Yêu cầu";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            cboSinhVien.SelectedValue = maSV;
            cboLopHocPhan.SelectedValue = maLHP;

            if (DateTime.TryParse(ngayYCStr, out DateTime dt))
                dtpNgayYeuCau.Value = dt;

            txtTrangThai.Text = trangThai;
            txtLyDo.Text = lyDo;

            // Inputs disabled for read-only view mode
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

        private void ShowEditMode(string maPK = "")
        {
            isAddingNew = true;

            lblDetailHeader.Text = "✚  Thêm yêu cầu mới";
            lblDetailHeader.ForeColor = Color.FromArgb(46, 125, 50);

            cboSinhVien.SelectedValue = maSVHienTai;
            cboLopHocPhan.SelectedIndex = -1;
            dtpNgayYeuCau.Value = DateTime.Today;
            txtTrangThai.Text = "Chờ duyệt";
            txtLyDo.Text = "";

            // Enable appropriate controls for student input
            cboSinhVien.Enabled = false; // Always locked to self
            cboLopHocPhan.Enabled = true;
            dtpNgayYeuCau.Enabled = true;
            txtTrangThai.ReadOnly = true;
            txtLyDo.ReadOnly = false;

            btnLuuDetail.Visible = true;
            btnHuyDetail.Visible = true;
            btnCloseDetail.Visible = false;

            lblDetailStatus.Text = $"➕ Đang thêm mới... {maPK}";

            OpenSidebar();
            cboLopHocPhan.Focus();
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

        private void btnHuyDetail_Click(object sender, EventArgs e)
        {
            CloseSidebar();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maSVHienTai))
            {
                MessageBox.Show("Không tìm thấy thông tin sinh viên, không thể gửi yêu cầu.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAddingNew = true;
            dataGridView.ClearSelection();
            string nextMaPK = GenerateNewMaPhucKhao();
            currentMaPK = nextMaPK;
            ShowEditMode(nextMaPK);
        }

        private string GenerateNewMaPhucKhao()
        {
            try
            {
                string sql = "SELECT MaPhucKhao FROM PhucKhao ORDER BY MaPhucKhao DESC";
                DataTable dt = FunctionQa.getdatatotable(sql);
                if (dt.Rows.Count == 0)
                {
                    return "PK001";
                }

                int maxVal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["MaPhucKhao"].ToString().Trim();
                    if (id.StartsWith("PK") && int.TryParse(id.Substring(2), out int val))
                    {
                        if (val > maxVal)
                        {
                            maxVal = val;
                        }
                    }
                }
                return "PK" + (maxVal + 1).ToString("D3");
            }
            catch
            {
                return "PK001";
            }
        }

        private void btnLuuDetail_Click(object sender, EventArgs e)
        {
            if (cboLopHocPhan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần phúc khảo.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLopHocPhan.Focus();
                return;
            }

            string lyDo = txtLyDo.Text.Trim();
            if (string.IsNullOrEmpty(lyDo))
            {
                MessageBox.Show("Vui lòng nhập lý do phúc khảo cụ thể.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLyDo.Focus();
                return;
            }

            string maLHP = cboLopHocPhan.SelectedValue.ToString();

            // Kiểm tra xem sinh viên đã có điểm nào của lớp học phần này trên hệ thống chưa
            string sqlCheckDiem = $"SELECT COUNT(*) FROM KetQua WHERE MaSV = '{maSVHienTai}' AND MaLHP = '{maLHP}'";
            int countDiem = 0;
            int.TryParse(FunctionQa.getfieldvalue(sqlCheckDiem), out countDiem);

            if (countDiem == 0)
            {
                MessageBox.Show("Sinh viên chỉ có thể phúc khảo khi đã có đầy đủ điểm hoặc có cập nhật điểm trên hệ thống.", 
                    "Không thể phúc khảo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ngayYC = dtpNgayYeuCau.Value.ToString("yyyy-MM-dd");
            string trangThai = txtTrangThai.Text;

            try
            {
                if (isAddingNew)
                {
                    string maPK = GenerateNewMaPhucKhao();

                    string sql = $"INSERT INTO PhucKhao (MaPhucKhao, MaSV, MaLHP, NgayYeuCau, TrangThai, LyDo) " +
                                 $"VALUES ('{maPK}', '{maSVHienTai}', '{maLHP}', '{ngayYC}', N'{trangThai}', N'{lyDo}')";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Gửi đơn phúc khảo mới thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TaiDuLieu();
                    ShowViewMode(maPK, maSVHienTai, maLHP, ngayYC, trangThai, lyDo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi yêu cầu phúc khảo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm lớp học phần...";
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
            if (kw != "Tìm lớp học phần...")
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
