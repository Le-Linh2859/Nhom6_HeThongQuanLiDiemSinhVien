
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
<<<<<<< HEAD

            // Migration tự động: Hash toàn bộ mật khẩu Plaintext còn tồn tại trong DB
            // Chạy một lần duy nhất, idempotent (tự phát hiện tài khoản đã hash, bỏ qua)
            PasswordMigration.MigrateToHashedPasswords();

=======
>>>>>>> 852c7397ed4fa053430c64fc1d247b95687fe997
            Application.Run(new frmDangNhap());
        }
    }
}

