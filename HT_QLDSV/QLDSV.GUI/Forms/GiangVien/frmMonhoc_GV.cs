using QLDSV.BLL;
using QLDSV.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.GiangVien
{
    public partial class frmMonhoc_GV : Form, IShellChildForm
    {
        private readonly MonHocBLL bll = new MonHocBLL();
        private bool detailPanelVisible = false;
        private DataTable tblMonhoc;
        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }
        public frmMonhoc_GV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            BuildSidebarMenu();

            this.btnTim.Click += btnTim_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.dataGridView.CellClick += dataGridView_CellClick;
            // Dùng Shown thay vì Load: Shown bắn sau khi form thực sự visible
            // → DataGridView đã có layout → columns được tạo đúng
            this.Shown += frmMonhoc_GV_Shown;

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
            string[] icons = { "📚", "📝", "📈", "🔄" };

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
                        targetForm = new frmKetQuaHocTap();
                    else if (itemText == "Phúc khảo")
                        targetForm = new frmPhucKhao_GV();

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

        private void frmMonhoc_GV_Load(object sender, EventArgs e)
        {
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
            try
            {
                Connection.KetNoi();
                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMonhoc_GV_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadComboBoxes()
        {
            // Filter khoa không dùng cho GV — ẩn control nếu có
            //if (cboFilterKhoa != null) cboFilterKhoa.Visible = false;
        }

        private void LoadData()
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                // TenDangNhap = MaGV theo quy ước liên kết DB
                string maGV = SessionHelper.TenDangNhap;

                tblMonhoc = bll.GetDanhSachTheoGiangVien(maGV, keyword);
                dataGridView.DataSource = tblMonhoc;

                if (dataGridView.Columns.Contains("MaMon"))  dataGridView.Columns["MaMon"].HeaderText  = "Mã Môn";
                if (dataGridView.Columns.Contains("TenMon"))  dataGridView.Columns["TenMon"].HeaderText  = "Tên Môn Học";
                if (dataGridView.Columns.Contains("SoTC"))    dataGridView.Columns["SoTC"].HeaderText    = "Số Tín Chỉ";
                if (dataGridView.Columns.Contains("TenKhoa")) dataGridView.Columns["TenKhoa"].HeaderText = "Khoa Quản Lý";
                if (dataGridView.Columns.Contains("MoTa"))    dataGridView.Columns["MoTa"].HeaderText    = "Mô Tả";
                if (dataGridView.Columns.Contains("MaKhoa"))  dataGridView.Columns["MaKhoa"].Visible     = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblMonhoc?.Rows.Count ?? 0} môn học";
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
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string soTC = row.Cells["SoTC"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string moTa = row.Cells["MoTa"].Value?.ToString();

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
            lblDetailMaMon.Visible = true;
            lblDetailTenMon.Visible = true;
            lblDetailSoTC.Visible = true;
            lblDetailKhoa.Visible = true;
            lblDetailMoTa.Visible = true;

            // bỏ tất cả controls nhập liệu và nút ghi
            //txtEditMaMon.Visible = false;
            //txtEditTenMon.Visible = false;
            //txtEditSoTC.Visible = false;
            //cboEditKhoa.Visible = false;
            //txtEditMoTa.Visible = false;
            //chkEditActive.Visible = false;
            //btnLuuDetail.Visible = false;
            //btnHuyDetail.Visible = false;
            btnCloseDetail.Visible = true;

            // Gán giá trị
            lblDetailMaMon.Text = maMon ?? "";
            lblDetailTenMon.Text = tenMon ?? "";
            lblDetailSoTC.Text = soTC ?? "0";
            lblDetailKhoa.Text = tenKhoa ?? "";
            lblDetailMoTa.Text = moTa ?? "";

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

        //private void cboFilterKhoa_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadData();
        //}

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadData();
        }
    }
}
