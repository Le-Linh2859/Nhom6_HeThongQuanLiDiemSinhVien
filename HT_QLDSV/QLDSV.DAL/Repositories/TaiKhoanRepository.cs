using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QLDSV.DAL.Models;

namespace QLDSV.DAL.Repositories
{
    /// <summary>
    /// Triển khai Repository Pattern cho bảng TaiKhoan sử dụng SqlCommand thuần.
    /// Tất cả truy vấn đều dùng parameterized queries để chống SQL Injection.
    /// </summary>
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly DbConnectionFactory _factory;

        public TaiKhoanRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        /// <inheritdoc />
        public TaiKhoan GetByTenDangNhap(string tenDangNhap)
        {
            const string sql = @"SELECT MaTaiKhoan, TenDangNhap, MatKhau, MaVaiTro 
                                 FROM TaiKhoan 
                                 WHERE TenDangNhap = @TenDangNhap";

            using (var conn = (SqlConnection)_factory.CreateConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaiKhoan(reader);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public List<TaiKhoan> GetAll()
        {
            const string sql = @"SELECT MaTaiKhoan, TenDangNhap, MatKhau, MaVaiTro 
                                 FROM TaiKhoan";

            var list = new List<TaiKhoan>();
            using (var conn = (SqlConnection)_factory.CreateConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(MapTaiKhoan(reader));
                }
            }
            return list;
        }

        /// <inheritdoc />
        public void Insert(TaiKhoan taiKhoan)
        {
            const string sql = @"INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, MaVaiTro) 
                                 VALUES (@MaTaiKhoan, @TenDangNhap, @MatKhau, @MaVaiTro)";

            using (var conn = (SqlConnection)_factory.CreateConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", taiKhoan.MaTaiKhoan);
                    cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                    cmd.Parameters.AddWithValue("@MaVaiTro", taiKhoan.MaVaiTro);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public void Update(TaiKhoan taiKhoan)
        {
            const string sql = @"UPDATE TaiKhoan 
                                 SET TenDangNhap = @TenDangNhap, 
                                     MatKhau = @MatKhau, 
                                     MaVaiTro = @MaVaiTro 
                                 WHERE MaTaiKhoan = @MaTaiKhoan";

            using (var conn = (SqlConnection)_factory.CreateConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
                    cmd.Parameters.AddWithValue("@MaVaiTro", taiKhoan.MaVaiTro);
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", taiKhoan.MaTaiKhoan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <inheritdoc />
        public void Delete(string maTaiKhoan)
        {
            const string sql = "DELETE FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";

            using (var conn = (SqlConnection)_factory.CreateConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static TaiKhoan MapTaiKhoan(IDataReader reader)
        {
            return new TaiKhoan
            {
                MaTaiKhoan  = reader["MaTaiKhoan"].ToString(),
                TenDangNhap = reader["TenDangNhap"].ToString(),
                MatKhau     = reader["MatKhau"].ToString(),
                MaVaiTro    = reader["MaVaiTro"].ToString()
            };
        }
    }
}
