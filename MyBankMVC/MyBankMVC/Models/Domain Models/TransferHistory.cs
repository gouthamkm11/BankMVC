using MyBankMVC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MyBankMVC.Models.Domain_Models
{
    public class TransferHistory : IEntity
    {
        public string FrmAccNo { get; set; }
        public string ToAccNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string ChkAccNo { get; set; }

        public void SetFields(DataRow dr)
        {
            this.FrmAccNo = (string)dr["FromAccountNum"];
            this.ToAccNo = (string)dr["FromAccountNum"];
            this.Date = (DateTime)dr["Date"];
            this.Amount = (decimal)dr["Amount"];
            this.ChkAccNo = (string)dr["CheckingAccountNumber"];
        }
    }
}