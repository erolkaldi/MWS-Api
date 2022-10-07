using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CompanyEntities.Entities
{
    public class CompanyUser:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public bool Confirmed { get; set; }
        public bool Creator { get; set; }
    }
}
