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
        /// Lấy danh sách tất cả sinh viên.
        /// </summary>
        public DataTable GetSinhVien()
        {
            string sql = @"
                SELECT 
                    sv.MaSV AS MaSinhVien,
                    sv.HoTen AS HoVaTen,
                    sv.NgaySinh,
                    sv.GioiTinh,
                    sv.DiaChi,
                    sv.Email,
                    sv.SoDT AS SoDienThoai,
                    lnc.MaKhoa AS MaKhoa,
                    k.TenKhoa,
                    lnc.MaLopNienChe AS MaLopNienChe,
                    lnc.TenLop AS LopNienChe,
                    sv.NienKhoa
                FROM SinhVien sv
                INNER JOIN TaiKhoan tk ON sv.MaTaiKhoan = tk.MaTaiKhoan
                LEFT JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe
                LEFT JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa
                ORDER BY sv.MaSV";

            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy danh sách khoa.
        /// </summary>
        public DataTable GetKhoa()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy danh sách lớp niên chế theo mã khoa.
        /// </summary>
        /// <param name="maKhoa">Mã khoa (có thể là chuỗi rỗng để trả về danh sách trống)</param>
        public DataTable GetLopNienChe(string maKhoa)
        {
            if (string.IsNullOrEmpty(maKhoa))
            {
                // Trả về DataTable trống nhưng có cấu trúc cột
                DataTable dt = new DataTable();
                dt.Columns.Add("MaLop", typeof(string));
                dt.Columns.Add("TenLop", typeof(string));
                return dt;
            }

            string sql = string.Format(@"
                SELECT 
                    MaLopNienChe AS MaLop,
                    TenLop
                FROM LopNienChe
                WHERE MaKhoa = '{0}'
                ORDER BY TenLop", maKhoa);

            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy danh sách lớp niên chế theo mã khoa (được sử dụng để lọc trong combobox).
        /// </summary>
        /// <param name="maKhoa">Mã khoa</param>
        public DataTable LoadLopNienCheTheoKhoa(string maKhoa)
        {
            if (string.IsNullOrEmpty(maKhoa))
            {
                // Trả về DataTable trống nhưng có cấu trúc cột
                DataTable dt = new DataTable();
                dt.Columns.Add("MaLop", typeof(string));
                dt.Columns.Add("TenLop", typeof(string));
                return dt;
            }

            string sql = string.Format(@"
                SELECT 
                    MaLopNienChe AS MaLop,
                    TenLop
                FROM LopNienChe
                WHERE MaKhoa = '{0}'
                ORDER BY TenLop", maKhoa);

            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Tạo mã sinh viên mới.
        /// </summary>
        public string TaoMaSVMoi()
        {
            // Try to get the last ID and increment
            string sql = "SELECT TOP 1 MaSV FROM SinhVien ORDER BY MaSV DESC";
            DataTable dt = Connection.GetDataToTable(sql);
            if (dt.Rows.Count > 0)
            {
                string lastId = dt.Rows[0]["MaSV"].ToString();
                // Assuming format: SV followed by numbers
                if (lastId.StartsWith("SV") && lastId.Length > 2)
                {
                    string numPart = lastId.Substring(2);
                    if (int.TryParse(numPart, out int num))
                    {
                        return "SV" + (num + 1).ToString("D" + numPart.Length);
                    }
                }
            }
            // Fallback to timestamp
            return "SV" + DateTime.Now.ToString("MMddHHmm");
        }

        /// <summary>
        /// Thêm sinh viên mới.
        /// </summary>
        public bool ThemSinhVien(string maSV, string hoTen, DateTime? ngaySinh, bool gioiTinh, string diaChi, string email, string soDT, string maLopNienChe, string nienKhoa)
        {
            string gioiTinhStr = gioiTinh ? "Nam" : "Nữ";
            string ngaySinhStr = ngaySinh.HasValue ? $"'{ngaySinh.Value.ToString("yyyy-MM-dd")}'" : "NULL";

            string sql = $"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, Email, SoDT, MaLopNienChe, NienKhoa, MaTaiKhoan) " +
                         $"VALUES ('{maSV}', N'{hoTen}', {ngaySinhStr}, N'{gioiTinhStr}', N'{diaChi}', N'{email}', N'{soDT}', '{maLopNienChe}', N'{nienKhoa}', NULL)";

            try
            {
                Connection.RunSql(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên.
        /// </summary>
        public bool SuaSinhVien(string maSV, string hoTen, DateTime? ngaySinh, bool gioiTinh, string diaChi, string email, string soDT, string maLopNienChe, string nienKhoa)
        {
            string gioiTinhStr = gioiTinh ? "Nam" : "Nữ";
            string ngaySinhStr = ngaySinh.HasValue ? $"'{ngaySinh.Value.ToString("yyyy-MM-dd")}'" : "NULL";

            string sql = $"UPDATE SinhVien SET " +
                         $"HoTen = N'{hoTen}', " +
                         $"NgaySinh = {ngaySinhStr}, " +
                         $"GioiTinh = N'{gioiTinhStr}', " +
                         $"DiaChi = N'{diaChi}', " +
                         $"Email = N'{email}', " +
                         $"SoDT = N'{soDT}', " +
                         $"MaLopNienChe = '{maLopNienChe}', " +
                         $"NienKhoa = N'{nienKhoa}' " +
                         $"WHERE MaSV = '{maSV}'";

            try
            {
                Connection.RunSql(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa sinh viên theo mã sinh viên.
        /// </summary>
        public bool XoaSinhVien(string maSV)
        {
            string sql = $"DELETE FROM SinhVien WHERE MaSV = '{maSV}'";

            try
            {
                Connection.RunSql(sql);
                return true;
            }
            catch
            {
                return false;
            }
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