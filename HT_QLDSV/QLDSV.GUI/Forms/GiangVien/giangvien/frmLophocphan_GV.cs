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
//using System.Windows;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.GiangVien
{
    public partial class frmLophocphan_GV : Form, IShellChildForm
    {
        private LopHocPhanBLL bll = new LopHocPhanBLL();

        private DataTable tblLophocphan;

        private bool detailPanelVisible = false;
       
        //private string currentMaLHP = "";
        public frmLophocphan_GV()
        {
            InitializeComponent();
            btnLamMoi.Click += btnLamMoi_Click;
            btnTim.Click += btnTim_Click;
            cboFilterMon.SelectedIndexChanged += cboFilterMon_SelectedIndexChanged;
            this.Shown += frmLophocphan_GV_Shown;
        }
        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (pnlHeader != null) pnlHeader.Visible = false;
        }

        private void frmLophocphan_GV_Load(object sender, EventArgs e)
        {
            pnlDetail.Width = 0;
            pnlDetail.Visible = false;
            try
            {
                Connection.KetNoi();
                LoadInitialComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLophocphan_GV_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadInitialComboBoxes()
        {
            
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                DataRow rAll = dtKhoa.NewRow();

                rAll["MaKhoa"] = "ALL";
                rAll["TenKhoa"] = "--- Tất cả ---";

                dtKhoa.Rows.InsertAt(rAll, 0);

                UpdateFilterMonComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateFilterMonComboBox()
        {
            try
            {
                // Tạm ngắt event để tránh LoadData() bị gọi khi đang set DataSource
                cboFilterMon.SelectedIndexChanged -= cboFilterMon_SelectedIndexChanged;

                // TenDangNhap = MaGV theo quy ước liên kết DB
                string maGV = SessionHelper.TenDangNhap;

                DataTable dtMon = bll.GetMonHocTheoGiangVien(maGV);

                DataRow rAll = dtMon.NewRow();
                rAll["MaMon"] = "ALL";
                rAll["TenMon"] = "--- Tất cả ---";
                dtMon.Rows.InsertAt(rAll, 0);

                cboFilterMon.DataSource    = dtMon;
                cboFilterMon.ValueMember   = "MaMon";
                cboFilterMon.DisplayMember = "TenMon";
                cboFilterMon.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Gắn lại event sau khi load xong
                cboFilterMon.SelectedIndexChanged += cboFilterMon_SelectedIndexChanged;
            }
        }

        private void LoadData()
        {
            try
            {
                // TenDangNhap = MaGV theo quy ước liên kết DB
                string maGV = SessionHelper.TenDangNhap;

                tblLophocphan = bll.GetDanhSachTheoGiangVien(
                        maGV,
                        txtTimKiem.Text.Trim(),
                        cboFilterMon.SelectedValue?.ToString()
                        );

                dataGridView.DataSource = tblLophocphan;

                if (dataGridView.Columns.Contains("MaLHP"))         dataGridView.Columns["MaLHP"].HeaderText         = "Mã LHP";
                if (dataGridView.Columns.Contains("TenLopHocPhan")) dataGridView.Columns["TenLopHocPhan"].HeaderText = "Tên lớp";
                if (dataGridView.Columns.Contains("ThoiGianHoc"))   dataGridView.Columns["ThoiGianHoc"].HeaderText   = "Thời gian";
                if (dataGridView.Columns.Contains("PhongHoc"))      dataGridView.Columns["PhongHoc"].HeaderText      = "Phòng";
                if (dataGridView.Columns.Contains("TenKhoa"))       dataGridView.Columns["TenKhoa"].HeaderText       = "Khoa";
                if (dataGridView.Columns.Contains("TenMon"))        dataGridView.Columns["TenMon"].HeaderText        = "Môn học";
                if (dataGridView.Columns.Contains("TenGiangVien"))  dataGridView.Columns["TenGiangVien"].HeaderText  = "Giảng viên";
                if (dataGridView.Columns.Contains("TrangThai"))     dataGridView.Columns["TrangThai"].HeaderText     = "Trạng thái";

                if (dataGridView.Columns.Contains("MaKhoa")) dataGridView.Columns["MaKhoa"].Visible = false;
                if (dataGridView.Columns.Contains("MaMon"))  dataGridView.Columns["MaMon"].Visible  = false;
                if (dataGridView.Columns.Contains("MaGV"))   dataGridView.Columns["MaGV"].Visible   = false;

                dataGridView.AllowUserToAddRows = false;
                dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblTongBanGhi.Text = $"Tổng: {tblLophocphan?.Rows.Count ?? 0} lớp học phần";
                
//);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";

            //cboFilterKhoa.SelectedIndex = 0;

            UpdateFilterMonComboBox();

            LoadData();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadData();
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

        
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //if (isEditMode) return; // Không cho chuyển row khi đang nhập liệu

            DataGridViewRow row = dataGridView.Rows[e.RowIndex];

            string maLHP = row.Cells["MaLHP"].Value?.ToString();
            string tenLHP = row.Cells["TenLopHocPhan"].Value?.ToString();
            string thoiGian = row.Cells["ThoiGianHoc"].Value?.ToString();
            string phong = row.Cells["PhongHoc"].Value?.ToString();
            string tenKhoa = row.Cells["TenKhoa"].Value?.ToString();
            string tenMon = row.Cells["TenMon"].Value?.ToString();
            string tenGV = row.Cells["TenGiangVien"].Value?.ToString();
            string maKhoa = row.Cells["MaKhoa"].Value?.ToString();
            string maMon = row.Cells["MaMon"].Value?.ToString();
            string maGV = row.Cells["MaGV"].Value?.ToString();
            string trangThai = row.Cells["TrangThai"].Value?.ToString();

            ShowViewMode(maLHP, tenLHP, thoiGian, phong, tenKhoa, tenMon, tenGV, maKhoa, maMon, maGV, trangThai);
        }

        private void ShowViewMode(string maLHP, string tenLHP, string thoiGian, string phong, string tenKhoa, string tenMon, string tenGV, string maKhoa, string maMon, string maGV, string trangThai)
        {
            
            //currentMaLHP = maLHP;

            lblDetailHeader.Text = "Chi tiết Lớp Học Phần";
            lblDetailHeader.ForeColor = Color.FromArgb(21, 101, 192);

            // Toggle Visibility
            lblDetailMaLHP.Visible = true;
            lblDetailTenLHP.Visible = true;
            lblDetailThoiGianHoc.Visible = true;
            lblDetailPhongHoc.Visible = true;
            lblDetailKhoa.Visible = true;
            lblDetailMon.Visible = true;
            lblDetailGiangVien.Visible = true;
            lblDetailTrangThai.Visible = true;

            //txtEditMaLHP.Visible = false;
            //txtEditTenLHP.Visible = false;
            
            btnCloseDetail.Visible = true;

            // Load Values
            lblDetailMaLHP.Text = maLHP ?? "";
            lblDetailTenLHP.Text = tenLHP ?? "";
            lblDetailThoiGianHoc.Text = thoiGian ?? "";
            lblDetailPhongHoc.Text = phong ?? "";
            lblDetailKhoa.Text = tenKhoa ?? "";
            lblDetailMon.Text = tenMon ?? "";
            lblDetailGiangVien.Text = tenGV ?? "";
            lblDetailTrangThai.Text = trangThai == "DangMo" ? "Đang mở" : "Đã đóng";

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
    }
}
