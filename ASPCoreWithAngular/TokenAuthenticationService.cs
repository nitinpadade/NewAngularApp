using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspCoreData.Contract;
using AspCoreData.UserAuthentication;
using AspCoreDomainModels.Models;
using ASPCoreWithAngular.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ASPCoreWithAngular
{
    public class TokenAuthenticationService : IAuthenticateService
    {

        private readonly TokenManagement _tokenManagement;
        private readonly IUserAuthentication _userAuthentication;

        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement, IUserAuthentication userAuthentication)
        {
            _tokenManagement = tokenManagement.Value;
            _userAuthentication = userAuthentication;
        }

        public UserAuthenticationModel IsAuthenticated(TokenRequest request)
        {
            var result = _userAuthentication.Get(request.Username, request.Password);
            if (result != null && result.IsAuthenticated)
            {
                var claim = new[]
                    {
                        new Claim(ClaimTypes.Name, result.Name),
                        new Claim("UserInfo", result.Id.ToString() + '|' + result.Name + '|' + result.RoleId.ToString())
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claim,
                    notBefore:DateTime.Now,
                    expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );

                result.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return result;

            }
            else
                return new UserAuthenticationModel { IsAuthenticated = false };
        }

        public UserAuthenticationModel IsAuthenticated(RefreshTokenModel request)
        {
            var result = _userAuthentication.Get(request.Id);
            if (result != null && result.IsAuthenticated)
            {
                var claim = new[]
                    {
                        new Claim(ClaimTypes.Name, result.Name),
                        new Claim("UserInfo", result.Id.ToString() + '|' + result.Name + '|' + result.RoleId.ToString())
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claim,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );

                result.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return result;

            }
            else
                return new UserAuthenticationModel { IsAuthenticated = false };
        }
    }
}
