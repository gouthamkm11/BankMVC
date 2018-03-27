using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyBankMVC.Utilities
{
    public interface IEntity
    {
        void SetFields(DataRow dr);
    }
}