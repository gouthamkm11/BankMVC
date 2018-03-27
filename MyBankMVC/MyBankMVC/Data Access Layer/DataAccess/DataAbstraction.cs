using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.DataAccess
{
    public class DataAbstraction : IDataAccess
    {
        IDataAccess idac = null;

        public DataAbstraction(IDataAccess _idac)
        {
            idac = _idac;
        }

        public DataAbstraction() : this(new DataAccess())
        { }

        #region SQL DataAccess Methods

        public object GetSingleAnswer(string sql, List<SqlParameter> PList)
        {
            try
            {
                return idac.GetSingleAnswer(sql, PList);
            }
            
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetManyRowsnColumns(string sql, List<SqlParameter> PList)
        {
            try
            {
                return idac.GetManyRowsnColumns(sql, PList);
            }

            catch(Exception)
            {
                throw;
            }
            
        }
        public int InsertUpdaterDelete(string sql, List<SqlParameter> PList)
        {
            try
            {
                return idac.InsertUpdaterDelete(sql, PList);
            }
            
            catch(Exception)
            {
                throw;
            }
        }

        #endregion

        #region SQL DataAcess Transaction Methods
        public object GetSingleAnswerTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            try
            {
                return idac.GetSingleAnswerTR(sql, PList, conn, sqtr, bTransaction);
            }
            catch (Exception)
            { throw; }
        }

        public DataTable GetManyRowsColsTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            try
            {
                return idac.GetManyRowsColsTR(sql, PList, conn, sqtr, bTransaction);
            }
            catch (Exception)
            { throw; }
        }
        public int InsertUpdateDeleteTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false)
        {
            try
            {
                return idac.InsertUpdateDeleteTR(sql, PList, conn, sqtr, bTransaction);
            }
            catch (Exception)
            { throw; }
        }
        #endregion
    }
}