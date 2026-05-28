using QLDSV.GUI.Forms.GiangVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bỏ qua đăng nhập, vào thẳng giao diện Admin để test nhanh
            SessionHelper.MaTaiKhoan = "TK006";
            SessionHelper.TenDangNhap = "Admin2025";
            SessionHelper.MaVaiTro = "VT001";

            Application.Run(new frmMain());
        }
    }
}
