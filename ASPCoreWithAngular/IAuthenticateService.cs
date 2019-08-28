using AspCoreData.UserAuthentication;
using AspCoreDomainModels.Models;
using ASPCoreWithAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular
{
    public interface IAuthenticateService
    {
        UserAuthenticationModel IsAuthenticated(TokenRequest request);

        UserAuthenticationModel IsAuthenticated(RefreshTokenModel request);
    }
}
