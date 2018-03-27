using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Cache
{
    public class CacheAbstraction : IWebCache
    {
        IWebCache _Iwcac = null;
        public CacheAbstraction(IWebCache Iwcac)
        {
            _Iwcac = Iwcac;
        }

        public CacheAbstraction() : this(new MemCached())
        {

        }

        #region Cache Methods
        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Store(string key, object obj)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(string key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}