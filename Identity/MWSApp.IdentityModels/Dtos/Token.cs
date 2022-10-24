using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MWSApp.IdentityModels.Dtos
{
    public class Token
    {
        public string Message { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public DateTime Expiration { get; set; }
        public bool Success { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; } = "";
        public string CompanyId { get; set; } = "";
        public DateTime RefreshTokenExpiration { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
