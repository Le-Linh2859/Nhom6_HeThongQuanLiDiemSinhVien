using QLDSV.BLL;
using QLDSV.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class frmLophocphan_SV : Form, IShellChildForm
    {
        private readonly LopHocPhanBLL bll = new LopHocPhanBLL();
        private DataTable tblLophocphan;
        private bool detailPanelVisible = false;

        public frmLophocphan_SV()
        {
            InitializeComponent();
            btnLamMoi.Click += btnLamMoi_Click;
            btnTim.Click    += btnTim_Click;
            dataGridView.CellClick += dataGridView_CellClick;
            btnCloseDetail.Click   += (s, e) => CloseSidebar();
            this.Shown += frmLophocphan_SV_Shown;

            if (cboFilterKhoa != null) cboFilterKhoa.Visible = false;
            if (cboFilterMon  != null) cboFilterMon.Visible  = false;
            if (lblFilterKhoa != null) lblFilterKhoa.Visible = false;
            if (lblFilterMon  != null) lblFilterMon.Visible  = false;
        }

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader  != null) pnlHeader.Visible  = false;
        }

        private void frmLophocphan_SV_Load(object sender, EventArgs e)
        {
            pnlDetail.Width   = 0;
            pnlDetail.Visible = false;
            Connection.KetNoi();
        }

        private void frmLophocphan_SV_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string maSV    = SessionHelper.TenDangNhap; // TenDangNhap = MaSV theo quy ước liên kết DB
                string keyword = txtTimKiem?.Text.Trim() ?? "";

                tblLophocphan = bll.GetDanhSachTheoSinhVien(maSV);

                // Lọc theo keyword phía client nếu có
                if (!string.IsNullOrEmpty(keyword) && tblLophocphan != null)
                {
                    string kw = keyword.ToLower();
                    DataView dv = tblLophocphan.DefaultView;
                    dv.RowFilter = $"MaLHP LIKE '%{keyword}%' OR TenLopHocPhan LIKE '%{keyword}%'";
                    tblLophocphan = dv.ToTable();
                }

                dataGridView.DataSource = tblLophocphan;

                if (tblLophocphan == null || tblLophocphan.Columns.Count == 0)
                {
                    lblTongBanGhi.Text = "Tổng: 0 lớp học phần";
                    return;
                }

                if (dataGridView.Columns.Contains("MaLHP"))         dataGridView.Columns["MaLHP"].HeaderText         = "Mã LHP";
                if (dataGridView.Columns.Contains("TenLopHocPhan")) dataGridView.Columns["TenLopHocPhan"].HeaderText = "Tên lớp";
                if (dataGridView.Columns.Contains("ThoiGianHoc"))   dataGridView.Columns["ThoiGianHoc"].HeaderText   = "Thời gian";
                if (dataGridView.Columns.Contains("PhongHoc"))      dataGridView.Columns["PhongHoc"].HeaderText      = "Phòng";
                if (dataGridView.Columns.Contains("TenKhoa"))       dataGridView.Columns["TenKhoa"].HeaderText       = "Khoa";
                if (dataGridView.Columns.Contains("TenMon"))        dataGridView.Columns["TenMon"].HeaderText        = "Môn học";
                if (dataGridView.Columns.Contains("TenGiangVien"))  dataGridView.Columns["TenGiangVien"].HeaderText  = "Giảng viên";
                if (dataGridView.Columns.Contains("TrangThai"))     dataGridView.Columns["TrangThai"].HeaderText     = "Trạng thái";

                // Ẩn các cột khoá ngoài
                if (dataGridView.Columns.Contains("MaKhoa")) dataGridView.Columns["MaKhoa"].Visible = false;
                if (dataGridView.Columns.Contains("MaMon"))  dataGridView.Columns["MaMon"].Visible  = false;
                if (dataGridView.Columns.Contains("MaGV"))   dataGridView.Columns["MaGV"].Visible   = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode           = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode      = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblLophocphan?.Rows.Count ?? 0} lớp học phần";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp học phần: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            string maLHP    = row.Cells["MaLHP"].Value?.ToString();
            string tenLHP   = row.Cells["TenLopHocPhan"].Value?.ToString();
            string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
            string phong    = row.Cells["PhongHoc"].Value?.ToString();
            string tenKhoa  = row.Cells["TenKhoa"].Value?.ToString();
            string tenMon   = row.Cells["TenMon"].Value?.ToString();
            string tenGV    = row.Cells["TenGiangVien"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();

            ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, trangThai);
        }

        private void ShowViewMode(string maLHP, string tenLHP, string thoiGian,
            string phong, string tenKhoa, string tenMon, string tenGV, string trangThai)
        {
            lblDetailHeader.Text      = "Chi tiết Lớp Học Phần";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            lblDetailMaLHP.Visible      = true;
            lblDetailTenLHP.Visible     = true;
            lblDetailThoiGianHoc.Visible = true;
            lblDetailPhongHoc.Visible   = true;
            lblDetailKhoa.Visible       = true;
            lblDetailMon.Visible        = true;
            lblDetailGiangVien.Visible  = true;
            lblDetailTrangThai.Visible  = true;

            // Ẩn tất cả controls chỉnh sửa (SV chỉ xem)
            txtEditMaLHP.Visible    = false;
            txtEditTenLHP.Visible   = false;
            txtEditThoiGianHoc.Visible = false;
            txtEditPhongHoc.Visible = false;
            cboEditKhoa.Visible     = false;
            cboEditMon.Visible      = false;
            cboEditGiangVien.Visible = false;
            chkEditActive.Visible   = false;
            btnLuuDetail.Visible    = false;
            btnHuyDetail.Visible    = false;
            btnCloseDetail.Visible  = true;

            lblDetailMaLHP.Text       = maLHP    ?? "";
            lblDetailTenLHP.Text      = tenLHP   ?? "";
            lblDetailThoiGianHoc.Text = thoiGian ?? "";
            lblDetailPhongHoc.Text    = phong    ?? "";
            lblDetailKhoa.Text        = tenKhoa  ?? "";
            lblDetailMon.Text         = tenMon   ?? "";
            lblDetailGiangVien.Text   = tenGV    ?? "";
            lblDetailTrangThai.Text   = trangThai ?? "";

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
                    pnlDetail.Width   = 0;
                    pnlDetail.Visible = false;
                    t.Stop();
                    t.Dispose();
                    detailPanelVisible = false;
                    dataGridView.ClearSelection();
                }
            };
            t.Start();
        }

        private void btnTim_Click(object sender, EventArgs e)    => LoadData();
        private void btnLamMoi_Click(object sender, EventArgs e) { txtTimKiem.Text = ""; LoadData(); }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }
    }
}

