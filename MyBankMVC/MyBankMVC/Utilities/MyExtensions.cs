using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MyBankMVC.Utilities
{
    public static class MyExtensions
    {
        // extendig object class to have a method called LosSerialize
        // it will serialize, encrypt and base64 convert the object to a string
        public static string LosSerialize(this object obj)
        {
            try
            {
                var sw = new StringWriter();
                var formatter = new LosFormatter();
                formatter.Serialize(sw, obj);
                return sw.ToString();
            }
            catch(Exception)
            {
                throw;
            }
        }

        // converts en encrypted string back to an object
        public static object LosDeserialize(this String losEncData)
        {
            if (String.IsNullOrEmpty(losEncData))
                return null;
            else
            {
                var formatter = new LosFormatter();
                return formatter.Deserialize(losEncData);
            }
        }

    }
}