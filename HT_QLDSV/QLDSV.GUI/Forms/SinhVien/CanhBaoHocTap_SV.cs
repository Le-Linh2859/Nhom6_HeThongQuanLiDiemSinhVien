using QLDSV.BLL;
using QLDSV.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class frmCanhBaoHocVu_SV : Form, IShellChildForm
    {
        private readonly CanhBaoHocVuBLL bll = new CanhBaoHocVuBLL();

        private DataTable dtCanhBao;
        public frmCanhBaoHocVu_SV()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }
        public void OnEmbeddedInShell()
        {

        }
        private string maSV;
        private string LayMaSinhVienDangNhap()
        {
            return bll.GetMaSVByTaiKhoan(
                SessionHelper.MaTaiKhoan);
        }
        private void CanhBaoHocTap_SV_Load(object sender, EventArgs e)
        {

            try
            {
                LoadComboboxFilters();
                WireEvents();
                LoadData();
                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LOAD DỮ LIỆU CẢNH BÁO CỦA CHÍNH SINH VIÊN
        // =====================================================

        private void LoadData()
        {
            try
            {
                if (string.IsNullOrEmpty(maSV))
                {
                    maSV = LayMaSinhVienDangNhap();
                }


                dtCanhBao =
                    bll.GetCanhBaoBySinhVien(maSV);

                dgvCanhBao.DataSource =
                    dtCanhBao;

                if (dgvCanhBao.Columns.Count > 0)
                {
                    dgvCanhBao.Columns["MaCanhBao"]
                        .HeaderText = "Mã cảnh báo";

                    dgvCanhBao.Columns["TenLoaiHK"]
                        .HeaderText = "Học kỳ";

                    dgvCanhBao.Columns["TenNamHoc"]
                        .HeaderText = "Năm học";

                    dgvCanhBao.Columns["Noidung"]
                        .HeaderText = "Nội dung";

                    dgvCanhBao.Columns["LoaiCanhBao"]
                        .HeaderText = "Mức";

                    dgvCanhBao.Columns["ThoiDiem"]
                        .HeaderText = "Ngày cảnh báo";

                    dgvCanhBao.Columns["LanThu"]
                        .HeaderText = "Lần";

                    dgvCanhBao.Columns["MaCanhBao"]
                        .Visible = false;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetupGrid()
        {
            dgvCanhBao.AllowUserToAddRows =
                false;

            dgvCanhBao.EditMode =
                DataGridViewEditMode.EditProgrammatically;

            dgvCanhBao.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCanhBao.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvCanhBao.EnableHeadersVisualStyles =
                false;

            dgvCanhBao.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(100, 88, 255);

            dgvCanhBao.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
        }
        private void dgvCanhBao_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string noiDung =
                dgvCanhBao.Rows[e.RowIndex]
                .Cells["Noidung"]
                .Value?.ToString();

            MessageBox.Show(
                noiDung,
                "Chi tiết cảnh báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        private void LoadComboboxFilters()
        {
            try
            {
                // Năm học
                if (string.IsNullOrEmpty(maSV))
                {
                    maSV = LayMaSinhVienDangNhap();
                }

                // Năm học
                DataTable dtNamHoc =
                    bll.GetNamHocTheoSinhVien(maSV);

                DataRow rowNH = dtNamHoc.NewRow();

                rowNH["MaNamHoc"] = "Tất cả";
                rowNH["TenNamHoc"] = "Tất cả";

                dtNamHoc.Rows.InsertAt(rowNH, 0);

                cboNamHoc.DataSource = dtNamHoc;
                cboNamHoc.DisplayMember = "TenNamHoc";
                cboNamHoc.ValueMember = "MaNamHoc";

                // Học kỳ
                DataTable dtHocKy = bll.GetHocKy();

                DataRow rowHK = dtHocKy.NewRow();

                rowHK["MaLoaiHK"] = "Tất cả";
                rowHK["TenLoaiHK"] = "Tất cả";

                dtHocKy.Rows.InsertAt(rowHK, 0);

                cboHocKy.DataSource = dtHocKy;
                cboHocKy.DisplayMember = "TenLoaiHK";
                cboHocKy.ValueMember = "MaLoaiHK";

                // Loại cảnh báo
                DataTable dtLoai = new DataTable();

                dtLoai.Columns.Add("Loai");
                dtLoai.Columns.Add("TenLoai");

                dtLoai.Rows.Add("Tất cả", "Tất cả");
                dtLoai.Rows.Add("2", "Mức 2 - Khối lượng 0 tín chỉ");
                dtLoai.Rows.Add("3", "Mức 3 - TBC dưới 1.5");

                cboLoaiCanhBao.DataSource = dtLoai;
                cboLoaiCanhBao.DisplayMember = "TenLoai";
                cboLoaiCanhBao.ValueMember = "Loai";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void WireEvents()
        {
            cboNamHoc.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboHocKy.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboLoaiCanhBao.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            btnLammoi.Click += btnLammoi_Click;
        }
        private void cboFilter_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            ApplyFilter();
        }

        private void txtTimKiem_TextChanged(
            object sender,
            EventArgs e)
        {
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            if (dtCanhBao == null)
                return;

            string filter = "1=1";

            if (cboNamHoc.SelectedValue != null &&
                cboNamHoc.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND TenNamHoc = '{cboNamHoc.Text}'";
            }

            if (cboHocKy.SelectedValue != null &&
                cboHocKy.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND TenLoaiHK = '{cboHocKy.Text}'";
            }

            if (cboLoaiCanhBao.SelectedValue != null &&
                cboLoaiCanhBao.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND LoaiCanhBao = {cboLoaiCanhBao.SelectedValue}";
            }

            string kw = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(kw))
            {
                kw = kw.Replace("'", "''");

                filter +=
                    $" AND Noidung LIKE '%{kw}%'";
            }

            dtCanhBao.DefaultView.RowFilter = filter;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
                txtTimKiem.Text = "";

                cboNamHoc.SelectedIndex = 0;

                cboHocKy.SelectedIndex = 0;

                cboLoaiCanhBao.SelectedIndex = 0;

                LoadData();
            }
    }
}