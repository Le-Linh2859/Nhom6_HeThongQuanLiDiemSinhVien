namespace QLDSV.DAL.Models
{
    /// <summary>
    /// Kết quả trả về sau khi thực hiện đăng nhập.
    /// </summary>
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}
