using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class LopHocPhanDAL
    {
        public DataTable GetDanhSach(string keyword, string maKhoa, string maMon)
        {
            DataTable dt = new DataTable();

            string sql =
                "SELECT lhp.MaLHP, lhp.TenLopHocPhan, " +
                "lhp.Thu + ' (Ca ' + CAST(lhp.CaHoc AS varchar) + ')' AS ThoiGianHoc, " +
                "lhp.PhongHoc, k.TenKhoa, mh.TenMon, " +
                "hk.TenLoaiHK, nh.TenNamhoc, " +
                "gv.HoTen AS TenGiangVien, " +
                "CASE WHEN lhp.TrangThai = 'DangMo' THEN N'Hoạt động' ELSE N'Khóa' END AS TrangThai, " +
                "lhp.ThoiGianBD, lhp.ThoiGianKT, " +
                "mh.MaKhoa, lhp.MaMon, lhp.MaGV " +
                "FROM LopHocPhan lhp " +
                "LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                "LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa " +
                "LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV " +
                "LEFT JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "LEFT JOIN LoaiHocKy hk ON hk.MaLoaiHK = hknh.MaLoaiHK " +
                "LEFT JOIN Namhoc nh ON nh.MaNamHoc = hknh.MaNamHoc " +
                "WHERE 1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += $" AND (lhp.MaLHP LIKE N'%{keyword}%' " +
                       $"OR lhp.TenLopHocPhan LIKE N'%{keyword}%')";
            }

            if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "ALL")
            {
                sql += $" AND mh.MaKhoa = '{maKhoa}'";
            }

            if (!string.IsNullOrEmpty(maMon) && maMon != "ALL")
            {
                sql += $" AND lhp.MaMon = '{maMon}'";
            }

            return Connection.GetDataToTable(sql);
        }
        //lấy danh sách theo role giảng viên
        public DataTable GetDanhSachTheoGiangVien(string maGV, string keyword, string maMon)
        {
            string escapedMaGV = maGV.Replace("'", "''");
            string sql =
                 "SELECT lhp.MaLHP, lhp.TenLopHocPhan, " +
                 "lhp.Thu + N' (Ca ' + CAST(lhp.CaHoc AS varchar) + N')' AS ThoiGianHoc, " +
                 "lhp.PhongHoc, k.TenKhoa, mh.TenMon, " +
                 "hk.TenLoaiHK, nh.TenNamhoc, " +
                 "gv.HoTen AS TenGiangVien, " +
                 "CASE WHEN lhp.TrangThai = 'DangMo' THEN N'Đang mở' ELSE N'Đã đóng' END AS TrangThai, " +
                 "lhp.ThoiGianBD, lhp.ThoiGianKT, " +
                 "mh.MaKhoa, lhp.MaMon, lhp.MaGV " +
                 "FROM LopHocPhan lhp " +
                 "LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                 "LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa " +
                 "LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV " +
                 "LEFT JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                 "LEFT JOIN LoaiHocKy hk ON hk.MaLoaiHK = hknh.MaLoaiHK " +
                 "LEFT JOIN Namhoc nh ON nh.MaNamHoc = hknh.MaNamHoc " +
                 $"WHERE lhp.MaGV = '{escapedMaGV}'";
            if (!string.IsNullOrEmpty(keyword))
            {
                string esc = keyword.Replace("'", "''");
                sql += $" AND (lhp.MaLHP LIKE N'%{esc}%' OR lhp.TenLopHocPhan LIKE N'%{esc}%')";
            }

            if (!string.IsNullOrEmpty(maMon) && maMon != "ALL")
            {
                string escMon = maMon.Replace("'", "''");
                sql += $" AND lhp.MaMon = '{escMon}'";
            }

            return Connection.GetDataToTable(sql);
        }
        public DataTable GetMonHocTheoGiangVien(string maGV)
        {
            string escapedMaGV = maGV.Replace("'", "''");
            string sql =
                "SELECT DISTINCT mh.MaMon, mh.TenMon " +
                "FROM MonHoc mh " +
                "INNER JOIN LopHocPhan lhp ON mh.MaMon = lhp.MaMon " +
                $"WHERE lhp.MaGV = '{escapedMaGV}'";

            return Connection.GetDataToTable(sql);
        }
        //Lấy danh sách theo role sinh viên
        public DataTable GetDanhSachTheoSinhVien(string maSV)
        {
            string escapedMaSV = maSV.Replace("'", "''");
            string sql = $@"
        SELECT 
            lhp.MaLHP, 
            lhp.TenLopHocPhan, 
            lhp.Thu + N' (Ca ' + CAST(lhp.CaHoc AS varchar) + N')' AS ThoiGianHoc, 
            lhp.PhongHoc, 
            k.TenKhoa, 
            mh.TenMon, 
            hk.TenLoaiHK, 
            nh.TenNamhoc, 
            gv.HoTen AS TenGiangVien, 
            CASE 
                WHEN lhp.TrangThai = 'DangMo' THEN N'Đang mở' 
                ELSE N'Đã đóng' 
            END AS TrangThai,
            lhp.ThoiGianBD,
            lhp.ThoiGianKT
        FROM LopHocPhan lhp 
        INNER JOIN DangKyLopHoc dk ON lhp.MaLHP = dk.MaLHP 
        LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon 
        LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa 
        LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV 
        LEFT JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH 
        LEFT JOIN LoaiHocKy hk ON hk.MaLoaiHK = hknh.MaLoaiHK 
        LEFT JOIN Namhoc nh ON nh.MaNamHoc = hknh.MaNamHoc 
        WHERE dk.MaSV = '{escapedMaSV}';";

            return Connection.GetDataToTable(sql);
        }
        public DataTable GetKhoa()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return Connection.GetDataToTable(sql);
            
        }

        public DataTable GetMonHocTheoKhoa(string maKhoa)
        {
            DataTable dt = new DataTable();

            //string sql =
            //    $"SELECT MaMon, TenMon FROM MonHoc WHERE MaKhoa = '{maKhoa}'";
            string sql = $"SELECT MaMon, TenMon FROM MonHoc";
            return Connection.GetDataToTable(sql);
            
        }

        public DataTable GetGiangVienTheoKhoa(string maKhoa)
        {
            DataTable dt = new DataTable();

            string sql =
                $"SELECT MaGV, HoTen FROM GiangVien WHERE MaKhoa = '{maKhoa}'";
            return Connection.GetDataToTable(sql);

        }

        public bool CheckMaExists(string ma)
        {            string sql =
                $"SELECT COUNT(*) FROM LopHocPhan WHERE MaLHP = '{ma}'";
            DataTable table = Connection.GetDataToTable(sql);
            if (table == null || table.Rows.Count == 0)
                return false;

            int count;
            if (!int.TryParse(table.Rows[0][0].ToString(), out count))
                return false;

            return count > 0;
        }
        public string GetMaHKNH(string maLoaiHK, string maNamHoc)
        {
            string sql = $@"
        SELECT MaHKNH
        FROM HocKy_NamHoc
        WHERE MaLoaiHK = '{maLoaiHK}'
          AND MaNamHoc = '{maNamHoc}'";

            DataTable dt = Connection.GetDataToTable(sql);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["MaHKNH"].ToString();

            return "";
        }
        public DataTable GetHocKy()
        {
            string sql = @"
        SELECT MaLoaiHK, TenLoaiHK
        FROM LoaiHocKy";

            return Connection.GetDataToTable(sql);
        }

        public DataTable GetNamHoc()
        {
            string sql = @"
        SELECT MaNamHoc, TenNamhoc
        FROM Namhoc
        ORDER BY TenNamhoc";

            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy thông tin lớp học phần mà sinh viên đang đăng ký cho một môn học cụ thể.
        /// </summary>
        public DataTable GetLopHocPhanTheoMonVaSinhVien(string maMon, string maSV)
        {
            string escMon = maMon.Replace("'", "''");
            string escSV  = maSV.Replace("'", "''");
            string sql = $@"
        SELECT TOP 1
            hk.TenLoaiHK,
            nh.TenNamhoc,
            lhp.ThoiGianBD,
            lhp.ThoiGianKT
        FROM LopHocPhan lhp
        INNER JOIN DangKyLopHoc dk ON lhp.MaLHP = dk.MaLHP
        LEFT JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH
        LEFT JOIN LoaiHocKy hk ON hk.MaLoaiHK = hknh.MaLoaiHK
        LEFT JOIN Namhoc nh ON nh.MaNamHoc = hknh.MaNamHoc
        WHERE lhp.MaMon = '{escMon}'
          AND dk.MaSV   = '{escSV}'";
            return Connection.GetDataToTable(sql);
        }


        public void Insert(
            string ma,
            string ten,
            int caHoc,
            string thu,
            string phong,
            string trangThai,
            string maMon,
            string maGV,
            string maHKNH,
            DateTime thoiGianBD,
            DateTime thoiGianKT)
        {
            string bdStr = thoiGianBD.ToString("yyyy-MM-dd");
            string ktStr = thoiGianKT.ToString("yyyy-MM-dd");

            string sql =
                "INSERT INTO LopHocPhan " +
                "(MaLHP, TenLopHocPhan, CaHoc, Thu, PhongHoc, " +
                "ThoiGianBD, ThoiGianKT, SoLuongToiDa, TrangThai, " +
                "MaMon, MaGV, MaHKNH) " +
                "VALUES " +
                $"('{ma}', N'{ten}', {caHoc}, N'{thu}', N'{phong}', " +
                $"'{bdStr}', '{ktStr}', 50, " +
                $"N'{trangThai}', '{maMon}', '{maGV}', '{maHKNH}')";
            Connection.RunSql(sql);
        }

        public void Update(
    string ma,
    string ten,
    int caHoc,
    string thu,
    string phong,
    string trangThai,
    string maMon,
    string maGV,
    string maHKNH,
    DateTime thoiGianBD,
    DateTime thoiGianKT)
        {
            string bdStr = thoiGianBD.ToString("yyyy-MM-dd");
            string ktStr = thoiGianKT.ToString("yyyy-MM-dd");

            string sql =
                "UPDATE LopHocPhan SET " +
                $"TenLopHocPhan = N'{ten}', " +
                $"CaHoc = {caHoc}, " +
                $"Thu = N'{thu}', " +
                $"PhongHoc = N'{phong}', " +
                $"TrangThai = N'{trangThai}', " +
                $"MaMon = '{maMon}', " +
                $"MaGV = '{maGV}', " +
                $"MaHKNH = '{maHKNH}', " +
                $"ThoiGianBD = '{bdStr}', " +
                $"ThoiGianKT = '{ktStr}' " +
                $"WHERE MaLHP = '{ma}'";

            Connection.RunSql(sql);
        }

        public void Delete(string ma)
        {
            string sql =
                $"DELETE FROM LopHocPhan WHERE MaLHP = '{ma}'";
            Connection.RunSql(sql);
            
        }
    }
}


