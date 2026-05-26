using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class CanhBaoHocVuDAL
    {
        // Load danh sách cảnh báo
        public DataTable LoadCanhBao()
        {
            string sql =
            @"SELECT MaCanhBao,
                     NoiDung,
                     LoaiCanhBao
              FROM CanhBaoHocVu";

            return Connection.GetDataToTable(sql);
        }

        // Load danh sách sinh viên bị cảnh báo
        public DataTable LoadCanhBaoSinhVien()
        {
            string sql =
            @"SELECT MaCanhBao,
                     MaSV,
                     MaHKNH,
                     ThoiDiem,
                     LanThu
              FROM CanhBao_SinhVien";

            return Connection.GetDataToTable(sql);
        }

        // Load năm học
        public DataTable LoadNamHoc()
        {
            string sql =
            @"SELECT MaNamHoc,
                     TenNamHoc
              FROM NamHoc";

            return Connection.GetDataToTable(sql);
        }

        // Load học kỳ
        public DataTable LoadHocKy()
        {
            string sql =
            @"SELECT MaLoaiHK,
                     TenLoaiHK
              FROM LoaiHocKy";

            return Connection.GetDataToTable(sql);
        }

        // Load lớp niên chế
        public DataTable LoadLop()
        {
            string sql =
            @"SELECT MaLopNienChe,
                     TenLop
              FROM LopNienChe";

            return Connection.GetDataToTable(sql);
        }
    }
}