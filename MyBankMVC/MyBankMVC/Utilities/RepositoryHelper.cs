using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyBankMVC.Utilities
{
    public class RepositoryHelper
    {
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
            where T : IEntity, new()
        {
            List<T> TList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T tp = new T();
                tp.SetFields(dr);
                TList.Add(tp);
            }
            return TList;
        }
    }
}