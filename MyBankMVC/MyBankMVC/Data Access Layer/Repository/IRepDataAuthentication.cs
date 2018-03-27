using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Data_Access_Layer.Repository
{
    public interface IRepDataAuthentication
    {
        string IsValidUser(string uname, string pwd);
        bool UpdatePassword(string uname, string oldPW, string newPW);
        string GetUserRoles(string uname);
    }
}