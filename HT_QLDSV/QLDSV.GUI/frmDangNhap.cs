using System;
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

            // Validation đã được chuyển vào AuthService,
            // nhưng vẫn kiểm tra sớm ở GUI để UX tốt hơn
            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi AuthService thông qua ServiceLocator (Repository Pattern + Dapper)
                var authService = ServiceLocator.GetAuthService();
                var result = authService.DangNhap(taikhoan, matkhau);

                if (result.Success)
                {
                    // Lưu thông tin session
                    SessionHelper.SetCurrentUser(result.TaiKhoan);

                    // Mở form chính
                    frmMain main = new frmMain();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(result.Message, "Đăng nhập thất bại",
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