using MyBankMVC.Models.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Models.View_Models
{
    public class XferHistory
    {
        public List<TransferHistory> xferlist { get; set; }
    }
}