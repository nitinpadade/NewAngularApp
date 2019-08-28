using AspCoreDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData.Contract
{
    public interface IUserAuthentication
    {
        UserAuthenticationModel Get(string userName, string password);

        UserAuthenticationModel Get(int id);
    }
}
