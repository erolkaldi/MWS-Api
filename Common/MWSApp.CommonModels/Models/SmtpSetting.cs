using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonModels.Models
{
    public class SmtpSetting
    {
        public string Host { get; set; } = "";
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
}
