namespace QLDSV.DAL.Models
{
    /// <summary>
    /// DTO đại diện cho bảng TaiKhoan trong database.
    /// </summary>
    public class TaiKhoan
    {
        public string MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string MaVaiTro { get; set; }
    }
}
