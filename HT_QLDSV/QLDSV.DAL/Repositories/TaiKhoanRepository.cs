using System.Collections.Generic;
using System.Linq;
using Dapper;
using QLDSV.DAL.Models;

namespace QLDSV.DAL.Repositories
{
    /// <summary>
    /// Triển khai Repository Pattern cho bảng TaiKhoan sử dụng Dapper.
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

            using (var conn = _factory.CreateConnection())
            {
                return conn.QueryFirstOrDefault<TaiKhoan>(sql, new { TenDangNhap = tenDangNhap });
            }
        }

        /// <inheritdoc />
        public List<TaiKhoan> GetAll()
        {
            const string sql = @"SELECT MaTaiKhoan, TenDangNhap, MatKhau, MaVaiTro 
                                 FROM TaiKhoan";

            using (var conn = _factory.CreateConnection())
            {
                return conn.Query<TaiKhoan>(sql).ToList();
            }
        }

        /// <inheritdoc />
        public void Insert(TaiKhoan taiKhoan)
        {
            const string sql = @"INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, MaVaiTro) 
                                 VALUES (@MaTaiKhoan, @TenDangNhap, @MatKhau, @MaVaiTro)";

            using (var conn = _factory.CreateConnection())
            {
                conn.Execute(sql, taiKhoan);
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

            using (var conn = _factory.CreateConnection())
            {
                conn.Execute(sql, taiKhoan);
            }
        }

        /// <inheritdoc />
        public void Delete(string maTaiKhoan)
        {
            const string sql = "DELETE FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";

            using (var conn = _factory.CreateConnection())
            {
                conn.Execute(sql, new { MaTaiKhoan = maTaiKhoan });
            }
        }
    }
}
