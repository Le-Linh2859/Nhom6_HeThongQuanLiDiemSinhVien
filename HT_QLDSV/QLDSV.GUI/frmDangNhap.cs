using System;
using System.Data;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string taikhoan = txtTaikhoan.Text.Trim();
            string matkhau = txtMatkhau.Text.Trim();

            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FunctionQa.ketnoi();
                string sql = $"SELECT MaTaiKhoan, TenDangNhap, MaVaiTro FROM TaiKhoan WHERE TenDangNhap = '{taikhoan}' AND MatKhau = '{matkhau}'";
                DataTable dt = FunctionQa.getdatatotable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    SessionHelper.MaTaiKhoan = dt.Rows[0]["MaTaiKhoan"]?.ToString().Trim() ?? "";
                    SessionHelper.TenDangNhap = dt.Rows[0]["TenDangNhap"]?.ToString().Trim() ?? "";
                    SessionHelper.MaVaiTro = dt.Rows[0]["MaVaiTro"]?.ToString().Trim() ?? "";

                    frmMain main = new frmMain();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatkhau.Text = "";
                    txtMatkhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtMatkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDangnhap_Click(sender, e);
        }
    }
}
        