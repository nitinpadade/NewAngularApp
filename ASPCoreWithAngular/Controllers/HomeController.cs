using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWithAngular.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //read cookie from IHttpContextAccessor  
            string cookieValueFromContext = HttpContext.Request.Cookies["UserName"];

            //read cookie from Request object  
            string cookieValueFromReq = Request.Cookies["UserName"];

            //Response.Cookies.Delete(key); 
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
