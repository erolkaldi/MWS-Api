using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonInterfaces.Redis
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
