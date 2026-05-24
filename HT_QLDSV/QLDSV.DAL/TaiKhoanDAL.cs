//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace QLDSV.DAL
//{
//    public class TaiKhoanDAL
//    {
//        Connection conn = new Connection();

//        // Lấy toàn bộ danh sách tài khoản
//        public DataTable GetList()
//        {
//            string sql = @"SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.MatKhau, 
//                                  vt.TenVaiTro
//                           FROM TaiKhoan tk
//                           JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro";
//            return conn.GetDataTable(sql);
//        }
//        // Lấy danh sách vai trò (binding ComboBox)
//        public DataTable GetVaiTroList()
//        {
//            string sql = "SELECT MaVaiTro, TenVaiTro FROM VaiTro";
//            return conn.GetDataTable(sql);
//        }
//    }
//}
