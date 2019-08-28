using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspCoreData.UserAuthentication;
using AspCoreDomainModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWithAngular.Controllers
{
    public class LoginController : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            UserLoginModel model = new UserLoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(UserLoginModel request)
        {

            if (ModelState.IsValid)
            {
                /* var result = UserAuthenticationAccess.Get(userLoginModel.UserName, userLoginModel.Password);
                 if (result != null && result.IsAuthenticated)
                 {
                     CookieOptions option = new CookieOptions();
                     option.Expires = DateTime.Now.AddMinutes(30);
                     Response.Cookies.Append("UserName", result.Name, option);
                     Response.Cookies.Append("UserId", result.Id.ToString(), option);
                     Response.Cookies.Append("RoleId", result.RoleId.ToString(), option);
                     return RedirectToAction("Index", "Home");
                 }*/

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("UserName", "Nitin Padade", option);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}