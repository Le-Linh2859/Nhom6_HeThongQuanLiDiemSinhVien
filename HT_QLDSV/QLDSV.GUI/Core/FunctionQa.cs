using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    internal class FunctionQa
    {
        public static SqlConnection conn;
        public static string connstring;

        public static void ketnoi()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                return;

            connstring = ConfigurationManager.ConnectionStrings["QLDSV"]?.ConnectionString
                ?? @"Data Source=localhost\SQL2022DEV;Initial Catalog=DB_QLDiemSinhVien;Integrated Security=True;Encrypt=False";

            conn = new SqlConnection(connstring);
            conn.Open();
        }
        public static DataTable getdatatotable(string sql)
        {
            SqlDataAdapter mydata = new SqlDataAdapter(sql, FunctionQa.conn);
            DataTable table = new DataTable();
            mydata.Fill(table);
            return table;
        }
        public static void fillcombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter mydata = new SqlDataAdapter(sql, FunctionQa.conn);
            DataTable table = new DataTable();
            mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
        }
        public static string getfieldvalue(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, FunctionQa.conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                ma = reader.GetValue(0).ToString();

            }
            reader.Close();
            return ma;


        }
        public static bool checkkey(string sql)
        {
            SqlDataAdapter mydata = new SqlDataAdapter(sql, FunctionQa.conn);
            DataTable table = new DataTable();
            mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static void runsql(string sql)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = FunctionQa.conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        //Dành cho nút xoá
        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = FunctionQa.conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xóa...", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }


    }
}
