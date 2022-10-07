using MWSApp.CommonModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.IdentityEntites.Entities
{
    public class RefreshToken:MainBase
    {
        [MaxLength(50)]
        public string UserId { get; set; } = "";
        public string Token { get; set; } = "";
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
