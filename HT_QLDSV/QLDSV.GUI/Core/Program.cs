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
            Application.Run(new frmDangNhap());
        }
    }
}

