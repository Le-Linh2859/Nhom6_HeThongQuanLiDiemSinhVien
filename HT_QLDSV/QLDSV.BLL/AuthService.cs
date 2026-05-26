using QLDSV.DAL.Models;
using QLDSV.DAL.Repositories;

namespace QLDSV.BLL
{
    /// <summary>
    /// Service xử lý logic xác thực đăng nhập.
    /// Tách riêng business logic khỏi tầng GUI.
    /// </summary>
    public class AuthService
    {
        private readonly ITaiKhoanRepository _repo;

        public AuthService(ITaiKhoanRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Thực hiện đăng nhập với tên đăng nhập và mật khẩu.
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhau">Mật khẩu (plaintext)</param>
        /// <returns>LoginResult chứa kết quả đăng nhập</returns>
        public LoginResult DangNhap(string tenDangNhap, string matKhau)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Vui lòng nhập tài khoản và mật khẩu."
                };
            }

            // Tìm tài khoản theo tên đăng nhập (parameterized query, an toàn SQL Injection)
            var taiKhoan = _repo.GetByTenDangNhap(tenDangNhap);

            if (taiKhoan == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Tài khoản hoặc mật khẩu không đúng."
                };
            }

            // So khớp mật khẩu (plaintext)
            if (taiKhoan.MatKhau == null || taiKhoan.MatKhau.Trim() != matKhau.Trim())
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Tài khoản hoặc mật khẩu không đúng."
                };
            }

            // Đăng nhập thành công
            return new LoginResult
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                TaiKhoan = taiKhoan
            };
        }
    }
}
