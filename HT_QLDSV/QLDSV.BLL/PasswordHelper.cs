using BCrypt.Net;

namespace QLDSV.BLL
{
    /// <summary>
    /// Lớp tiện ích tập trung xử lý mã hóa mật khẩu sử dụng BCrypt.
    /// BCrypt tự động sinh Salt ngẫu nhiên và đính kèm trong chuỗi Hash đầu ra ($2a$...).
    /// </summary>
    public static class PasswordHelper
    {
        // WorkFactor = 12: điểm cân bằng tối ưu giữa bảo mật cao và tốc độ chấp nhận được
        private const int WorkFactor = 12;

        /// <summary>
        /// Băm mật khẩu khi tạo tài khoản mới hoặc đổi mật khẩu.
        /// Trả về chuỗi hash BCrypt (gồm cả Salt đính kèm) để lưu vào DB.
        /// </summary>
        public static string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);
        }

        /// <summary>
        /// Xác minh mật khẩu nhập vào so với chuỗi hash đang lưu trong DB.
        /// Trả về true nếu khớp, false nếu không khớp.
        /// </summary>
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(hashedPassword))
                return false;

            // Nếu mật khẩu trong DB vẫn là Plaintext (chưa migrate), so sánh trực tiếp
            // Điều này đảm bảo tương thích ngược trong giai đoạn chuyển đổi
            if (!IsBCryptHash(hashedPassword))
                return plainPassword == hashedPassword;

            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là BCrypt hash hợp lệ không.
        /// BCrypt hash luôn bắt đầu bằng "$2" (ví dụ: $2a$12$...).
        /// </summary>
        public static bool IsBCryptHash(string value)
        {
            return !string.IsNullOrEmpty(value) && value.StartsWith("$2");
        }
    }
}
