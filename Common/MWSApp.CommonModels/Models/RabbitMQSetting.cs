using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonModels.Models
{
    public class RabbitMQSetting
    {
        public string RabbitUri { get; set; } 
        public string RabbitUser { get; set; } 
        public string RabbitPassword { get; set; } 
        public string RabbitQueue { get; set; } 
    }
}
