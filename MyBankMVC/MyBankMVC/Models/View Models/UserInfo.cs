using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Models.View_Models
{
    [Serializable]
    public class UserInfo
    {
        public string username { get; set; }
        public string chkAccNumber { get; set; }
    }
}