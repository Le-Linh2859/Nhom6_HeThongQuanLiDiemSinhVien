using QLDSV.GUI.Forms.GiangVien;
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

            SessionHelper.MaTaiKhoan = "TK007"; // tài khoản có thật trong DB
            SessionHelper.TenDangNhap = "GV20260001";
            SessionHelper.MaVaiTro = "VT002";

            Application.Run(new frmThongTinCaNhan_GV());
        }
    }
}

