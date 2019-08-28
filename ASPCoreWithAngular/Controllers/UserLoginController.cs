using AspCoreDomainModels.Models;
using ASPCoreWithAngular.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWithAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/UserLogin")]
    [EnableCors("Cors")]
    public class UserLoginController : Controller
    {

        private readonly IAuthenticateService _authService;
        public UserLoginController(IAuthenticateService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]UserLoginModel request)
        {
            var tokenRequest = new TokenRequest { Username = request.UserName, Password = request.Password };
            var result = _authService.IsAuthenticated(tokenRequest);
            if (result != null && result.IsAuthenticated)
            {
                return Ok(result);
            }
            else
                return Ok(new UserAuthenticationModel { IsAuthenticated = false });
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody]RefreshTokenModel request)
        {
            var result = _authService.IsAuthenticated(request);
            if (result != null && result.IsAuthenticated)
            {
                return Ok(result);
            }
            else
                return Ok(new UserAuthenticationModel { IsAuthenticated = false });
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            return Ok(new UserAuthenticationModel { IsAuthenticated = false });
        }
    }
}