using System;
using System.Data;
using System.Windows.Forms;
using QLDSV.BLL;
using QLDSV.GUI;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class frmThongTinCaNhan_SV : Form
    {
        private readonly SinhVienBLL _bll = new SinhVienBLL();
        private string _maTaiKhoan;
        private string _maSV;

        public frmThongTinCaNhan_SV()
        {
            InitializeComponent();
            this.Load += frmThongTinCaNhan_SV_Load;
        }

        private void frmThongTinCaNhan_SV_Load(object sender, EventArgs e)
        {
            LoadThongTinSinhVien();
            // Thông tin định danh - chỉ xem, không chỉnh sửa
            guna2TextBox1.ReadOnly = true;
            guna2TextBox2.ReadOnly = true;
            guna2TextBox4.ReadOnly = true;
            guna2TextBox5.ReadOnly = true;
            guna2TextBox6.ReadOnly = true;
            guna2TextBox7.ReadOnly = true;
            guna2TextBox8.ReadOnly = true;
        }

        private void LoadThongTinSinhVien()
        {
            try
            {
                _maTaiKhoan = SessionHelper.MaTaiKhoan;
                DataTable dt = _bll.GetThongTinSinhVien(_maTaiKhoan);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy thông tin sinh viên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                DataRow row = dt.Rows[0];
                _maSV = row["MaSV"].ToString();

                guna2TextBox1.Text = row["MaSV"].ToString();
                guna2TextBox2.Text = row["HoTen"].ToString();
                guna2TextBox4.Text = row["NgaySinh"] != DBNull.Value 
                    ? Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy") 
                    : "";
                guna2TextBox5.Text = row["GioiTinh"] != DBNull.Value 
                    ? (Convert.ToBoolean(row["GioiTinh"]) ? "Nam" : "Nữ") 
                    : "";
                guna2TextBox6.Text = row["TenLop"].ToString();
                guna2TextBox7.Text = row["TenKhoa"].ToString();
                guna2TextBox8.Text = row["TenDangNhap"].ToString();

                guna2TextBox10.Text = row["SoDT"].ToString();
                guna2TextBox3.Text = row["Email"].ToString();
                guna2TextBox11.Text = row["DiaChi"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải thông tin: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate thông tin
                string loi = _bll.ValidateThongTin(guna2TextBox10.Text.Trim(), guna2TextBox3.Text.Trim());
                if (!string.IsNullOrEmpty(loi))
                {
                    MessageBox.Show(loi, "Lỗi validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra trùng email
                if (_bll.IsDuplicateEmail(guna2TextBox3.Text.Trim(), _maSV))
                {
                    MessageBox.Show("Email đã được sử dụng bởi sinh viên khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    guna2TextBox3.Focus();
                    return;
                }

                // Kiểm tra trùng số điện thoại
                if (_bll.IsDuplicateSoDT(guna2TextBox10.Text.Trim(), _maSV))
                {
                    MessageBox.Show("Số điện thoại đã được sử dụng bởi sinh viên khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    guna2TextBox10.Focus();
                    return;
                }

                _bll.UpdateThongTinSinhVien(
                    guna2TextBox10.Text.Trim(),
                    guna2TextBox3.Text.Trim(),
                    guna2TextBox11.Text.Trim(),
                    _maSV
                );

                MessageBox.Show(
                    "Cập nhật thông tin thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadThongTinSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadThongTinSinhVien();
        }

        private void kryptonTableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void kryptonTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
        }
    }
}
