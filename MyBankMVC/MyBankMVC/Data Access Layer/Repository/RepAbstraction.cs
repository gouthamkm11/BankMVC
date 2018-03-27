using MyBankMVC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBankMVC.Models.Domain_Models;

namespace MyBankMVC.Data_Access_Layer.Repository
{
    public class RepAbstraction : IRepDataAuthentication,IChkBalances,IXferRep
    {
        IRepDataAuthentication _irepdacc = null;
        IChkBalances _ichkBal = null;
        IXferRep _ixfer = null;
        
        public RepAbstraction()
        {
            _irepdacc = GenericFactory<repository,IRepDataAuthentication>.CreateInstance();
            _ichkBal = GenericFactory<repository, IChkBalances>.CreateInstance();
            _ixfer = GenericFactory<repository, IXferRep>.CreateInstance();

        }

        public T CreateInstance<T>(T trep)
            where T : IRepDataAuthentication,IChkBalances, new()
        {
            trep = new T();
            _irepdacc = (IRepDataAuthentication)trep;
            _ichkBal = (IChkBalances)trep;
            return trep;
        }

        #region Rep Data Authentication
        public string IsValidUser(string uname, string pwd)
        {
            return _irepdacc.IsValidUser(uname, pwd);
        }

        public bool UpdatePassword(string uname, string oldPW, string newPW)
        {
            return _irepdacc.UpdatePassword(uname, oldPW, newPW);
        }

        public string GetUserRoles(string uname)
        {
            return _irepdacc.GetUserRoles(uname);
        }

        #endregion

        #region Check Balances

        public string chkSavingsAcc(string savingaccno)
        {
            return _ichkBal.chkSavingsAcc(savingaccno);
        }

        public string chkCheckingAcc(string checkingaccno)
        {
            return _ichkBal.chkCheckingAcc(checkingaccno);
        }

        public string getSavAccNo(string chkaccno)
        {
            return _ichkBal.getSavAccNo(chkaccno);
        }

        public bool TransferCheckingToSaving(decimal amount, string checkingAccountNum, string savingAccountNum)
        {
            return _ichkBal.TransferCheckingToSaving(amount, checkingAccountNum, savingAccountNum);
        }

        #endregion

        #region Transfer
        public List<TransferHistory> GetTransferHistory(int ChkAccNo)
        {
            return _ixfer.GetTransferHistory(ChkAccNo);
        }
        #endregion

    }
}