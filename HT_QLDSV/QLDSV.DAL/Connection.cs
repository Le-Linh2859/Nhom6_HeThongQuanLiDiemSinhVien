using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLDSV.DAL
    {
        public class Connection
        {
            // Chuỗi kết nối đến Server của bạn (Dựa trên ảnh Server Explorer của bạn)
            private string con = "Data Source=LINEZ;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            public SqlConnection GetConnect()
            {
                return new SqlConnection(con);
            }

            // Hàm dùng chung để lấy dữ liệu (đổ vào bảng)
            public DataTable GetDataTable(string sql)
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = GetConnect())
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
                return dt;
            }
        }
    }
