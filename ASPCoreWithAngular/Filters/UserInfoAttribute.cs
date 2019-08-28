using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using AspCoreData.UserData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Http;

namespace ASPCoreWithAngular.Filters
{
    [Authorize]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserInfoAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public UserInfoAttribute()
        {

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();

                var userInfo = user.Claims.Where(n => n.Type.Equals("UserInfo")).FirstOrDefault().Value.Split('|');
                SetLoginUserCookie(context, userInfo);
                return;
            }

        }

        public void SetLoginUserCookie(AuthorizationFilterContext context, string[] userInfo)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(30);
            context.HttpContext.Response.Cookies.Append("UserName", userInfo[1], option);
            context.HttpContext.Response.Cookies.Append("UserId", userInfo[0], option);
            context.HttpContext.Response.Cookies.Append("RoleId", userInfo[2], option);
        }

        
    }
}
