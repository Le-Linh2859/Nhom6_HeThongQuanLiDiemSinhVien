using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDSV.BLL;

namespace QLDSV.GUI.Forms.GiangVien
{
    public partial class frmThongTinCaNhan_GV : Form
    {
        private readonly GiangVienBLL bll = new GiangVienBLL();
        private string maTaiKhoan;

        public frmThongTinCaNhan_GV()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frmThongTinCaNhan_GV_Load(object sender, EventArgs e)
        {
            LoadThongTinGiangVien();
            txtMatk.Enabled = false;
            txtMagv.Enabled = false;
            txtGioitinh.Enabled = false;
            txtTenkhoa.Enabled = false;
            txtMatk.Enabled = false;

        }
        private void LoadThongTinGiangVien()
        {
            try
            {
                DataTable dt = bll.GetGiangVien();
                maTaiKhoan = SessionHelper.MaTaiKhoan;

                DataRow row = dt.AsEnumerable()
                    .FirstOrDefault(r => r["MaTaiKhoan"].ToString() == maTaiKhoan);
                if (row == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy thông tin giảng viên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Close();
                    return;
                }

                txtMagv.Text = row["MaGV"].ToString();
                txtTengv.Text = row["HoTen"].ToString();
                bool gioiTinh = Convert.ToBoolean(row["GioiTinh"]);

                txtGioitinh.Text = gioiTinh ? "Nam" : "Nữ";
                txtTenkhoa.Text = row["TenKhoa"].ToString();
                txtMatk.Text = row["MaTaiKhoan"].ToString();
                txtSdt.Text = row["SoDT"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtDiachi.Text = row["DiaChi"].ToString();

                txtMagv.ReadOnly = true;
                txtTenkhoa.ReadOnly = true;
                txtMatk.ReadOnly = true;
                txtGioitinh.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTengv.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!");
                    txtTengv.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSdt.Text))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!");
                    txtSdt.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập email!");
                    txtEmail.Focus();
                    return;
                }

                bll.UpdateGiangVien(
                    txtSdt.Text.Trim(),
                    txtTengv.Text.Trim(),
                    true, 
                    txtDiachi.Text.Trim(),
                    txtMagv.Text.Trim(),
                    txtEmail.Text.Trim(),
                    LayMaKhoa(txtTenkhoa.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật thông tin thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadThongTinGiangVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private string LayMaKhoa(string tenKhoa)
        {
            DataTable dtKhoa = bll.GetKhoa();

            foreach (DataRow row in dtKhoa.Rows)
            {
                if (row["TenKhoa"].ToString() == tenKhoa)
                    return row["MaKhoa"].ToString();
            }

            return "";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadThongTinGiangVien();
        }
    }
}
