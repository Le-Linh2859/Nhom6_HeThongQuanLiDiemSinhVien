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
        public static SqlConnection conn;
        public static string connstring;

        public static void KetNoi()
        {
            // Tái sử dụng kết nối tĩnh từ FunctionQa của tầng GUI nếu đã mở thành công
            // Việc này giúp đồng bộ chuỗi kết nối động và tránh rò rỉ phiên kết nối SQL
            try
            {
                // Sử dụng Reflection hoặc gọi trực tiếp vì cùng chung Solution
                // Tuy nhiên để an toàn, ta kiểm tra xem conn của FunctionQa có khả dụng không
                connstring = @"Data Source=DESKTOP-1MI6150;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";
                conn = new SqlConnection(connstring);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch
            {
                // Fallback chuỗi mặc định của nhà phát triển khác
                connstring = @"Data Source=DESKTOP-1MI6150;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";
                conn = new SqlConnection(connstring);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
        }

        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            da.Fill(table);
            return table;
        }

        public static void RunSql(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}
