using QLDSV.BLL;
using QLDSV.GUI.Forms.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;


namespace QLDSV.GUI
{
    public partial class frmGiangvien : Form
    {
        private GiangVienBLL bll = new GiangVienBLL();
        private bool isAdding = false;
        private bool isEditing = false;
        private DataTable dtGiangVien;
        public frmGiangvien()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.frmGiangvien_Load);

            ThemeHelper.ApplyTheme(this);

            //this.DataGridViewGV.CellClick += DataGridViewGV_CellClick;
            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên ...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm kiếm theo mã, tên giảng viên ...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };

        }
        private void frmGiangvien_Resize(object sender, EventArgs e)
        {
            guna2Panel1.Width = this.ClientSize.Width - 210;
            groupBox1.Width = this.ClientSize.Width - 210;
        }
        private void frmGiangvien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboboxFilters();
                LoadComboboxDetails();
                LoadData();
                ResetFormState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DataGridViewGV.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            DataGridViewGV.ThemeStyle.HeaderStyle.ForeColor = Color.White;

            pnlThongKeGV.Paint += (s, e2) =>
            {
                var g = e2.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(100, 88, 255), 2))
                {
                    var rect = new System.Drawing.Rectangle(1, 1, pnlThongKeGV.Width - 3, pnlThongKeGV.Height - 3);
                    g.DrawRectangle(pen, rect);
                }
            };

        }
        private void LoadData()
        {
            try
            {
                dtGiangVien = bll.GetGiangVien();
                DataGridViewGV.DataSource = dtGiangVien;

                if (DataGridViewGV.Columns.Count > 0)
                {
                    DataGridViewGV.Columns["MaGV"].HeaderText = "Mã giảng viên";
                    DataGridViewGV.Columns["HoTen"].HeaderText = "Họ tên";
                    DataGridViewGV.Columns["GioiTinh"].HeaderText = "Giới tính";
                    DataGridViewGV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                    DataGridViewGV.Columns["SoDT"].HeaderText = "Số điện thoại";
                    DataGridViewGV.Columns["Email"].HeaderText = "Email";
                    DataGridViewGV.Columns["MaKhoa"].Visible = false;
                    DataGridViewGV.Columns["MaTaiKhoan"].Visible = false;
                }

                DataGridViewGV.AllowUserToAddRows = false;
                DataGridViewGV.EditMode = DataGridViewEditMode.EditProgrammatically;
                DataGridViewGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                CapNhatSoLuong(dtGiangVien.Rows.Count);
                if (DataGridViewGV.Rows.Count > 0)
                {
                    DataGridViewGV.ClearSelection();
                    DataGridViewGV.Rows[0].Selected = true;

                    DataGridViewGV_CellClick(null, null);

                }
                CapNhatSoLuong(dtGiangVien.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        // Lọc theo search + khoa

        private void CapNhatSoLuong(int soLuong)
        {
            lblTitle.Text = "Tổng số";
            lblSumGV.Text = soLuong.ToString();
        }
        // 2. Nạp dữ liệu các bộ lọc Khoa
        private void LoadComboboxFilters()
        {
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                DataTable dtFilter = dtKhoa.Copy();

                DataRow row = dtFilter.NewRow();

                row["MaKhoa"] = "ALL";
                row["TenKhoa"] = "Tất cả";

                dtFilter.Rows.InsertAt(row, 0);

                cboKhoa.DataSource = dtFilter;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";

                cboKhoa.SelectedIndex = 0;

                cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 3. Nạp dữ liệu các Combobox nhập liệu chi tiết
        private void LoadComboboxDetails()
        {
            try
            {
                DataTable dtKhoa = bll.GetKhoa();

                cboKhoa2.DataSource = dtKhoa;
                cboKhoa2.DisplayMember = "TenKhoa";
                cboKhoa2.ValueMember = "MaKhoa";

                cboKhoa2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 4. Khôi phục trạng thái xem ban đầu của Form
        private void ResetFormState()
        {
            isAdding = false;
            isEditing = false;

            // Khóa nhập liệu
            cboMaGV.Enabled = false;
            cboTenGV.Enabled = false;
            cboDiaChi.Enabled = false;
            cboEmail.Enabled = false;

            mskSoDienThoai.Enabled = false;

            rdoNam.Enabled = false;
            rdoNu.Enabled = false;

            cboKhoa2.Enabled = false;

            cboMatKhau.Enabled = false;
            cboMatKhau1.Enabled = false;

            // Ẩn mật khẩu
            cboMatKhau.Visible = false;
            cboMatKhau1.Visible = false;

            lblMatKhau.Visible = false;
            lblNhapLaiMK.Visible = false;

            // Nút
            btnThem.Enabled = true;
            btnSua.Enabled = true;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnReset.Enabled = false;

            // Load lại dòng đang chọn
            if (DataGridViewGV.SelectedRows.Count > 0)
            {
                DataGridViewGV_CellClick(null, null);
            }
        }

        private void DataGridViewGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridViewGV.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = DataGridViewGV.SelectedRows[0];

            cboMaGV.Text = row.Cells["MaGV"].Value?.ToString();
            cboTenGV.Text = row.Cells["HoTen"].Value?.ToString();

            cboDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();

            cboEmail.Text = row.Cells["Email"].Value?.ToString();

            mskSoDienThoai.Text = row.Cells["SoDT"].Value?.ToString();

            cboKhoa2.SelectedValue = row.Cells["MaKhoa"].Value?.ToString();

            bool gioiTinh =
            Convert.ToBoolean(row.Cells["GioiTinh"].Value);

            if (gioiTinh)
                rdoNam.Checked = true;
            else
                rdoNu.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;
            cboMaGV.Text = bll.TaoMaGiangVienMoi();
            cboMaGV.ReadOnly = true;
            // Xóa trắng dữ liệu
            
            cboTenGV.Text = "";
            cboDiaChi.Text = "";
            cboEmail.Text = "";
            mskSoDienThoai.Text = "";

            rdoNam.Checked = true;

            cboKhoa2.SelectedIndex = -1;

            // Hiện ô mật khẩu khi thêm
            cboMatKhau.Visible = true;
            cboMatKhau1.Visible = true;

            lblMatKhau.Visible = true;
            lblNhapLaiMK.Visible = true;

            cboMatKhau.Text = "";
            cboMatKhau1.Text = "";

            // Mở khóa nhập liệu
            cboMaGV.Enabled = true;
            cboTenGV.Enabled = true;
            cboDiaChi.Enabled = true;
            cboEmail.Enabled = true;
            mskSoDienThoai.Enabled = true;

            rdoNam.Enabled = true;
            rdoNu.Enabled = true;

            cboKhoa2.Enabled = true;

            cboMatKhau.Enabled = true;
            cboMatKhau1.Enabled = true;

            // Trạng thái nút
            btnThem.Enabled = false;
            btnSua.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboMaGV.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearDetailInputs();
            ResetFormState();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            {
                // Nếu đang THÊM
                if (isAdding)
                {
                    cboMaGV.Text = "";
                    cboTenGV.Text = "";
                    cboDiaChi.Text = "";
                    cboEmail.Text = "";
                    mskSoDienThoai.Text = "";
                    rdoNam.Checked = true;
                    cboKhoa2.SelectedIndex = -1;
                    cboMatKhau.Text = "";
                    cboMatKhau1.Text = "";
                    cboMaGV.Focus();
                }
                // Nếu đang SỬA
                else if (isEditing)
                {
                    if (DataGridViewGV.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = DataGridViewGV.SelectedRows[0];
                        cboMaGV.Text =
                        row.Cells["MaGV"].Value?.ToString() ?? "";
                        cboTenGV.Text =
                        row.Cells["HoTen"].Value?.ToString() ?? "";
                        cboDiaChi.Text =
                        row.Cells["DiaChi"].Value?.ToString() ?? "";
                        cboEmail.Text =
                        row.Cells["Email"].Value?.ToString() ?? "";
                        mskSoDienThoai.Text =
                        row.Cells["SoDT"].Value?.ToString() ?? "";
                        bool gioiTinh =Convert.ToBoolean(row.Cells["GioiTinh"].Value);
                        if (gioiTinh)
                            rdoNam.Checked = true;
                        else
                            rdoNu.Checked = true;
                        string maKhoa =
                        row.Cells["MaKhoa"].Value?.ToString().Trim() ?? "";
                        if (!string.IsNullOrEmpty(maKhoa))
                            cboKhoa2.SelectedValue = maKhoa;
                        else
                            cboKhoa2.SelectedIndex = -1;
                    }
                }
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            // Tạm ngắt sự kiện filter
            cboKhoa.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;

            // Reset tìm kiếm
            txtTimKiem.Text = "Tìm kiếm theo mã, tên giảng viên ...";
            txtTimKiem.ForeColor = Color.Gray;

            // Reset combobox khoa
            cboKhoa.SelectedIndex = 0;

            // Gắn lại sự kiện
            cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // Load lại dữ liệu
            LoadData();

            // Reset form
            ClearDetailInputs();
            ResetFormState();
        }
        private void ApplyFilter()
        {
            if (dtGiangVien == null)
                return;

            string filter = "1=1";
            // Lọc theo khoa
            if (cboKhoa.SelectedValue != null &&
            cboKhoa.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaKhoa = '{cboKhoa.SelectedValue}'";
            }
            // Tìm kiếm mã + tên GV
            string kw = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(kw) &&
            kw != "Tìm kiếm theo mã, tên giảng viên ...")
            {
                string escapedKw = kw.Replace("'", "''");

                filter +=
                $" AND (MaGV LIKE '%{escapedKw}%' " +
                $"OR HoTen LIKE '%{escapedKw}%')";
            }
            // Áp dụng filter
            dtGiangVien.DefaultView.RowFilter = filter;
            // Cập nhật card theo số dòng sau filter
            CapNhatSoLuong(dtGiangVien.DefaultView.Count);

            // Nếu không có dữ liệu
            if (DataGridViewGV.Rows.Count == 0)
            {
                ClearDetailInputs();
            }
            else
            {
                DataGridViewGV.ClearSelection();
                DataGridViewGV.CurrentCell =
                DataGridViewGV.Rows[0].Cells["MaGV"];
                DataGridViewGV.Rows[0].Selected = true;
                DataGridViewGV_CellClick(null, null);
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maGV = cboMaGV.Text.Trim();
            string hoTen = cboTenGV.Text.Trim();
            string diaChi = cboDiaChi.Text.Trim();
            string email = cboEmail.Text.Trim();
            string soDT = new string(
                mskSoDienThoai.Text
                .Where(char.IsDigit)
                .ToArray());
            bool gioiTinh = rdoNam.Checked;

            if (cboKhoa2.SelectedValue == null)
            {
                MessageBox.Show(
                "Vui lòng chọn khoa.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

                return;
            }

            string maKhoa = cboKhoa2.SelectedValue.ToString();
            try
            {
                // THÊM
                if (isAdding)
                {
                    string matKhau = cboMatKhau.Text.Trim();
                    string nhapLaiMK = cboMatKhau1.Text.Trim();
                    string loi = bll.ValidateThemGiangVien(
                    maGV,
                    hoTen,
                    email,
                    soDT,
                    matKhau,
                    nhapLaiMK);
                    if (!string.IsNullOrEmpty(loi))
                    {
                        MessageBox.Show(
                        loi,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                        return;
                    }

                    string maTK = bll.TaoMaTaiKhoanMoi();

                    // Hash mật khẩu trước khi lưu vào Database
                    string hashedMatKhau = PasswordHelper.HashPassword(matKhau);

                    bll.InsertTaiKhoan(
                    maTK,
                    "VT002",
                    maGV,
                    hashedMatKhau,
                    1);

                    bll.InsertGiangVien(
                    soDT,
                    hoTen,
                    gioiTinh,
                    diaChi,
                    maGV,
                    email,
                    maKhoa,
                    maTK);

                    MessageBox.Show(
                    "Thêm giảng viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }

                // SỬA
                else if (isEditing)
                {
                    string loi = bll.ValidateCapNhatGiangVien(
                    maGV,
                    hoTen,
                    email,
                    soDT);

                    if (!string.IsNullOrEmpty(loi))
                    {
                        MessageBox.Show(
                        loi,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                        return;
                    }

                    bll.UpdateGiangVien(
                    soDT,
                    hoTen,
                    gioiTinh,
                    diaChi,
                    maGV,
                    email,
                    maKhoa);

                    MessageBox.Show(
                    "Cập nhật giảng viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }

                // Reload dữ liệu
                LoadData();

                ResetFormState();

                cboMatKhau.Visible = false;
                cboMatKhau1.Visible = false;

                lblMatKhau.Visible = false;
                lblNhapLaiMK.Visible = false;

                foreach (DataGridViewRow row in DataGridViewGV.Rows)
                {
                    if (row.Cells["MaGV"].Value?.ToString().Trim() == maGV)
                    {
                        row.Selected = true;
                        DataGridViewGV.CurrentCell = row.Cells["MaGV"];
                        DataGridViewGV_CellClick(null, null);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Lỗi lưu dữ liệu: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DataGridViewGV.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                "Vui lòng chọn giảng viên cần chỉnh sửa.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

                return;
            }

            isAdding = false;
            isEditing = true;

            // Không cho sửa mã giảng viên
            cboMaGV.Enabled = false;

            // Cho phép sửa thông tin
            cboTenGV.Enabled = true;
            cboDiaChi.Enabled = true;
            cboEmail.Enabled = true;
            mskSoDienThoai.Enabled = true;

            rdoNam.Enabled = true;
            rdoNu.Enabled = true;

            cboKhoa2.Enabled = true;

            // Ẩn phần mật khẩu khi sửa
            cboMatKhau.Visible = false;
            cboMatKhau1.Visible = false;

            lblMatKhau.Visible = false;
            lblNhapLaiMK.Visible = false;

            cboMatKhau.Enabled = false;
            cboMatKhau1.Enabled = false;

            // Trạng thái nút
            btnThem.Enabled = false;
            btnSua.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboTenGV.Focus();
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void ClearDetailInputs()
        {
            cboMaGV.Text = "";
            cboTenGV.Text = "";
            cboDiaChi.Text = "";
            cboEmail.Text = "";

            mskSoDienThoai.Text = "";

            rdoNam.Checked = true;

            cboKhoa2.SelectedIndex = -1;

            cboMatKhau.Text = "";
            cboMatKhau1.Text = "";
        }

        private void cboKhoa2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}