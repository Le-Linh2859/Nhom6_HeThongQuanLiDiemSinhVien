using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmKhoa : Form, IShellChildForm
    {
        private bool detailPanelVisible = false;
        private bool isEditMode = false;        // false = View, true = Add/Edit
        private bool isAddingNew = false;       // true = Thêm mới, false = Sửa

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        public frmKhoa()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up toolbar buttons programmatically
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnXoa.Click += btnXoa_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.btnTim.Click += btnTim_Click;

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
            
            // Filter menu items based on logged-in user's role
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
                if (visibleItems[i] == "Tổng quan")
                {
                    btn.FillColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                    btn.BorderRadius = 5;
                }
                
                // Add click handler for navigation
                string itemText = visibleItems[i];
                btn.Click += (s, ev) =>
                {
                    Form targetForm = null;
                    if (itemText == "Giảng viên")
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

        // ─── Load Form ────────────────────────────────────────────────────────
        private void frmKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message +
                    "\n\nVui lòng kiểm tra lại chuỗi kết nối trong FunctionQa.cs",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
        }

        // ─── Load DataGridView ────────────────────────────────────────────────
        private DataTable tblKhoa;

        private void LoadData()
        {
            string sql = "SELECT MaKhoa, TenKhoa, NamThanhLap, MoTa FROM Khoa";
            tblKhoa = FunctionQa.getdatatotable(sql);
            dataGridView.DataSource = tblKhoa;

            if (dataGridView.Columns.Count >= 4)
            {
                dataGridView.Columns[0].HeaderText = "Mã Khoa";
                dataGridView.Columns[1].HeaderText = "Tên Khoa";
                dataGridView.Columns[2].HeaderText = "Năm TL";
                dataGridView.Columns[3].HeaderText = "Mô Tả";
            }

            dataGridView.AllowUserToAddRows = false;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblTongBanGhi.Text = $"Tổng: {tblKhoa.Rows.Count} bản ghi";
        }

        // ─── Click row → View Mode ────────────────────────────────────────────
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isEditMode) return;     // Không cho chuyển row khi đang nhập liệu

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            ShowViewMode(
                row.Cells[0].Value?.ToString(),
                row.Cells[1].Value?.ToString(),
                row.Cells[2].Value?.ToString(),
                row.Cells[3].Value?.ToString()
            );
        }

        // ─── SIDEBAR: Show View Mode ──────────────────────────────────────────
        private void ShowViewMode(string maKhoa, string tenKhoa, string namTL, string moTa)
        {
            isEditMode = false;
            isAddingNew = false;

            lblDetailHeader.Text = "Chi tiết Khoa";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Hiển thị labels, ẩn TextBoxes và buttons Save/Cancel
            lblDetailMaKhoa.Visible = true;
            lblDetailTenKhoa.Visible = true;
            lblDetailNamTL.Visible = true;
            lblDetailMoTa.Visible = true;

            txtEditMaKhoa.Visible = false;
            txtEditTenKhoa.Visible = false;
            txtEditNamTL.Visible = false;
            txtEditMoTa.Visible = false;
            btnLuuDetail.Visible = false;
            btnHuyDetail.Visible = false;
            btnCloseDetail.Visible = true;

            // Nạp dữ liệu vào Labels
            lblDetailMaKhoa.Text = maKhoa ?? "";
            lblDetailTenKhoa.Text = tenKhoa ?? "";
            lblDetailNamTL.Text = namTL ?? "";
            lblDetailMoTa.Text = moTa ?? "";

            OpenSidebar();
        }

        // ─── SIDEBAR: Show Edit Mode ──────────────────────────────────────────
        private void ShowEditMode(string maKhoa = "", string tenKhoa = "", string namTL = "", string moTa = "")
        {
            isEditMode = true;

            lblDetailHeader.Text = isAddingNew ? "✚  Thêm Khoa mới" : "✎  Chỉnh sửa Khoa";
            lblDetailHeader.ForeColor = isAddingNew
                ? Color.FromArgb(27, 120, 53)
                : Color.FromArgb(180, 80, 0);

            // Ẩn labels, hiện TextBoxes và buttons
            lblDetailMaKhoa.Visible = false;
            lblDetailTenKhoa.Visible = false;
            lblDetailNamTL.Visible = false;
            lblDetailMoTa.Visible = false;

            txtEditMaKhoa.Visible = true;
            txtEditTenKhoa.Visible = true;
            txtEditNamTL.Visible = true;
            txtEditMoTa.Visible = true;
            btnLuuDetail.Visible = true;
            btnHuyDetail.Visible = true;
            btnCloseDetail.Visible = false;

            // Nạp dữ liệu vào TextBoxes
            txtEditMaKhoa.Text = maKhoa;
            txtEditTenKhoa.Text = tenKhoa;
            txtEditNamTL.Text = namTL;
            txtEditMoTa.Text = moTa;

            // Khi Thêm mới: MaKhoa được nhập tự do; Khi Sửa: khóa MaKhoa lại
            txtEditMaKhoa.ReadOnly = !isAddingNew;
            txtEditMaKhoa.BackColor = isAddingNew ? Color.White : Color.FromArgb(240, 240, 240);

            OpenSidebar();
            txtEditTenKhoa.Focus();
        }

        // ─── Animate Sidebar Open ─────────────────────────────────────────────
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

        // ─── Animate Sidebar Close ────────────────────────────────────────────
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

        // ─── Nút ✕ Đóng Sidebar ───────────────────────────────────────────────
        private void btnCloseDetail_Click(object sender, EventArgs e)
        {
            CloseSidebar();
        }

        // ─── Nút Thêm ─────────────────────────────────────────────────────────
        private void btnThem_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            dataGridView.ClearSelection();
            ShowEditMode();
        }

        // ─── Nút Sửa ──────────────────────────────────────────────────────────
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khoa cần sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            isAddingNew = false;
            ShowEditMode(
                row.Cells[0].Value?.ToString(),
                row.Cells[1].Value?.ToString(),
                row.Cells[2].Value?.ToString(),
                row.Cells[3].Value?.ToString()
            );
        }

        // ─── Nút Xóa ──────────────────────────────────────────────────────────
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKhoa = dataGridView.SelectedRows[0].Cells[0].Value?.ToString();
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa khoa '{maKhoa}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                FunctionQa.RunSqlDel($"DELETE FROM Khoa WHERE MaKhoa = '{maKhoa}'");
                CloseSidebar();
                LoadData();
            }
        }

        // ─── Nút Làm mới ──────────────────────────────────────────────────────
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadData();
        }

        // ─── Nút Tìm ──────────────────────────────────────────────────────────
        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            string sql = $"SELECT MaKhoa, TenKhoa, NamThanhLap, MoTa FROM Khoa " +
                         $"WHERE MaKhoa LIKE '%{keyword}%' OR TenKhoa LIKE '%{keyword}%'";
            tblKhoa = FunctionQa.getdatatotable(sql);
            dataGridView.DataSource = tblKhoa;
            lblTongBanGhi.Text = $"Tổng: {tblKhoa.Rows.Count} bản ghi";
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTim_Click(null, null);
        }

        // ─── Nút 💾 Lưu (trong Sidebar) ───────────────────────────────────────
        private void btnLuuDetail_Click(object sender, EventArgs e)
        {
            string ma = txtEditMaKhoa.Text.Trim();
            string ten = txtEditTenKhoa.Text.Trim();
            string nam = txtEditNamTL.Text.Trim();
            string moTa = txtEditMoTa.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Mã Khoa và Tên Khoa không được để trống.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(nam) && !int.TryParse(nam, out _))
            {
                MessageBox.Show("Năm thành lập phải là số nguyên.", "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditNamTL.Focus();
                return;
            }

            int namInt = string.IsNullOrEmpty(nam) ? 0 : int.Parse(nam);

            try
            {
                if (isAddingNew)
                {
                    // Kiểm tra trùng mã
                    if (FunctionQa.checkkey($"SELECT 1 FROM Khoa WHERE MaKhoa = '{ma}'"))
                    {
                        MessageBox.Show($"Mã Khoa '{ma}' đã tồn tại. Vui lòng chọn mã khác.",
                            "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEditMaKhoa.Focus();
                        return;
                    }

                    string sql = $"INSERT INTO Khoa (MaKhoa, TenKhoa, NamThanhLap, MoTa) " +
                                 $"VALUES ('{ma}', N'{ten}', {(namInt > 0 ? namInt.ToString() : "NULL")}, N'{moTa}')";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Thêm khoa mới thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = $"UPDATE Khoa SET TenKhoa = N'{ten}', " +
                                 $"NamThanhLap = {(namInt > 0 ? namInt.ToString() : "NULL")}, " +
                                 $"MoTa = N'{moTa}' " +
                                 $"WHERE MaKhoa = '{ma}'";
                    FunctionQa.runsql(sql);
                    MessageBox.Show("Cập nhật thông tin khoa thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();

                // Sau khi lưu: chuyển sang View Mode với dữ liệu vừa lưu
                detailPanelVisible = false;
                ShowViewMode(ma, ten, namInt > 0 ? namInt.ToString() : "", moTa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Nút ✕ Hủy (trong Sidebar) ────────────────────────────────────────
        private void btnHuyDetail_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                // Nếu đang thêm mới → đóng sidebar hoàn toàn
                CloseSidebar();
            }
            else
            {
                // Nếu đang sửa → quay về chế độ Xem với dữ liệu cũ
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    detailPanelVisible = true;  // Giữ sidebar mở
                    ShowViewMode(
                        row.Cells[0].Value?.ToString(),
                        row.Cells[1].Value?.ToString(),
                        row.Cells[2].Value?.ToString(),
                        row.Cells[3].Value?.ToString()
                    );
                }
                else
                {
                    CloseSidebar();
                }
            }
        }
    }
}
