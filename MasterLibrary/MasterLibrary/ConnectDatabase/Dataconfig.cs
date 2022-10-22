using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.ConnectDatabase
{
    public class Dataconfig
    {
        private static string connect = @"Data Source = .\SQLEXPRESS ; Initial Catalog = LoginDatabase ; Integrated Security = True";
        private static SqlConnection sqlcon = null;

        public Dataconfig()
        {
            OpenConnect();
        }

        private static void OpenConnect()
        {
            sqlcon = new SqlConnection(connect);
            sqlcon.Open();
            if(sqlcon.State == System.Data.ConnectionState.Open)
                sqlcon.Close();
        }

        /// <summary>
        ///  Get data from database
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        public static DataTable DataTransport(string sSQL)
        {
            OpenConnect();
            SqlDataAdapter adapter = new SqlDataAdapter(sSQL, sqlcon);
            DataTable dt = new DataTable();
            dt.Clear();
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// insert data to database
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        public static int DataExcution(string sSQL)
        {
            int result = 0;
            OpenConnect();
            if (sqlcon.State != ConnectionState.Closed)
                sqlcon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sSQL;
            result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
