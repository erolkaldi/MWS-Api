using MWSApp.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonInterfaces
{
    public interface IUserRepository
    {
        UserInfo User { get; }
    }
}
