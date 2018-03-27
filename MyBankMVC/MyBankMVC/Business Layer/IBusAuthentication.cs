using MyBankMVC.Models.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Business_Layer
{
    public interface IBusAuthentication
    {
        string IsValidUser(string uname, string pwd);
        bool UpdatePassword(string uname, string oldPW, string newPW);
        string GetUserRoles(string uname);
        string chkSavingsAcc(string savingaccno);
        string chkCheckingAcc(string checkingaccno);
        List<TransferHistory> GetTransferHistory(int ChkAccNo);
        string getSavAccNo(string chkaccno);
        bool TransferCheckingToSaving(decimal amount, string checkingAccountNum, string savingAccountNum);
    }
}