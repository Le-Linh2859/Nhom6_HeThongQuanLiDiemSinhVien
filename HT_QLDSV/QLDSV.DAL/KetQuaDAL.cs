using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace QLDSV.DAL
{
    public class KetQuaDAL
    {
        // ─── Lấy danh sách năm học ────────────────────────────────────────────────
        public DataTable GetNamHoc()
        {
            string sql = "SELECT MaNamHoc, TenNamHoc FROM NamHoc";
            return Connection.GetDataToTable(sql);
        }

        // ─── Lấy danh sách loại học kỳ ───────────────────────────────────────────
        public DataTable GetLoaiHocKy()
        {
            string sql = "SELECT MaLoaiHK, TenLoaiHK FROM LoaiHocKy";
            return Connection.GetDataToTable(sql);
        }

        // ─── Lấy lớp học phần theo năm học, học kỳ, giảng viên ───────────────────
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

        // ─── Lấy danh sách sinh viên trong lớp học phần ──────────────────────────
        public DataTable GetSinhVienByLopHocPhan(string maLHP)
        {
            string sql =
                "SELECT dklh.MaSV, (dklh.MaSV + ' - ' + sv.HoTen) AS DisplayText " +
                "FROM DangKyLopHoc dklh " +
                "INNER JOIN SinhVien sv ON dklh.MaSV = sv.MaSV " +
                $"WHERE dklh.MaLHP = '{maLHP}'";
            return Connection.GetDataToTable(sql);
        }

        // ─── Lấy bảng điểm tổng hợp (pivot) của lớp học phần ────────────────────
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

        // ─── Lấy điểm của một sinh viên trong lớp học phần ───────────────────────
        public DataTable GetDiemSinhVien(string maSV, string maLHP)
        {
            string sql =
                "SELECT MaLoaiDiem, Diem FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}'";
            return Connection.GetDataToTable(sql);
        }

        // ─── Lấy MaGV theo MaTaiKhoan ─────────────────────────────────────────────
        public string GetMaGVByTaiKhoan(string maTaiKhoan)
        {
            string sql = $"SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '{maTaiKhoan}'";
            return GetSingleValue(sql);
        }

        // ─── Lấy MaGV theo TenDangNhap (fallback) ────────────────────────────────
        public string GetMaGVByTenDangNhap(string tenDangNhap)
        {
            string sql = $"SELECT MaGV FROM GiangVien WHERE MaGV = '{tenDangNhap}'";
            return GetSingleValue(sql);
        }

        // ═══════════════════════════════════════════════════════════════════════════
        // SINH VIÊN – Kết quả học tập
        // ═══════════════════════════════════════════════════════════════════════════

        // ─── Lấy MaSV và HoTen theo MaTaiKhoan ───────────────────────────────────
        public DataTable GetThongTinSinhVienByTaiKhoan(string maTaiKhoan)
        {
            string sql =
                "SELECT sv.MaSV, sv.HoTen " +
                "FROM SinhVien sv " +
                $"WHERE sv.MaTaiKhoan = '{maTaiKhoan}'";
            return Connection.GetDataToTable(sql);
        }

        // ─── Lấy danh sách NamHoc mà sinh viên có đăng ký lớp học phần ──────────
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

        // ─── Lấy bảng điểm của sinh viên theo năm học và học kỳ ─────────────────
        // Trả về: MaMon, TenMon, SoTC, DiemCC, DiemKT1, DiemKT2, DiemThi
        public DataTable GetBangDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
        {
            // Xây dựng điều kiện lọc linh hoạt
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

        // ─── Kiểm tra bản ghi điểm đã tồn tại chưa ───────────────────────────────
        public bool KiemTraDiemTonTai(string maSV, string maLHP, string maLoaiDiem)
        {
            string sql =
                $"SELECT 1 FROM KetQua " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{maLoaiDiem}'";
            DataTable dt = Connection.GetDataToTable(sql);
            return dt.Rows.Count > 0;
        }

        // ─── Thêm mới điểm ────────────────────────────────────────────────────────
        public void ThemDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            string diemStr = diem.ToString(CultureInfo.InvariantCulture);
            string sql =
                $"INSERT INTO KetQua (MaSV, MaLHP, MaLoaiDiem, Diem) " +
                $"VALUES ('{maSV}', '{maLHP}', '{maLoaiDiem}', {diemStr})";
            Connection.RunSql(sql);
        }

        // ─── Cập nhật điểm ────────────────────────────────────────────────────────
        public void CapNhatDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            string diemStr = diem.ToString(CultureInfo.InvariantCulture);
            string sql =
                $"UPDATE KetQua SET Diem = {diemStr} " +
                $"WHERE MaSV = '{maSV}' AND MaLHP = '{maLHP}' AND MaLoaiDiem = '{maLoaiDiem}'";
            Connection.RunSql(sql);
        }

        // ─── Lưu hoặc cập nhật điểm (upsert) ─────────────────────────────────────
        public void LuuHoacCapNhatDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
        {
            if (KiemTraDiemTonTai(maSV, maLHP, maLoaiDiem))
                CapNhatDiem(maSV, maLHP, maLoaiDiem, diem);
            else
                ThemDiem(maSV, maLHP, maLoaiDiem, diem);
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

        // ═══════════════════════════════════════════════════════════════════════════
        // ADMIN – Theo dõi kết quả học tập
        // ═══════════════════════════════════════════════════════════════════════════

        // ─── Danh mục cho ComboBox Admin ─────────────────────────────────────────

        /// <summary>Lấy danh sách năm học (MaNamHoc, TenNamHoc) cho ComboBox.</summary>
        public DataTable GetDanhSachNamHoc()
        {
            string sql = "SELECT MaNamHoc, TenNamHoc FROM NamHoc ORDER BY MaNamHoc DESC";
            return Connection.GetDataToTable(sql);
        }

        /// <summary>Lấy danh sách loại học kỳ (MaLoaiHK, TenLoaiHK) cho ComboBox.</summary>
        public DataTable GetDanhSachLoaiHocKy()
        {
            string sql = "SELECT MaLoaiHK, TenLoaiHK FROM LoaiHocKy ORDER BY MaLoaiHK";
            return Connection.GetDataToTable(sql);
        }

        /// <summary>Lấy danh sách lớp niên chế (MaLopNienChe, TenLop) cho ComboBox.</summary>
        public DataTable GetDanhSachLopNienChe()
        {
            string sql = "SELECT MaLopNienChe, TenLop FROM LopNienChe ORDER BY MaLopNienChe";
            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy danh sách tổng hợp kết quả học tập của sinh viên theo bộ lọc.
        /// Truyền "ALL" để bỏ qua điều kiện tương ứng.
        /// Trả về: MaSV, HoTen, TenLop, DTB10
        /// </summary>
        public DataTable GetKetQuaHocTapAdmin(
            string maNamHoc, string maLoaiHK, string maLopNienChe, string keyword)
        {
            string wNam  = maNamHoc     == "ALL" ? "" : $" AND hknh.MaNamHoc = '{maNamHoc}'";
            string wHK   = maLoaiHK     == "ALL" ? "" : $" AND hknh.MaLoaiHK = '{maLoaiHK}'";
            string wLop  = maLopNienChe == "ALL" ? "" : $" AND sv.MaLopNienChe = '{maLopNienChe}'";
            string wKey  = string.IsNullOrWhiteSpace(keyword) ? ""
                : $" AND (sv.MaSV LIKE N'%{keyword.Replace("'","''")}%'" +
                  $" OR sv.HoTen LIKE N'%{keyword.Replace("'","''")}%')";

            // CTE bước 1: tính điểm tổng kết từng môn (hệ 10) cho từng SV-LHP
            // CTE bước 2: tính DTB10 có trọng số tín chỉ theo từng sinh viên
            // Tránh nested aggregate (SUM(SoTC * MAX(...))) không hợp lệ trong SQL Server
            string sql =
                "WITH DiemMon AS ( " +
                "  SELECT dklh.MaSV, lhp.MaMon, mh.SoTC, " +
                "    ROUND( " +
                "      ISNULL(MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END), 0) * 0.1  + " +
                "      ISNULL(MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END), 0) * 0.15 + " +
                "      ISNULL(MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END), 0) * 0.15 + " +
                "      ISNULL(MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END), 0) * 0.6, 2) AS DiemTK, " +
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
                "WHERE 1=1" + wLop + wKey +
                " GROUP BY sv.MaSV, sv.HoTen, lnc.TenLop" +
                " ORDER BY sv.MaSV";
            return Connection.GetDataToTable(sql);
        }

        /// <summary>
        /// Lấy chi tiết điểm từng môn của một sinh viên theo bộ lọc.
        /// Trả về: MaLHP, TenMon, SoTC, DiemCC, DiemKT1, DiemKT2, DiemThi
        /// </summary>
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
                " ORDER BY mh.TenMon";
            return Connection.GetDataToTable(sql);
        }
    }
}
