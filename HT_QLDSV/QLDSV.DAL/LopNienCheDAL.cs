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
        // Lấy toàn bộ danh sách lớp niên chế kèm thông tin Khoa và Giảng viên cố vấn
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

        // Lấy danh sách khoa 
        public DataTable LoadKhoa()
        {
            string sql = "SELECT MaKhoa, TenKhoa FROM Khoa";
            return Connection.GetDataToTable(sql);
        }

        //Lấy danh sách giảng viên
        public DataTable LoadGiangVien()
        {
            string sql = "SELECT MaGV, HoTen FROM GiangVien";
            return Connection.GetDataToTable(sql);
        }

        //Thêm lớp niên chế mới
        public void InsertLopNienChe(string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            string sql = $"INSERT INTO LopNienChe (MaLopNienChe, TenLop, NienKhoa, MaKhoa, MaGV) " +
                         $"VALUES ('{maLop}', N'{tenLop}', N'{nienKhoa}', '{maKhoa}', '{maGV}')";
            Connection.RunSql(sql);
        }

        // Cập nhật lớp niên chế
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

        public bool ExistsByMaLop(string maLop)
        {
            string sql = $"SELECT MaLopNienChe FROM LopNienChe WHERE MaLopNienChe = '{maLop}'";
            return CheckKey(sql);
        }

        public bool ExistsKhoa(string maKhoa)
        {
            string sql = $"SELECT MaKhoa FROM Khoa WHERE MaKhoa = '{maKhoa}'";
            return CheckKey(sql);
        }

        public bool ExistsGiangVien(string maGV)
        {
            string sql = $"SELECT MaGV FROM GiangVien WHERE MaGV = '{maGV}'";
            return CheckKey(sql);
        }

        //Kiểm tra sự tồn tại của khóa chính hoặc bản ghi theo điều kiện
        public bool CheckKey(string sql)
        {
            DataTable table = Connection.GetDataToTable(sql);
            return table != null && table.Rows.Count > 0;
        }
    }
}
