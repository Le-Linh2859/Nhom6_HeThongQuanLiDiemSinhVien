using System;
using System.Data;
using System.Data.SqlClient;
using BCrypt.Net;

namespace QLDSV.DAL
{
    /// <summary>
    /// Migration tự động: Chuyển đổi toàn bộ mật khẩu Plaintext trong DB sang BCrypt Hash.
    /// Chạy một lần khi ứng dụng khởi động — idempotent (chạy nhiều lần không ảnh hưởng).
    /// </summary>
    public static class PasswordMigration
    {
        /// <summary>
        /// Quét toàn bộ bảng TaiKhoan, nếu phát hiện mật khẩu chưa được mã hóa BCrypt
        /// (không bắt đầu bằng "$2"), sẽ tự động hash và cập nhật vào DB.
        /// </summary>
        public static void MigrateToHashedPasswords()
        {
            try
            {
                string connString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["QLDSV"]?.ConnectionString;

                if (string.IsNullOrEmpty(connString)) return;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // 1. Lấy tất cả tài khoản có mật khẩu chưa được hash BCrypt
                    string selectSql = "SELECT MaTaiKhoan, MatKhau FROM TaiKhoan";
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(selectSql, conn))
                    {
                        da.Fill(dt);
                    }

                    int migratedCount = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        string maTaiKhoan = row["MaTaiKhoan"].ToString();
                        string matKhau = row["MatKhau"].ToString();

                        // Bỏ qua nếu đã là BCrypt hash (bắt đầu bằng "$2")
                        if (!string.IsNullOrEmpty(matKhau) && matKhau.StartsWith("$2"))
                            continue;

                        // Hash mật khẩu Plaintext bằng BCrypt (work factor 12)
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau, 12);

                        // Cập nhật vào DB
                        string updateSql = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE MaTaiKhoan = @MaTaiKhoan";
                        using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                            cmd.ExecuteNonQuery();
                        }

                        migratedCount++;
                    }

                    // Ghi log nếu có tài khoản được migrate
                    if (migratedCount > 0)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"[PasswordMigration] Đã mã hóa BCrypt thành công {migratedCount} tài khoản.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Không throw để không chặn việc khởi động ứng dụng
                System.Diagnostics.Debug.WriteLine($"[PasswordMigration] Lỗi: {ex.Message}");
            }
        }
    }
}
