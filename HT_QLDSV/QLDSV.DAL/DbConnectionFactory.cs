using System.Data;
using System.Data.SqlClient;

namespace QLDSV.DAL
{
    /// <summary>
    /// Factory tạo SqlConnection từ connection string.
    /// Thay thế cho Connection.cs cũ – không hardcode connection string.
    /// </summary>
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tạo và trả về một IDbConnection mới (chưa mở).
        /// Caller chịu trách nhiệm dispose (dùng using).
        /// </summary>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
