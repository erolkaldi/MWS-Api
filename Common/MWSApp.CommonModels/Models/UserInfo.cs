using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonModels.Models
{
    public class UserInfo
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public Guid UserId { get; set; }
        public string DisplayName { get; set; } = "";
        public Guid CompanyId { get; set; }
    }
}
