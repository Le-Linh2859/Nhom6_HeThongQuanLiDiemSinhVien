using System;
using System.Collections.Generic;
using System.Configuration;
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
            // Nếu conn đã được gán từ bên ngoài (GUI sync) và đang mở → dùng lại
            if (conn != null && conn.State == ConnectionState.Open)
                return;

            // Đọc từ App.config, fallback về hardcode
            connstring = ConfigurationManager.ConnectionStrings["QLDSV"]?.ConnectionString
                ?? @"Data Source=admin-pc\quynhanh;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";

            conn = new SqlConnection(connstring);
            conn.Open();
        }

        public static DataTable GetDataToTable(string sql)
        {
            KetNoi(); // thêm dòng này
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            da.Fill(table);
            return table;
        }

        public static void RunSql(string sql)
        {
            KetNoi(); // thêm dòng này
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static object ExecuteScalar(string sql)
        {
            KetNoi(); // thêm dòng này
            SqlCommand cmd = new SqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            cmd.Dispose();
            return result;
        }

        public static void ExecuteNonQuery(string sql)
        {
            RunSql(sql); // dùng lại RunSql đã có sẵn
        }
    }
}
