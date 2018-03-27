using MyBankMVC.Data_Access_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBankMVC.Models.Domain_Models;

namespace MyBankMVC.Business_Layer
{
    public class BusinessLayer : IBusAuthentication
    {
        RepAbstraction _rab = null;
        public BusinessLayer(RepAbstraction rab)
        {
            _rab = rab;
        }

        public BusinessLayer () :this(new RepAbstraction())
        {

        }

        #region BusinessLayer Methods (Same as Repository Methods)
        public string IsValidUser(string uname, string pwd)
        {
            return _rab.IsValidUser(uname, pwd);
        }

        public bool UpdatePassword(string uname, string oldPW, string newPW)
        {
            return _rab.UpdatePassword(uname, oldPW, newPW);
        }

        public string GetUserRoles(string uname)
        {
            return _rab.GetUserRoles(uname);
        }

        public string chkSavingsAcc(string savingaccno)
        {
            return _rab.chkSavingsAcc(savingaccno);
        }

        public string chkCheckingAcc(string checkingaccno)
        {
            return _rab.chkCheckingAcc(checkingaccno);
        }

        public List<TransferHistory> GetTransferHistory(int ChkAccNo)
        {
            return _rab.GetTransferHistory(ChkAccNo);
        }

        public string getSavAccNo(string chkaccno)
        {
            return _rab.getSavAccNo(chkaccno);
        }

        public bool TransferCheckingToSaving(decimal amount, string checkingAccountNum, string savingAccountNum)
        {
            return _rab.TransferCheckingToSaving(amount, checkingAccountNum, savingAccountNum);
        }
        #endregion
    }
}