using QLDSV.DAL.Models;

namespace QLDSV.GUI
{
    /// <summary>
    /// Lưu thông tin phiên đăng nhập hiện tại (static session).
    /// </summary>
    public static class SessionHelper
    {
        public static string MaTaiKhoan { get; set; }
        public static string TenDangNhap { get; set; }
        public static string MaVaiTro { get; set; }

        /// <summary>
        /// Gán thông tin user sau khi đăng nhập thành công.
        /// </summary>
        public static void SetCurrentUser(TaiKhoan tk)
        {
            MaTaiKhoan = tk.MaTaiKhoan?.Trim();
            TenDangNhap = tk.TenDangNhap?.Trim();
            MaVaiTro = tk.MaVaiTro?.Trim();
        }

        /// <summary>
        /// Xóa thông tin session (dùng khi đăng xuất).
        /// </summary>
        public static void Clear()
        {
            MaTaiKhoan = null;
            TenDangNhap = null;
            MaVaiTro = null;
        }
    }
}
