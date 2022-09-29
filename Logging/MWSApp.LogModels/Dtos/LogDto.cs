using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.LogModels.Dtos
{
    public class LogDto
    {
        public DateTime ActionDate { get; set; }
        public string UserName { get; set; } = "";
        public string TableName { get; set; } = "";
        public string FieldName { get; set; } = "";
        public string OldValue { get; set; } = "";
        public string NewValue { get; set; } = "";
    }
}
