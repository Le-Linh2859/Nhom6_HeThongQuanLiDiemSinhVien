using System;
using System.Data;

namespace QLDSV.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu cho bảng SinhVien.
    /// </summary>
    public class SinhVienDAL
    {
        /// <summary>
        /// Lấy thông tin sinh viên theo mã tài khoản.
        /// </summary>
        public DataTable LoadThongTinSinhVien(string maTaiKhoan)
        {
            string sql = $@"
                SELECT 
                    sv.MaSV,
                    sv.HoTen,
                    sv.NgaySinh,
                    sv.GioiTinh,
                    sv.DiaChi,
                    sv.SoDT,
                    sv.Email,
                    sv.NienKhoa,
                    lnc.TenLop,
                    k.TenKhoa,
                    tk.TenDangNhap
                FROM SinhVien sv
                INNER JOIN TaiKhoan tk ON sv.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe
                LEFT JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa
                WHERE sv.MaTaiKhoan = '{maTaiKhoan}'";

            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên (số điện thoại, email, địa chỉ).
        /// </summary>
        public void UpdateSinhVien(string soDT, string email, string diaChi, string maSV)
        {
            string sql = $@"
                UPDATE SinhVien
                SET SoDT = '{soDT}',
                    Email = '{email}',
                    DiaChi = N'{diaChi}'
                WHERE MaSV = '{maSV}'";

            Connection.RunSql(sql);
        }

        /// <summary>
        /// Cập nhật tên đăng nhập cho tài khoản.
        /// </summary>
        public void UpdateTenDangNhap(string tenDangNhap, string maTaiKhoan)
        {
            string sql = $@"
                UPDATE TaiKhoan
                SET TenDangNhap = '{tenDangNhap}'
                WHERE MaTaiKhoan = '{maTaiKhoan}'";

            Connection.RunSql(sql);
        }

        /// <summary>
        /// Cập nhật mật khẩu cho tài khoản.
        /// </summary>
        public void UpdateMatKhau(string matKhau, string maTaiKhoan)
        {
            string sql = $@"
                UPDATE TaiKhoan
                SET MatKhau = '{matKhau}'
                WHERE MaTaiKhoan = '{maTaiKhoan}'";

            Connection.RunSql(sql);
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại trong bảng SinhVien (trừ sinh viên hiện tại).
        /// </summary>
        public bool CheckEmailUpdate(string email, string maSV)
        {
            string sql = $"SELECT * FROM SinhVien WHERE Email = '{email}' AND MaSV <> '{maSV}'";
            return Connection.GetDataToTable(sql).Rows.Count > 0;
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại trong bảng SinhVien (trừ sinh viên hiện tại).
        /// </summary>
        public bool CheckSoDTUpdate(string soDT, string maSV)
        {
            string sql = $"SELECT * FROM SinhVien WHERE SoDT = '{soDT}' AND MaSV <> '{maSV}'";
            return Connection.GetDataToTable(sql).Rows.Count > 0;
        }
    }
}