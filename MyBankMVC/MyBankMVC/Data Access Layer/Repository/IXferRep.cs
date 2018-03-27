using MyBankMVC.Models.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.Repository
{
    public interface IXferRep
    {
        List<TransferHistory> GetTransferHistory(int ChkAccNo);
    }
}