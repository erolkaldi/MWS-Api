using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MWSApp.IdentityServices.Tools
{
    public class TokenTools
    {
        internal RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }
    }
}
