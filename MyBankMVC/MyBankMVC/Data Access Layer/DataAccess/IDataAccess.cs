using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.DataAccess
{
    public interface IDataAccess
    {
        object GetSingleAnswer(string sql, List<SqlParameter> PList);
        DataTable GetManyRowsnColumns(string sql, List<SqlParameter> PList);
        int InsertUpdaterDelete(string sql, List<SqlParameter> PList);
        object GetSingleAnswerTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false);
        DataTable GetManyRowsColsTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false);
        int InsertUpdateDeleteTR(string sql, List<SqlParameter> PList, SqlConnection conn = null, SqlTransaction sqtr = null, bool bTransaction = false);
    }
}