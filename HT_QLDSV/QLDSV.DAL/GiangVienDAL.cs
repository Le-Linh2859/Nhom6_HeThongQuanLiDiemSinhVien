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
                "SELECT MaGV, HoTen, GioiTinh, DiaChi, SoDT, Email, MaKhoa FROM GiangVien";

            return Connection.GetDataToTable(sql);
        }
    }
}