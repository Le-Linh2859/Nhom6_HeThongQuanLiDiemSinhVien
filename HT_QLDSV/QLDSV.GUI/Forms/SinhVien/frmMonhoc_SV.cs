using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLDSV.BLL;
using QLDSV.DAL;
using QLDSV.GUI.Forms.Admin;

namespace QLDSV.GUI
{
    /// <summary>
    /// Form xem danh sách môn học dành cho Sinh viên (VT003) và Giảng viên (VT002).
    /// Chỉ đọc — không có quyền Thêm, Sửa, Xóa.
    /// </summary>
    public partial class frmMonhoc_SV : Form, IShellChildForm
    {
        private readonly MonHocBLL bll = new MonHocBLL();
        private bool detailPanelVisible = false;
        private DataTable tblMonhoc;

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        public frmMonhoc_SV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            // Wire up toolbar events
            this.btnTim.Click += btnTim_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.cboFilterKhoa.SelectedIndexChanged += cboFilterKhoa_SelectedIndexChanged;

            // Logout button
            if (this.guna2Button7 != null)
            {
                this.guna2Button7.Click += (s, e) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        SessionHelper.Clear();
                        var loginForm = new frmDangNhap();
                        loginForm.Show();
                        this.Hide();
                    }
                };
            }
        }

        private void BuildSidebarMenu()
        {
            // Sinh viên chỉ thấy: Môn học, Đăng ký lớp, Kết quả học tập, Phúc khảo
            string[] menuItems = { "Môn học", "Đăng ký lớp", "Kết quả học tập", "Phúc khảo" };
            string[] icons     = { "📚",      "📝",          "📈",              "🔄" };

            int startY = 55;
            for (int i = 0; i < menuItems.Length; i++)
            {
                var btn = new Guna.UI2.WinForms.Guna2Button();
                btn.FillColor = System.Drawing.Color.Transparent;
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Size = new System.Drawing.Size(180, 32);
                btn.Location = new System.Drawing.Point(8, startY + i * 34);
                btn.Text = "  " + icons[i] + "  " + menuItems[i];
                btn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                btn.Cursor = System.Windows.Forms.Cursors.Hand;

                if (menuItems[i] == "Môn học")
                {
                    btn.FillColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                    btn.BorderRadius = 5;
                }

                string itemText = menuItems[i];
                btn.Click += (s, ev) =>
                {
                    Form targetForm = null;
                    if (itemText == "Đăng ký lớp")
                        targetForm = new frmDangKyHocPhan();
                    else if (itemText == "Kết quả học tập")
                        targetForm = new frmTheoDoiDiem();
                    else if (itemText == "Phúc khảo")
                        if (SessionHelper.MaVaiTro == "VT001")
                            targetForm = new QLDSV.GUI.Forms.Admin.frmPhucKhao_Admin();
                        else if (SessionHelper.MaVaiTro == "VT002")
                            targetForm = new QLDSV.GUI.Forms.GiangVien.frmPhucKhao_GV();
                        else if (SessionHelper.MaVaiTro == "VT003")
                            targetForm = new QLDSV.GUI.Forms.SinhVien.frmPhucKhao_SV();

                    if (targetForm != null)
                    {
                        targetForm.FormClosed += (sf, ef) => this.Show();
                        targetForm.Show();
                        this.Hide();
                    }
                };

                pnlSidebar.Controls.Add(btn);
            }
        }

        private void frmMonhoc_SV_Load(object sender, EventArgs e)
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

        private void LoadComboBoxes()
        {
            try
            {
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
                    maKhoa = cboFilterKhoa.SelectedValue.ToString();

                tblMonhoc = bll.GetMonHocList(keyword, maKhoa);
                dataGridView.DataSource = tblMonhoc;

                if (dataGridView.Columns.Contains("MaMon"))   dataGridView.Columns["MaMon"].HeaderText   = "Mã Môn";
                if (dataGridView.Columns.Contains("TenMon"))  dataGridView.Columns["TenMon"].HeaderText  = "Tên Môn Học";
                if (dataGridView.Columns.Contains("SoTC"))    dataGridView.Columns["SoTC"].HeaderText    = "Số Tín Chỉ";
                if (dataGridView.Columns.Contains("TenKhoa")) dataGridView.Columns["TenKhoa"].HeaderText = "Khoa Quản Lý";
                if (dataGridView.Columns.Contains("MoTa"))    dataGridView.Columns["MoTa"].HeaderText    = "Mô Tả";
                if (dataGridView.Columns.Contains("MaKhoa"))  dataGridView.Columns["MaKhoa"].Visible     = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblMonhoc.Rows.Count} môn học";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn học: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            string maMon  = row.Cells["MaMon"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string soTC   = row.Cells["SoTC"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string moTa   = row.Cells["MoTa"].Value?.ToString();

            ShowViewMode(maMon, tenMon, soTC, tenKhoa, moTa);
        }

        /// <summary>
        /// Hiển thị panel chi tiết ở chế độ chỉ đọc (View Mode).
        /// Các control nhập liệu và nút lưu luôn ẩn với vai trò SV/GV.
        /// </summary>
        private void ShowViewMode(string maMon, string tenMon, string soTC, string tenKhoa, string moTa)
        {
            lblDetailHeader.Text = "Chi tiết Môn Học";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Hiển thị labels chỉ đọc
            lblDetailMaMon.Visible  = true;
            lblDetailTenMon.Visible = true;
            lblDetailSoTC.Visible   = true;
            lblDetailKhoa.Visible   = true;
            lblDetailMoTa.Visible   = true;

            // Ẩn tất cả controls nhập liệu và nút ghi
            txtEditMaMon.Visible  = false;
            txtEditTenMon.Visible = false;
            txtEditSoTC.Visible   = false;
            cboEditKhoa.Visible   = false;
            txtEditMoTa.Visible   = false;
            chkEditActive.Visible = false;
            btnLuuDetail.Visible  = false;
            btnHuyDetail.Visible  = false;
            btnCloseDetail.Visible = true;

            // Gán giá trị
            lblDetailMaMon.Text  = maMon  ?? "";
            lblDetailTenMon.Text = tenMon ?? "";
            lblDetailSoTC.Text   = soTC   ?? "0";
            lblDetailKhoa.Text   = tenKhoa ?? "";
            lblDetailMoTa.Text   = moTa   ?? "";

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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            cboFilterKhoa.SelectedIndex = 0;
            LoadData();
        }
    }
}
