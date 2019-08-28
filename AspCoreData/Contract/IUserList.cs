using AspCoreDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData.Contract
{
    public interface IUserList
    {
        List<UserListModel> Get(LoggedInUserModel userInfo);
    }
}
