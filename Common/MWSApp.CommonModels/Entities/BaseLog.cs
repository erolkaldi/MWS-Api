using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonModels.Entities
{
    public class BaseLog :MainBase
    {
        public DateTime ActionDate { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; } = "";
        [MaxLength(50)]
        public string TableName { get; set; } = "";
        [MaxLength(50)]
        public string FieldName { get; set; } = "";
        public string OldValue { get; set; } = "";
        public string NewValue { get; set; } = "";
        public Guid CompanyId { get; set; }
        public Guid TableId { get; set; }
    }
}
