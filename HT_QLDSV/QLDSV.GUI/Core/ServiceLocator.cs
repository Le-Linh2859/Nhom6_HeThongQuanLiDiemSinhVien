using System.Configuration;
using QLDSV.BLL;
using QLDSV.DAL;
using QLDSV.DAL.Repositories;

namespace QLDSV.GUI
{
    /// <summary>
    /// Service Locator đơn giản để khởi tạo dependencies cho WinForms.
    /// Đọc connection string từ App.config (không hardcode).
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly string ConnString =
            ConfigurationManager.ConnectionStrings["QLDSV"].ConnectionString;

        private static DbConnectionFactory _factory;

        /// <summary>
        /// Lấy DbConnectionFactory (singleton).
        /// </summary>
        public static DbConnectionFactory GetConnectionFactory()
        {
            if (_factory == null)
                _factory = new DbConnectionFactory(ConnString);
            return _factory;
        }

        /// <summary>
        /// Tạo AuthService mới (sử dụng TaiKhoanRepository).
        /// </summary>
        public static AuthService GetAuthService()
        {
            var factory = GetConnectionFactory();
            var repo = new TaiKhoanRepository(factory);
            return new AuthService(repo);
        }
    }
}
