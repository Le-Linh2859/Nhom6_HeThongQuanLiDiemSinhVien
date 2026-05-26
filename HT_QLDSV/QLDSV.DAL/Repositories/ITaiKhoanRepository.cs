using System.Collections.Generic;
using QLDSV.DAL.Models;

namespace QLDSV.DAL.Repositories
{
    /// <summary>
    /// Interface Repository cho bảng TaiKhoan.
    /// Giúp tách biệt logic truy vấn DB và dễ dàng thay thế/test.
    /// </summary>
    public interface ITaiKhoanRepository
    {
        /// <summary>
        /// Tìm tài khoản theo tên đăng nhập (dùng cho đăng nhập).
        /// </summary>
        TaiKhoan GetByTenDangNhap(string tenDangNhap);

        /// <summary>
        /// Lấy toàn bộ danh sách tài khoản.
        /// </summary>
        List<TaiKhoan> GetAll();

        /// <summary>
        /// Thêm tài khoản mới.
        /// </summary>
        void Insert(TaiKhoan taiKhoan);

        /// <summary>
        /// Cập nhật tài khoản.
        /// </summary>
        void Update(TaiKhoan taiKhoan);

        /// <summary>
        /// Xóa tài khoản theo mã.
        /// </summary>
        void Delete(string maTaiKhoan);
    }
}
