using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonModels.Models
{
    public class MailLogQueueMessage
    {
        public string Body { get; set; } = "";
        public string Subject { get; set; } = "";
        public string To { get; set; } = "";
        public string Error { get; set; } = "";
    }
}
