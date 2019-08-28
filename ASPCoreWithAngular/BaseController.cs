using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using AspCoreDomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ASPCoreWithAngular
{
    public class BaseController : Controller
    {
        public LoggedInUserModel LoggedInUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string[] userInfo = null;
            if (identity.IsAuthenticated)
            {
                userInfo = identity.Claims.Where(n => n.Type.Equals("UserInfo")).FirstOrDefault().Value.Split('|');
                return new LoggedInUserModel
                {
                    RoleId = Convert.ToInt32(userInfo[2]),
                    UserId = Convert.ToInt32(userInfo[0]),
                    UserName = userInfo[1]
                };
            }
            else
                return new LoggedInUserModel();

        }
    }
}
