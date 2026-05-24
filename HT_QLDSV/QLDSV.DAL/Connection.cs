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
            connstring = @"Data Source=ADMIN-PC\QUYNHANH;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";

            conn = new SqlConnection(connstring);

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
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
