using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonInterfaces.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IRedisConnectionFactory redisConnectionFactory;
        private readonly IDatabase database;

        public RedisCacheService(IRedisConnectionFactory redisConnectionFactory)
        {
            this.redisConnectionFactory = redisConnectionFactory;
            database=redisConnectionFactory.Connection().GetDatabase();
        }

        public void Delete(string key)
        {
            database.KeyDelete(key);
        }

        public T Get<T>(string key)
        {
            if(!Any(key)) return default(T);
            return JsonConvert.DeserializeObject<T>(database.StringGet(key));
        }

        public void Set(string key, object value)
        {
            database.StringSet(key, JsonConvert.SerializeObject(value));
        }

        public bool Any(string key)
        {
            return database.KeyExists(key);
        }
    }
}
