using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class MonHocDAL
    {
        // 1. Lấy danh sách môn học kèm tên khoa, hỗ trợ tìm kiếm theo từ khóa và lọc theo khoa
        public DataTable GetMonHocList(string keyword = "", string maKhoa = "ALL")
        {
            string sql = "SELECT mh.MaMon, mh.TenMon, mh.SoTC, k.TenKhoa, mh.MoTa, mh.MaKhoa " +
                         "FROM MonHoc mh LEFT JOIN Khoa k ON mh.MaKhoa = k.MaKhoa WHERE 1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                string escapedKeyword = keyword.Replace("'", "''");
                sql += $" AND (mh.MaMon LIKE '%{escapedKeyword}%' OR mh.TenMon LIKE N'%{escapedKeyword}%')";
            }

            if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "ALL")
            {
                string escapedMaKhoa = maKhoa.Replace("'", "''");
                sql += $" AND mh.MaKhoa = '{escapedMaKhoa}'";
            }

            return Connection.GetDataToTable(sql);
        }
        //danh sách môn học theo khoa của giảng viên
        public DataTable GetDanhSachTheoGiangVien(string maGV, string keyword)
        {
            string escapedMaGV = maGV.Replace("'", "''");
            string sql =
                "SELECT mh.MaMon, mh.TenMon, mh.SoTC, k.TenKhoa, mh.MoTa, mh.MaKhoa " +
                "FROM MonHoc mh " +
                "INNER JOIN Khoa k ON mh.MaKhoa = k.MaKhoa " +
                "INNER JOIN GiangVien gv ON mh.MaKhoa = gv.MaKhoa " +
                $"WHERE gv.MaGV = '{escapedMaGV}'";

            if (!string.IsNullOrEmpty(keyword))
            {
                string esc = keyword.Replace("'", "''");
                sql += $" AND (mh.MaMon LIKE '%{esc}%' OR mh.TenMon LIKE N'%{esc}%')";
            }

            return Connection.GetDataToTable(sql);
        }
        // 2. Lấy danh sách Khoa để nạp ComboBox
        public DataTable LoadKhoa()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return Connection.GetDataToTable(sql);
        }

        // 3. Kiểm tra sự tồn tại của Mã môn học (Khóa chính)
        public bool CheckKeyExist(string maMon)
        {
            string escapedMaMon = maMon.Replace("'", "''");
            string sql = $"SELECT 1 FROM MonHoc WHERE MaMon = '{escapedMaMon}'";
            DataTable table = Connection.GetDataToTable(sql);
            return table != null && table.Rows.Count > 0;
        }

        // 4. Thêm môn học mới
        public void InsertMonHoc(string maMon, string tenMon, int soTC, string maKhoa, string moTa)
        {
            string escMa = maMon.Replace("'", "''");
            string escTen = tenMon.Replace("'", "''");
            string escMaKhoa = maKhoa.Replace("'", "''");
            string escMoTa = moTa.Replace("'", "''");

            string sql = $"INSERT INTO MonHoc (MaMon, TenMon, SoTC, MaKhoa, MoTa) " +
                         $"VALUES ('{escMa}', N'{escTen}', {soTC}, '{escMaKhoa}', N'{escMoTa}')";
            Connection.RunSql(sql);
        }

        // 5. Cập nhật thông tin môn học
        public void UpdateMonHoc(string maMon, string tenMon, int soTC, string maKhoa, string moTa)
        {
            string escMa = maMon.Replace("'", "''");
            string escTen = tenMon.Replace("'", "''");
            string escMaKhoa = maKhoa.Replace("'", "''");
            string escMoTa = moTa.Replace("'", "''");

            string sql = $"UPDATE MonHoc SET TenMon = N'{escTen}', SoTC = {soTC}, MaKhoa = '{escMaKhoa}', " +
                         $"MoTa = N'{escMoTa}' " +
                         $"WHERE MaMon = '{escMa}'";
            Connection.RunSql(sql);
        }

        // 6. Xóa môn học
        public void DeleteMonHoc(string maMon)
        {
            string escMa = maMon.Replace("'", "''");
            string sql = $"DELETE FROM MonHoc WHERE MaMon = '{escMa}'";
            Connection.RunSql(sql);
        }
    }
}
