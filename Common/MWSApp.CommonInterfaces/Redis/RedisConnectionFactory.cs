
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWSApp.CommonModels.Models;

namespace MWSApp.CommonInterfaces.Redis
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisConnectionFactory(string Url)
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(Url));
        }

        public ConnectionMultiplexer Connection()
        {
            return _connectionMultiplexer.Value;
        }
    }
}
