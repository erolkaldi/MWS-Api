using Microsoft.AspNetCore.Http;
using MWSApp.CommonModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CommonInterfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserInfo User
        {
            get
            {
                var user = new UserInfo()
                {
                    UserName = _httpContextAccessor.HttpContext.User.FindFirst("UserName")?.Value ?? "",
                    Email = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value ?? "",
                    UserId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value ?? ""),
                    DisplayName = _httpContextAccessor.HttpContext.User.FindFirst("DisplayName")?.Value ?? "",
                    CompanyId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("CompanyId")?.Value ?? "")
                };
                return user;
            }
        }
        
    }
}
