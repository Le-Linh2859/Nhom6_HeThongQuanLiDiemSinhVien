using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLDSV.BLL;
using QLDSV.DAL;

namespace QLDSV.GUI
{
    public partial class frmMonhoc : Form, IShellChildForm
    {
        private MonHocBLL bll = new MonHocBLL();
        private bool detailPanelVisible = false;
        private bool isEditMode = false;        // false = View, true = Add/Edit
        private bool isAddingNew = false;       // true = Thêm mới, false = Sửa

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        public frmMonhoc()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up toolbar events
            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnLamMoi.Click += btnXoa_Click;
            this.btnXoa.Click += btnLamMoi_Click;
            this.btnTim.Click += btnTim_Click;
            this.cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;

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
                    if (menuItems[i] == "Tổng quan" || menuItems[i] == "Giảng viên" || menuItems[i] == "Lớp niên chế")
                    {
                        isVisible = false;
                    }
                }
                else if (role == "VT003") // Sinh viên
                {
                    if (menuItems[i] != "Đăng ký lớp" && menuItems[i] != "Kết quả học tập" && menuItems[i] != "Phúc khảo" && menuItems[i] != "Môn học")
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
                
                if (visibleItems[i] == "Môn học")
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
                    else if (itemText == "Lớp niên chế")
                    {
                        targetForm = new FrmLopNienChe();
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

        private void frmMonhoc_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();
                LoadComboBoxes();
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

        private DataTable tblMonhoc;

        private void LoadComboBoxes()
        {
            try
            {
                // Nạp combo Khoa ở toolbar bộ lọc
                DataTable dtKhoa = bll.GetKhoa();
                DataTable dtFilter = dtKhoa.Copy();
                DataRow rAll = dtFilter.NewRow();
                rAll["MaKhoa"] = "ALL";
                rAll["TenKhoa"] = "--- Tất cả ---";
                dtFilter.Rows.InsertAt(rAll, 0);

                cboFilterKhoa.DataSource = dtFilter;
                cboFilterKhoa.ValueMember = "MaKhoa";
                cboFilterKhoa.DisplayMember = "TenKhoa";
                cboFilterKhoa.SelectedIndex = 0;

                // Nạp combo Khoa ở detail panel nhập liệu
                DataTable dtEdit = dtKhoa.Copy();
                cboEditKhoa.DataSource = dtEdit;
                cboEditKhoa.ValueMember = "MaKhoa";
                cboEditKhoa.DisplayMember = "TenKhoa";
                cboEditKhoa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi tải ComboBox Khoa: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                string maKhoa = "ALL";
                if (cboFilterKhoa.SelectedValue != null)
                {
                    maKhoa = cboFilterKhoa.SelectedValue.ToString();
                }

                tblMonhoc = bll.GetMonHocList(keyword, maKhoa);
                dataGridView.DataSource = tblMonhoc;

                if (dataGridView.Columns.Contains("MaMon")) dataGridView.Columns["MaMon"].HeaderText = "Mã Môn";
                if (dataGridView.Columns.Contains("TenMon")) dataGridView.Columns["TenMon"].HeaderText = "Tên Môn Học";
                if (dataGridView.Columns.Contains("SoTC")) dataGridView.Columns["SoTC"].HeaderText = "Số Tín Chỉ";
                if (dataGridView.Columns.Contains("TenKhoa")) dataGridView.Columns["TenKhoa"].HeaderText = "Khoa Quản Lý";
                if (dataGridView.Columns.Contains("MoTa")) dataGridView.Columns["MoTa"].HeaderText = "Mô Tả";
                if (dataGridView.Columns.Contains("MaKhoa")) dataGridView.Columns["MaKhoa"].Visible = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblMonhoc.Rows.Count} môn học";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (isEditMode) return; // Không cho chuyển row khi đang nhập liệu

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string soTC = row.Cells["SoTC"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string moTa = row.Cells["MoTa"].Value?.ToString();
           // string activeStr = "Hoạt động"; // Default value since column doesn't exist
            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();

            ShowViewMode(maMon, tenMon, soTC, tenKhoa, moTa, maKhoa);
        }

        private void ShowViewMode(string maMon, string tenMon, string soTC, string tenKhoa, string moTa, string maKhoa)
        {
            isEditMode = false;
            isAddingNew = false;

            lblDetailHeader.Text = "Chi tiết Môn Học";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Toggle Visibility
            lblDetailMaMon.Visible = true;
            lblDetailTenMon.Visible = true;
            lblDetailSoTC.Visible = true;
            lblDetailKhoa.Visible = true;
            lblDetailMoTa.Visible = true;
            //lblDetailTrangThai.Visible = true;

            txtEditMaMon.Visible = false;
            txtEditTenMon.Visible = false;
            txtEditSoTC.Visible = false;
            cboEditKhoa.Visible = false;
            txtEditMoTa.Visible = false;
            //chkEditActive.Visible = false;
            btnLuuDetail.Visible = false;
            btnHuyDetail.Visible = false;
            btnCloseDetail.Visible = true;

            // Load Values
            lblDetailMaMon.Text = maMon ?? "";
            lblDetailTenMon.Text = tenMon ?? "";
            lblDetailSoTC.Text = soTC ?? "0";
            lblDetailKhoa.Text = tenKhoa ?? "";
            lblDetailMoTa.Text = moTa ?? "";
            //lblDetailTrangThai.Text = activeStr ?? "Hoạt động";

            OpenSidebar();
        }

        private void ShowEditMode(string maMon = "", string tenMon = "", string soTC = "", string maKhoa = "", string moTa = "", bool isActive = true)
        {
            isEditMode = true;

            lblDetailHeader.Text = isAddingNew ? "✚  Thêm Môn mới" : "✎  Chỉnh sửa Môn";
            lblDetailHeader.ForeColor = isAddingNew ? Color.FromArgb(27, 120, 53) : Color.FromArgb(180, 80, 0);

            // Toggle Visibility
            lblDetailMaMon.Visible = false;
            lblDetailTenMon.Visible = false;
            lblDetailSoTC.Visible = false;
            lblDetailKhoa.Visible = false;
            lblDetailMoTa.Visible = false;
            lblDetailTrangThai.Visible = false;

            txtEditMaMon.Visible = true;
            txtEditTenMon.Visible = true;
            txtEditSoTC.Visible = true;
            cboEditKhoa.Visible = true;
            txtEditMoTa.Visible = true;
            chkEditActive.Visible = false;
            btnLuuDetail.Visible = true;
            btnHuyDetail.Visible = true;
            btnCloseDetail.Visible = false;

            // Load values to inputs
            txtEditMaMon.Text = maMon;
            txtEditTenMon.Text = tenMon;
            txtEditSoTC.Text = soTC;
            txtEditMoTa.Text = moTa;
            //chkEditActive.Checked = isActive;

            if (!string.IsNullOrEmpty(maKhoa))
                cboEditKhoa.SelectedValue = maKhoa;
            else
                cboEditKhoa.SelectedIndex = -1;

            txtEditMaMon.ReadOnly = !isAddingNew;
            txtEditMaMon.BackColor = isAddingNew ? Color.White : Color.FromArgb(240, 240, 240);

            OpenSidebar();
            txtEditTenMon.Focus();
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
                MessageBox.Show("Vui lòng chọn môn học cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView.SelectedRows[0];
            isAddingNew = false;
            
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string soTC = row.Cells["SoTC"].Value?.ToString();
            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string moTa = row.Cells["MoTa"].Value?.ToString();
            bool isActive = true; // Giá trị mặc định do cột TrangThai không tồn tại trong DB

            ShowEditMode(maMon, tenMon, soTC, maKhoa, moTa, isActive);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMon = dataGridView.SelectedRows[0].Cells["MaMon"].Value?.ToString();
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn học '{maMon}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bll.DeleteMonHoc(maMon);
                    CloseSidebar();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            cboFilterKhoa.SelectedIndex = 0;
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
            LoadData();
        }

        private void btnLuuDetail_Click(object sender, EventArgs e)
        {
            string ma = txtEditMaMon.Text.Trim();
            string ten = txtEditTenMon.Text.Trim();
            string tcStr = txtEditSoTC.Text.Trim();
            string moTa = txtEditMoTa.Text.Trim();
            //int active = chkEditActive.Checked ? 1 : 0;

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(tcStr))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc (*).", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tcStr, out int soTC) || soTC <= 0)
            {
                MessageBox.Show("Số tín chỉ phải là số nguyên dương lớn hơn 0.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditSoTC.Focus();
                return;
            }

            if (cboEditKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa quản lý.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEditKhoa.Focus();
                return;
            }

            string maKhoa = cboEditKhoa.SelectedValue.ToString();

            try
            {
                if (isAddingNew)
                {
                    // Check duplicate
                    if (bll.CheckKeyExist(ma))
                    {
                        MessageBox.Show($"Mã môn học '{ma}' đã tồn tại trong hệ thống. Vui lòng chọn mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEditMaMon.Focus();
                        return;
                    }

                    bll.InsertMonHoc(ma, ten, soTC, maKhoa, moTa);
                    MessageBox.Show("Thêm môn học mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bll.UpdateMonHoc(ma, ten, soTC, maKhoa, moTa);
                    MessageBox.Show("Cập nhật thông tin môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                detailPanelVisible = false;

                // Show view mode of the saved entry
                //string activeStr = (active == 1) ? "Hoạt động" : "Khóa";
                string tenKhoa = cboEditKhoa.Text;
                // ShowViewMode(ma, ten, soTC.ToString(), tenKhoa, moTa, activeStr, maKhoa);
                ShowViewMode(ma, ten, soTC.ToString(), tenKhoa, moTa, maKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    
                    string maMon = row.Cells["MaMon"].Value?.ToString();
                    string tenMon = row.Cells["TenMon"].Value?.ToString();
                    string soTC = row.Cells["SoTC"].Value?.ToString();
                    string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
                    string moTa = row.Cells["MoTa"].Value?.ToString();
                    //string activeStr = row.Cells["TrangThai"].Value?.ToString();
                    string maKhoa = row.Cells["MaKhoa"].Value?.ToString();

                    ShowViewMode(maMon, tenMon, soTC, tenKhoa, moTa, maKhoa);
                }
                else
                {
                    CloseSidebar();
                }
            }
        }
    }
}
