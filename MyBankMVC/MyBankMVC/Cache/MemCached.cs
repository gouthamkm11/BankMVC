using Couchbase;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBankMVC.Cache
{
    public class MemCached : IWebCache
    {
        static ClientConfiguration config = null;
        private readonly Cluster cluster = null;
        public MemCached()
        {
            config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://localhost:8091/pools"),
                    //new Uri("http://192.168.56.102:8091/pools"),
                    //new Uri("http://192.168.56.103:8091/pools"),
                    //new Uri("http://192.168.56.104:8091/pools"),
                }
            };
            cluster = new Cluster(config);
        }



    public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Store(string key, object obj)
        {
            try
            {
                using (var cluster = new Cluster(config))
                {
                    using (var bucket = cluster.OpenBucket())
                    {
                        var upsert = bucket.Upsert(key, obj);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }

        }
    }
}