using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace QLDSV.DAL
{
    public class KetQuaDAL
    {
        ///Lấy tỷ lệ phần trăm của từng loại điểm từ bảng LoaiDiem.
        public Dictionary<string, decimal> GetTyLePhanTram()
        {
            const string sql = "SELECT MaLoaiDiem, TyLePhanTram FROM LoaiDiem";
            DataTable dt = Connection.GetDataToTable(sql);
            var tyLe = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in dt.Rows)
                tyLe[row["MaLoaiDiem"].ToString().Trim()] = Convert.ToDecimal(row["TyLePhanTram"]);
            return tyLe;
        }

        
        public DataTable GetNamHoc()
        {
            string sql = "SELECT MaNamHoc, TenNamHoc FROM NamHoc";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetLoaiHocKy()
        {
            string sql = "SELECT MaLoaiHK, TenLoaiHK FROM LoaiHocKy";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetLopHocPhanByGiangVien(string maNamHoc, string maLoaiHK, string maGV)
        {
            string sql =
                "SELECT lhp.MaLHP, (lhp.MaLHP + ' - ' + lhp.TenLopHocPhan) AS DisplayText " +
                "FROM LopHocPhan lhp " +
                "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                $"WHERE hknh.MaNamHoc = '{maNamHoc}' " +
                $"  AND hknh.MaLoaiHK = '{maLoaiHK}' " +
                $"  AND lhp.MaGV = '{maGV}'";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetSinhVienByLopHocPhan(string maLHP)
        {
            string sql =
                "SELECT dklh.MaSV, (dklh.MaSV + ' - ' + sv.HoTen) AS DisplayText " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN SinhVien sv ON dklh.MaSV = sv.MaSV " +
                $"WHERE dklh.MaLHP = '{maLHP}'";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetDiemByLopHocPhan(string maLHP)
        {
            string sql =
                "SELECT " +
                "  sv.MaSV AS [Mã SV], " +
                "  sv.HoTen AS [Họ Tên], " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) AS [Điểm CC], " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) AS [Điểm KT1], " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) AS [Điểm KT2], " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) AS [Điểm CK] " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN SinhVien sv ON dklh.MaSV = sv.MaSV " +
                "LEFT JOIN KetQua kq ON dklh.MaSV = kq.MaSV AND dklh.MaLHP = kq.MaLHP " +
                $"WHERE dklh.MaLHP = '{maLHP}' " +
                "GROUP BY sv.MaSV, sv.HoTen";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetDiemSinhVien(string maSV, string maLHP)
        {
            string sql =
                "SELECT MaLoaiDiem, Diem FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
            return Connection.GetDataToTable(sql);
        }

        
        public string GetMaGVByTaiKhoan(string maTaiKhoan)
        {
            string sql = $"SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '{maTaiKhoan}'";
            return GetSingleValue(sql);
        }

        public string GetMaGVByTenDangNhap(string tenDangNhap)
        {
            string sql = $"SELECT MaGV FROM GiangVien WHERE MaGV = '{tenDangNhap}'";
            return GetSingleValue(sql);
        }

        
        public DataTable GetThongTinSinhVienByTaiKhoan(string maTaiKhoan)
        {
            string sql =
                "SELECT sv.MaSV, sv.HoTen " +
                "FROM SinhVien sv " +
                $"WHERE sv.MaTaiKhoan = '{maTaiKhoan}'";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetThongTinSinhVienDayDu(string maSV)
        {
            string sql =
                "SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.DiaChi, sv.SoDT, sv.Email, " +
                "       sv.NienKhoa, lnc.TenLop, k.TenKhoa, tk.TenDangNhap " +
                "FROM SinhVien sv " +
                "INNER JOIN TaiKhoan tk ON sv.MaTaiKhoan = tk.MaTaiKhoan " +
                "LEFT JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe " +
                "LEFT JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa " +
                $"WHERE sv.MaSV = '{maSV}'";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetNamHocBySinhVien(string maSV)
        {
            string sql =
                "SELECT DISTINCT nh.MaNamHoc, nh.TenNamHoc " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN LopHocPhan lhp ON dklh.MaLHP = lhp.MaLHP " +
                "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "INNER JOIN NamHoc nh ON hknh.MaNamHoc = nh.MaNamHoc " +
                $"WHERE dklh.MaSV = '{maSV}' " +
                "ORDER BY nh.MaNamHoc DESC";
            return Connection.GetDataToTable(sql);
        }

        public DataTable GetNamHocByGiangVien(string maGV)
        {
            string sql =
                "SELECT DISTINCT nh.MaNamHoc, nh.TenNamHoc " +
                "FROM LopHocPhan lhp " +
                "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "INNER JOIN NamHoc nh ON hknh.MaNamHoc = nh.MaNamHoc " +
                $"WHERE lhp.MaGV = '{maGV}' " +
                "ORDER BY nh.MaNamHoc DESC";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetBangDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
        {
            
            string whereNamHoc  = (maNamHoc  == "ALL") ? "" : $"  AND hknh.MaNamHoc = '{maNamHoc}' ";
            string whereLoaiHK  = (maLoaiHK  == "ALL") ? "" : $"  AND hknh.MaLoaiHK = '{maLoaiHK}' ";

            string sql =
                "SELECT " +
                "  mh.MaMon, " +
                "  mh.TenMon, " +
                "  mh.SoTC, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'CC'  THEN kq.Diem END) AS DiemCC, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) AS DiemKT1, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) AS DiemKT2, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem = 'CK'  THEN kq.Diem END) AS DiemThi " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN LopHocPhan lhp ON dklh.MaLHP = lhp.MaLHP " +
                "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                "LEFT  JOIN KetQua kq ON kq.MaSV = dklh.MaSV AND kq.MaLHP = dklh.MaLHP " +
                $"WHERE dklh.MaSV = '{maSV}' " +
                whereNamHoc +
                whereLoaiHK +
                "GROUP BY mh.MaMon, mh.TenMon, mh.SoTC " +
                "ORDER BY mh.MaMon";
            return Connection.GetDataToTable(sql);
        }

        
        public bool KiemTraDiemTonTai(string maSV, string maLHP, string maLoaiDiem)
        {
            string sql =
                $"SELECT 1 FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{maLoaiDiem}'";
            DataTable dt = Connection.GetDataToTable(sql);
            return dt.Rows.Count > 0;
        }

        
        public void ThemDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            string diemStr = diem.ToString(CultureInfo.InvariantCulture);
            string sql =
                $"INSERT INTO KetQua (MaSV, MaLHP, MaLoaiDiem, Diem) " +
                $"VALUES ('{maSV}', '{maLHP}', '{maLoaiDiem}', {diemStr})";
            Connection.RunSql(sql);
        }

        
        public void CapNhatDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            string diemStr = diem.ToString(CultureInfo.InvariantCulture);
            string sql =
                $"UPDATE KetQua SET Diem = {diemStr} " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{maLoaiDiem}'";
            Connection.RunSql(sql);
        }

        
        public void LuuHoacCapNhatDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            if (KiemTraDiemTonTai(maSV, maLHP, maLoaiDiem))
                CapNhatDiem(maSV, maLHP, maLoaiDiem, diem);
            else
                ThemDiem(maSV, maLHP, maLoaiDiem, diem);
        }

        public void XoaDiem(string maSV, string maLHP, string maLoaiDiem)
        {
            string sql =
                $"DELETE FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{maLoaiDiem}'";
            Connection.RunSql(sql);
        }

        public void XoaTatCaDiemSinhVienLop(string maSV, string maLHP)
        {
            string sql =
                $"DELETE FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
            Connection.RunSql(sql);
        }

        private string GetSingleValue(string sql)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand(sql, Connection.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                result = reader.GetValue(0).ToString();
            reader.Close();
            cmd.Dispose();
            return result;
        }

        ///Lấy danh sách năm học
        public DataTable GetDanhSachNamHoc()
        {
            string sql = "SELECT MaNamHoc, TenNamHoc FROM NamHoc ORDER BY MaNamHoc DESC";
            return Connection.GetDataToTable(sql);
        }

        ///Lấy danh sách loại học kỳ 
        public DataTable GetDanhSachLoaiHocKy()
        {
            string sql = "SELECT MaLoaiHK, TenLoaiHK FROM LoaiHocKy ORDER BY MaLoaiHK";
            return Connection.GetDataToTable(sql);
        }

        ///Lấy danh sách lớp niên chế
        public DataTable GetDanhSachLopNienChe()
        {
            string sql = "SELECT MaLopNienChe, TenLop FROM LopNienChe ORDER BY MaLopNienChe";
            return Connection.GetDataToTable(sql);
        }

        
        public DataTable GetKetQuaHocTapAdmin(
            string maNamHoc, string maLoaiHK, string maLopNienChe, string keyword)
        {
            string wNam  = maNamHoc     == "ALL" ? "" : $" AND hknh.MaNamHoc = '{maNamHoc}'";
            string wHK   = maLoaiHK     == "ALL" ? "" : $" AND hknh.MaLoaiHK = '{maLoaiHK}'";
            string wLop  = maLopNienChe == "ALL" ? "" : $" AND sv.MaLopNienChe = '{maLopNienChe}'";
            string wKey  = string.IsNullOrWhiteSpace(keyword) ? ""
                : $" AND (sv.MaSV LIKE N'%{keyword.Replace("'","''")}%'" +
                  $" OR sv.HoTen LIKE N'%{keyword.Replace("'","''")}%')";

          
            string sql =
                "WITH DiemMon AS ( " +
                "  SELECT dklh.MaSV, lhp.MaMon, mh.SoTC, " +
                "    ROUND( " +
                "      ISNULL(SUM(kq.Diem * ld.TyLePhanTram / 100.0), 0), 2) AS DiemTK, " +
                "    CASE WHEN " +
                "      MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END) IS NOT NULL AND " +
                "      MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END) IS NOT NULL AND " +
                "      MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END) IS NOT NULL AND " +
                "      MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END) IS NOT NULL " +
                "    THEN 1 ELSE 0 END AS DaDuDiem " +
                "  FROM DangKyLopHoc dklh " +
                "  INNER JOIN LopHocPhan lhp ON dklh.MaLHP = lhp.MaLHP " +
                "  INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "  INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                "  LEFT  JOIN KetQua kq ON kq.MaSV = dklh.MaSV AND kq.MaLHP = dklh.MaLHP " +
                "  LEFT  JOIN LoaiDiem ld ON ld.MaLoaiDiem = kq.MaLoaiDiem " +
                "  WHERE 1=1" + wNam + wHK +
                "  GROUP BY dklh.MaSV, lhp.MaMon, mh.SoTC " +
                ") " +
                "SELECT sv.MaSV, sv.HoTen, lnc.TenLop, " +
                "  ROUND( " +
                "    SUM(dm.DiemTK * dm.SoTC) / NULLIF(SUM(dm.SoTC), 0) " +
                "  , 2) AS DTB10 " +
                "FROM SinhVien sv " +
                "INNER JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe " +
                "INNER JOIN DiemMon dm ON dm.MaSV = sv.MaSV " +
                // Chỉ tính trung bình trên các môn đã có đủ điểm tổng kết
                "WHERE dm.DaDuDiem = 1" + wLop + wKey +
                " GROUP BY sv.MaSV, sv.HoTen, lnc.TenLop" +
                " ORDER BY sv.MaSV";
            return Connection.GetDataToTable(sql);
        }
        public DataTable GetChiTietDiemSinhVien(
            string maSV, string maNamHoc, string maLoaiHK)
        {
            string wNam = maNamHoc == "ALL" ? "" : $" AND hknh.MaNamHoc = '{maNamHoc}'";
            string wHK  = maLoaiHK == "ALL" ? "" : $" AND hknh.MaLoaiHK = '{maLoaiHK}'";

            string sql =
                "SELECT lhp.MaLHP, mh.TenMon, mh.SoTC, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END) AS DiemCC, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END) AS DiemKT1, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END) AS DiemKT2, " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END) AS DiemThi " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN LopHocPhan lhp ON dklh.MaLHP = lhp.MaLHP " +
                "INNER JOIN HocKy_NamHoc hknh ON lhp.MaHKNH = hknh.MaHKNH " +
                "INNER JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                "LEFT  JOIN KetQua kq ON kq.MaSV = dklh.MaSV AND kq.MaLHP = dklh.MaLHP " +
                $"WHERE dklh.MaSV = '{maSV}'" + wNam + wHK +
                " GROUP BY lhp.MaLHP, mh.TenMon, mh.SoTC" +
                // Chỉ lấy môn đã có đủ 4 điểm thành phần (có điểm tổng kết)
                " HAVING " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END) IS NOT NULL AND " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END) IS NOT NULL AND " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END) IS NOT NULL AND " +
                "  MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END) IS NOT NULL " +
                " ORDER BY mh.TenMon";
            return Connection.GetDataToTable(sql);
        }
    }
}
