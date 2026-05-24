using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class GiangVienDAL
    {
        public DataTable LoadGiangVien()
        {
            string sql =
                        @"SELECT gv.MaGV,
                                 gv.HoTen,
                                 gv.GioiTinh,
                                 gv.DiaChi,
                                 gv.SoDT,
                                 gv.Email,
                                 k.TenKhoa
                        FROM GiangVien gv
                        INNER JOIN Khoa k
                        ON gv.MaKhoa = k.MaKhoa";
            return Connection.GetDataToTable(sql);
        }
        public DataTable LoadKhoa()
        {
            string sql =
                "SELECT MaKhoa, TenKhoa FROM Khoa";

            return Connection.GetDataToTable(sql);
        }

    }
}