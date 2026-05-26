using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class LopNienCheDAL
    {
        // 1. Lấy toàn bộ danh sách lớp niên chế kèm thông tin Khoa và Giảng viên cố vấn
        public DataTable LoadLopNienChe()
        {
            string sql = @"
                SELECT 
                    lnc.MaLopNienChe,
                    lnc.TenLop,
                    lnc.NienKhoa,
                    lnc.MaKhoa,
                    k.TenKhoa,
                    lnc.MaGV,
                    gv.HoTen AS TenGV
                FROM LopNienChe lnc
                LEFT JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa
                LEFT JOIN GiangVien gv ON lnc.MaGV = gv.MaGV";
            return Connection.GetDataToTable(sql);
        }

        // 2. Lấy danh sách khoa phục vụ ComboBox
        public DataTable LoadKhoa()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return Connection.GetDataToTable(sql);
        }

        // 3. Lấy danh sách giảng viên phục vụ ComboBox làm cố vấn
        public DataTable LoadGiangVien()
        {
            string sql = "SELECT MaGV, HoTen FROM GiangVien";
            return Connection.GetDataToTable(sql);
        }

        // 4. Thêm lớp niên chế mới
        public void InsertLopNienChe(string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            string sql = $"INSERT INTO LopNienChe (MaLopNienChe, TenLop, NienKhoa, MaKhoa, MaGV) " +
                         $"VALUES ('{maLop}', N'{tenLop}', N'{nienKhoa}', '{maKhoa}', '{maGV}')";
            Connection.RunSql(sql);
        }

        // 5. Cập nhật lớp niên chế
        public void UpdateLopNienChe(string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            string sql = $"UPDATE LopNienChe SET " +
                         $"TenLop = N'{tenLop}', " +
                         $"NienKhoa = N'{nienKhoa}', " +
                         $"MaKhoa = '{maKhoa}', " +
                         $"MaGV = '{maGV}' " +
                         $"WHERE MaLopNienChe = '{maLop}'";
            Connection.RunSql(sql);
        }

        // 6. Xóa lớp niên chế
        public void DeleteLopNienChe(string maLop)
        {
            string sql = $"DELETE FROM LopNienChe WHERE MaLopNienChe = '{maLop}'";
            Connection.RunSql(sql);
        }

        // 7. Kiểm tra sự tồn tại của khóa chính hoặc bản ghi theo điều kiện
        public bool CheckKey(string sql)
        {
            DataTable table = Connection.GetDataToTable(sql);
            return table != null && table.Rows.Count > 0;
        }
    }
}
