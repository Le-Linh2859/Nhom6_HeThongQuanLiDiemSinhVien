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
            txtTenkhoa.Enabled = false;

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
                txtTenkhoa.Text = row["TenKhoa"].ToString();
                txtMatk.Text = row["MaTaiKhoan"].ToString();
                txtSdt.Text = row["SoDT"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtDiachi.Text = row["DiaChi"].ToString();

                // Giới tính dùng RadioButton
                bool gioiTinh = Convert.ToBoolean(row["GioiTinh"]);
                rdoNam.Checked = gioiTinh;
                rdoNu.Checked = !gioiTinh;
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
                bool gioiTinhMoi = rdoNam.Checked;

                if (!bll.IsThongTinChanged(
                    txtMagv.Text.Trim(),
                    txtTengv.Text.Trim(),
                    txtSdt.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtDiachi.Text.Trim(),
                    gioiTinhMoi))
                {
                    MessageBox.Show(
                        "Bạn chưa thay đổi thông tin nào!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }
                string loi = bll.ValidateCapNhatGiangVien(
                     txtMagv.Text.Trim(),
                     txtTengv.Text.Trim(),
                     txtEmail.Text.Trim(),
                     txtSdt.Text.Trim());

                if (!string.IsNullOrEmpty(loi))
                {
                    MessageBox.Show(
                        loi,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

          
                bll.UpdateGiangVien
               (
                    txtSdt.Text.Trim(),
                    txtTengv.Text.Trim(),
                    gioiTinhMoi,
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
