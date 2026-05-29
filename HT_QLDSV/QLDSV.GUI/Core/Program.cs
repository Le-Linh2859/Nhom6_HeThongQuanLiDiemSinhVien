
using QLDSV.GUI.Forms.GiangVien;
using QLDSV.GUI.Forms.SinhVien;
using QLDSV.DAL;
using System;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi động chính của ứng dụng - Mở form Đăng nhập.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Migration tự động: Hash toàn bộ mật khẩu Plaintext còn tồn tại trong DB
            // Chạy một lần duy nhất, idempotent (tự phát hiện tài khoản đã hash, bỏ qua)
            PasswordMigration.MigrateToHashedPasswords();

            Application.Run(new frmDangNhap());
        }
    }
}

