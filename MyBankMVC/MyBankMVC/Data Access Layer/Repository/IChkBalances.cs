using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.Repository
{
    public interface IChkBalances
    {
        string chkSavingsAcc(string savingaccno);
        string getSavAccNo(string chkaccno);
        string chkCheckingAcc(string checkingaccno);
        bool TransferCheckingToSaving(decimal amount, string checkingAccountNum, string savingAccountNum);
    }
}