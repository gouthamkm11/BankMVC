using MyBankMVC.Data_Access_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MyBankMVC.Models.Domain_Models;
using MyBankMVC.Utilities;
using MyBankMVC.Cache;
using System.Configuration;

namespace MyBankMVC.Data_Access_Layer.Repository
{
    public class repository : IRepDataAuthentication,IChkBalances,IXferRep
    {
        DataAbstraction _dab = null;
        CacheAbstraction _cab = null;

        public repository(DataAbstraction dab,CacheAbstraction cab)
        {
            _dab = dab;
            _cab = cab;
        }

        public repository() : this(new DataAbstraction(),new CacheAbstraction())
        { }
        
        public string IsValidUser(string uname, string pwd)
        {
            string res = null;
            try
            {
                string sql = "select CheckingAccountNum from dbo.Users where Username=@username and Password=@password";
                List<SqlParameter> Plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                p1.Value = uname;
                Plist.Add(p1);
                SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
                p2.Value = pwd;
                Plist.Add(p2);
                object obj = _dab.GetSingleAnswer(sql, Plist);
                if(obj != null)
                res = obj.ToString();

            }
            catch(Exception)
            {
                throw;
            }

            return res;
        }

        public bool UpdatePassword(string uname, string oldPW, string newPW)
        {
            bool res = false;
            try
            {
                string sql = "update dbo.Users set Password=@password1 where Username=@username and Password=@password";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@password1", SqlDbType.VarChar);
                p1.Value = newPW;
                plist.Add(p1);
                SqlParameter p2 = new SqlParameter("@username", SqlDbType.VarChar);
                p2.Value = uname;
                plist.Add(p2);
                SqlParameter p3 = new SqlParameter("@password", SqlDbType.VarChar);
                p3.Value = oldPW;
                plist.Add(p3);
                int ans = _dab.InsertUpdaterDelete(sql, plist);
                if(ans != 0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            catch(Exception)
            {
                throw;
            }

            return res;
        }

        public string GetUserRoles(string uname)
        {
            string roles = "";
            try
            {
                string sql = "select r.RoleName from dbo.Roles r join dbo.UserRoles ur on r.RoleId = ur.RoleId where ur.UserName=@username";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                p1.Value = uname;
                plist.Add(p1);
                DataTable dt = _dab.GetManyRowsnColumns(sql, plist);
                if(dt!= null)
                {
                    foreach (DataRow dr in dt.Rows)
                        roles += dr["RoleName"].ToString() + "|";
                    if (roles.Length > 0)
                        roles = roles.Substring(0, roles.Length - 1);
                }
            }
            catch(Exception)
            {
                throw;
            }
            return roles;
        }

        public string chkSavingsAcc(string savingaccno)
        {
            string res = null;
            try
            {
                string sql = "select Balance from dbo.SavingAccounts where SavingAccountNumber=@SavingAccountNumber";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@SavingAccountNumber", SqlDbType.VarChar);
                p1.Value = savingaccno;
                plist.Add(p1);
                object obj = _dab.GetSingleAnswer(sql, plist);
                if(obj != null)
                res = obj.ToString();
            }
            catch(Exception)
            {
                throw;
            }
            return res;
        }

        public string chkCheckingAcc(string checkingaccno)
        {
            string res = null;
            try
            {
                string sql = "select Balance from dbo.CheckingAccounts where CheckingAccountNumber=@CheckingAccountNumber";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@CheckingAccountNumber", SqlDbType.VarChar);
                p1.Value = checkingaccno;
                plist.Add(p1);
                object obj = _dab.GetSingleAnswer(sql, plist);
                if(obj != null)
                res = obj.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public List<TransferHistory> GetTransferHistory(int ChkAccNo)
        {
            List<TransferHistory> XferList = null;
            try
            {
                string sql = "select * from dbo.TransferHistory where CheckingAccountNumber=@CheckingAccountNumber";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@CheckingAccountNumber", SqlDbType.VarChar);
                p1.Value = ChkAccNo;
                plist.Add(p1);
                DataTable dt = _dab.GetManyRowsnColumns(sql, plist);
                XferList = RepositoryHelper.ConvertDataTableToList<TransferHistory>(dt);
            }
            catch(Exception)
            {
                throw;
            }
            return XferList;
        }

        public string getSavAccNo(string ChkAccNo)
        {
            string no = null;
            try
            {
                string sql = "select ToAccountNum from dbo.TransferHistory where CheckingAccountNumber = @CheckingAccountNumber";
                List<SqlParameter> plist = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@CheckingAccountNumber", SqlDbType.VarChar);
                p1.Value = ChkAccNo;
                plist.Add(p1);
                object obj = _dab.GetSingleAnswer(sql, plist);
                if(obj!= null)
                {
                    no = obj.ToString();
                }
                return no;
            }

            catch(Exception)
            {
                throw;
            }
        }

        public bool TransferCheckingToSaving(decimal amount, string checkingAccountNum, string savingAccountNum)
        {
            bool ret = false;
            string connstr = ConfigurationManager.ConnectionStrings["BankDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstr);
            SqlTransaction sqtr = null;
            try
            {
                conn.Open();
                sqtr = conn.BeginTransaction();
                string sql1 = "Update CheckingAccounts set Balance=Balance-@Amount where CheckingAccountNumber=@CheckingAccountNumber";
                List<SqlParameter> ParamList = new List<SqlParameter>();
                SqlParameter p1 = new SqlParameter("@CheckingAccountNumber", SqlDbType.BigInt); p1.Value = checkingAccountNum;
                ParamList.Add(p1);
                SqlParameter p2 = new SqlParameter("@Amount", SqlDbType.Decimal);
                p2.Value = amount;
                ParamList.Add(p2);
                int rows = _dab.InsertUpdateDeleteTR(sql1, ParamList, conn, sqtr, true);
                if (rows > 0)
                {
                    string sql2 = "select Balance from CheckingAccounts where CheckingAccountNumber=@CheckingAccountNumber";
                    List<SqlParameter> ParamList2 = new List<SqlParameter>();
                    SqlParameter pa = new SqlParameter("@CheckingAccountNumber", SqlDbType.BigInt);
                    pa.Value = checkingAccountNum;
                    ParamList2.Add(pa);
                    object obj = _dab.GetSingleAnswerTR(sql2, ParamList2, conn, sqtr, true);
                    if (obj != null)
                    {
                        if (decimal.Parse(obj.ToString()) < 0)
                            throw new Exception("Insufficient funds in Checking Account - rolling back transaction");
                    }
                }
                else
                    throw new Exception("Problem in transferring from Checking Account..");
                string sql3 = "Update SavingAccounts set Balance=Balance+@Amount where SavingAccountNumber=@SavingAccountNumber";
                List<SqlParameter> ParamList3 = new List<SqlParameter>();
                SqlParameter pb = new SqlParameter("@SavingAccountNumber", SqlDbType.BigInt);
                pb.Value = savingAccountNum; ParamList3.Add(pb);
                SqlParameter pc = new SqlParameter("@Amount", SqlDbType.Decimal); pc.Value = amount;
                ParamList3.Add(pc);
                rows = _dab.InsertUpdateDeleteTR(sql3, ParamList3, conn, sqtr, true);
                if (rows > 0)
                {
                    sqtr.Commit();
                    ret = true;
                }
                else
                throw new Exception("Problem in transferring to Saving Account..");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            { conn.Close(); }
            return ret;
        }
    }
}