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
        private string connectionString =
            @"Data Source=localhost\SQL2022DEV;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";

        private SqlConnection conn;

        public LopHocPhanDAL()
        {
            conn = new SqlConnection(connectionString);
        }

        public DataTable GetDanhSach(string keyword, string maKhoa, string maMon)
        {
            DataTable dt = new DataTable();

            string sql =
                "SELECT lhp.MaLHP, lhp.TenLopHocPhan, " +
                "lhp.Thu + ' (Ca ' + CAST(lhp.CaHoc AS varchar) + ')' AS ThoiGianHoc, " +
                "lhp.PhongHoc, k.TenKhoa, mh.TenMon, gv.HoTen AS TenGiangVien, " +
                "CASE WHEN lhp.TrangThai = 'DangMo' THEN N'Hoạt động' ELSE N'Khóa' END AS TrangThai, " +
                "mh.MaKhoa, lhp.MaMon, lhp.MaGV " +
                "FROM LopHocPhan lhp " +
                "LEFT JOIN MonHoc mh ON lhp.MaMon = mh.MaMon " +
                "LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa " +
                "LEFT JOIN GiangVien gv ON lhp.MaGV = gv.MaGV " +
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

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetKhoa()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetMonHocTheoKhoa(string maKhoa)
        {
            DataTable dt = new DataTable();

            string sql =
                $"SELECT MaMon, TenMon FROM MonHoc WHERE MaKhoa = '{maKhoa}'";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }

        public DataTable GetGiangVienTheoKhoa(string maKhoa)
        {
            DataTable dt = new DataTable();

            string sql =
                $"SELECT MaGV, HoTen FROM GiangVien WHERE MaKhoa = '{maKhoa}'";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            return dt;
        }

        public bool CheckMaExists(string ma)
        {
            conn.Open();

            string sql =
                $"SELECT COUNT(*) FROM LopHocPhan WHERE MaLHP = '{ma}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            int count = (int)cmd.ExecuteScalar();

            conn.Close();

            return count > 0;
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
            string maHKNH)
        {
            conn.Open();

            string sql =
                "INSERT INTO LopHocPhan " +
                "(MaLHP, TenLopHocPhan, CaHoc, Thu, PhongHoc, " +
                "ThoiGianBD, ThoiGianKT, SoLuongToiDa, TrangThai, " +
                "MaMon, MaGV, MaHKNH) " +
                "VALUES " +
                $"('{ma}', N'{ten}', {caHoc}, N'{thu}', N'{phong}', " +
                "'2026-08-15', '2026-12-15', 50, " +
                $"N'{trangThai}', '{maMon}', '{maGV}', '{maHKNH}')";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Update(
            string ma,
            string ten,
            int caHoc,
            string thu,
            string phong,
            string trangThai,
            string maMon,
            string maGV)
        {
            conn.Open();

            string sql =
                "UPDATE LopHocPhan SET " +
                $"TenLopHocPhan = N'{ten}', " +
                $"CaHoc = {caHoc}, " +
                $"Thu = N'{thu}', " +
                $"PhongHoc = N'{phong}', " +
                $"TrangThai = N'{trangThai}', " +
                $"MaMon = '{maMon}', " +
                $"MaGV = '{maGV}' " +
                $"WHERE MaLHP = '{ma}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(string ma)
        {
            conn.Open();

            string sql =
                $"DELETE FROM LopHocPhan WHERE MaLHP = '{ma}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}


