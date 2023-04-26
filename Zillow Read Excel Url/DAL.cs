using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System;

namespace Zillow
{
    public class DAL
    {
        private SqlConnection connection;
        private SqlCommand command;
        public int Id;
        public DataTable dt;
        public string ProductUrl;
        public string SubCategory;
        public string Sub_SubCategory;

        public DAL()
        {
            connection = new SqlConnection(@"Data Source=DESKTOP-GHKICR3\SQLEXPRESS;Initial Catalog=zillow;Integrated Security=True");
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }

        public ConnectionState GetState()
        {
            return connection.State;
        }
        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        public string ExecuteQuery(string query)
        {
            string msg = "ok";
            try
            {
                OpenConnection();
                command.CommandText = query;
                command.ExecuteNonQuery();
                CloseConnection();

            }
            catch (System.Exception ex)
            {
               return ex.Message;
            }
            return msg;
      }

        public string ExecuteScalar(string query)
        {
            string msg = "ok";
            try
            {
                OpenConnection();
                command.CommandText = query;
                Id = Convert.ToInt32(command.ExecuteScalar());
                CloseConnection();

            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return msg;
        }



        //public string ExecuteSelectionQuery(string query)
        //{

        //    string msg = "ok";
        //    CloseConnection();
        //    try
        //    {
        //        OpenConnection();
        //        command.CommandText = query;
        //        SqlDataReader rdr = command.ExecuteReader();
        //        while (rdr.Read())
        //        {

        //            ProductUrl = rdr.GetString(0);
        //            SubCategory = rdr.GetString(1);
        //            Sub_SubCategory = rdr.GetString(2);

        //        }
        //        rdr.Close();
        //        CloseConnection();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    return msg;
        //}


        public DataTable ExecuteSelectionQuery(string query)
        {
                OpenConnection();
                dt = new DataTable();
                command.CommandText = query;
                //CloseConnection();
                SqlDataAdapter adpt = new SqlDataAdapter(command);
                adpt.Fill(dt);
                CloseConnection();
                return dt;
        }
    }
}
