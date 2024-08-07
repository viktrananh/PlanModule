using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using System.Security;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace PLAN_MODULE
{
    public class DBConnect
    {
        private static string connectionString;
        public static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;
        public static MySqlCommand Acomman;
        //static MySqlConnection Aconnection;
        //static SqlConnection sqlconnection;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "mariadbsv1.mvn";
            database = "TRACKING_SYSTEM";
            uid = "admin";
            password = "ManuAdmin$123";

        }
        private static bool OpenConnection()
        {
            try
            {
                string MyConString = "SERVER=mariadbsv1.mvn ; DATABASE=TRACKING_SYSTEM ; UID=admin ; PASSWORD=ManuAdmin$123; charset=utf8; SslMode=none;";
                connection = new MySqlConnection(MyConString);
                Acomman = new MySqlCommand();
                Acomman = connection.CreateCommand();
                connection.Open();
                return true;
            }
            catch (Exception Ex)
            {
                string Exm = Ex.Message;
                return false;
            }
        }
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Error");
                return false;
            }
        }
        private static bool ExecuteMySQL(string Comman)
        {
            Acomman.CommandType = CommandType.Text;// vua them
            Acomman.CommandText = Comman;
            Acomman.Connection = connection; // vua them
            // =========================================
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();// bat dau qua trinh transaction
            Acomman.Transaction = transaction;
            //===================================
            try
            {
                Acomman.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string data = ex.Message;
                return false;
            }
        }
        public static bool InsertMySql(string query)
        {


            if (OpenConnection())
            {
                if (ExecuteMySQL(query))
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static DataTable getData(string sql)
        {

            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                MySqlDataAdapter dta = new MySqlDataAdapter(sql, connection);
                dta.Fill(ds, sql);
                DataTable dt = ds.Tables[0];
                CloseConnection();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
