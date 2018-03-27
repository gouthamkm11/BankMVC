using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.DataAccess
{
    public class DataAccess : IDataAccess
    {
        string connstr = ConfigurationManager.ConnectionStrings["BankDB"].ConnectionString;
        #region DataBase Methods
        public object GetSingleAnswer(string sql, List<SqlParameter> PList)
        {
            object obj = null;
            SqlConnection conn = new SqlConnection(connstr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                if(PList != null)
                {
                    foreach (SqlParameter para in PList)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
                obj = cmd.ExecuteScalar();
            }

            catch(Exception)
            {
                throw;
            }

            finally
            {
                conn.Close();
            }
            return obj;
            
        }
        public DataTable GetManyRowsnColumns(string sql, List<SqlParameter> PList)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connstr);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sql,conn);
                if (PList != null)
                {
                    foreach (SqlParameter para in PList)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
                da.SelectCommand = cmd;
                da.Fill(dt);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        public int InsertUpdaterDelete(string sql, List<SqlParameter> PList)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connstr);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql);
                if (PList != null)
                {
                    foreach (SqlParameter para in PList)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
                result = cmd.ExecuteNonQuery();

            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion

        #region DataBase Transactions Methods
        public object GetSingleAnswerTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            object obj = null;
            if (bTransaction == false) conn = new SqlConnection(connstr);
            try
            {
                if (bTransaction == false) conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (bTransaction == true) cmd.Transaction = sqtr;
                if (PList != null)
                { foreach (SqlParameter p in PList) cmd.Parameters.Add(p); }
                obj = cmd.ExecuteScalar();
            }
            catch (Exception)
            { throw; }
            finally
            { if (bTransaction == false) conn.Close(); }
            return obj;
        }

        public DataTable GetManyRowsColsTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            DataTable dt = new DataTable();
            if (bTransaction == false) conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (PList != null)
                {
                    foreach (SqlParameter p in PList)
                        cmd.Parameters.Add(p);
                }
                if (bTransaction == true)
                    cmd.Transaction = sqtr;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception)
            { throw; }
            finally
            { if (bTransaction == false)
                    conn.Close();
            }
            return dt;
        }
        public int InsertUpdateDeleteTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            int rows = 0;
            if (bTransaction == false) conn = new SqlConnection(connstr);
            try
            {
                if (bTransaction == false)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (bTransaction == true)
                    cmd.Transaction = sqtr;
                if (PList != null)
                {
                    foreach (SqlParameter p in PList)
                        cmd.Parameters.Add(p);
                }
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { throw; }
            finally
            {
                if (bTransaction == false)
                    conn.Close();
            }
            return rows;
        }
            #endregion
        }
}