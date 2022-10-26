using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonInterfaces.Redis
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set(string key, object value);
        void Delete(string key);
        bool Any(string key);
    }
}
