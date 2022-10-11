using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CompanyModels.Dtos
{
    public class UserCompanyInfo
    {
        public Guid CompanyId { get; set; }
        public bool Creator { get; set; }
        public string CompanyName { get; set; }
    }
}
