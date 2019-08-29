using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels.Models.UserList
{
    public class UserListPaginationModel
    {
        public List<UserListModel> Data { get; set; }

        public int TotalCount { get; set; }

        public double TotalPages { get; set; }
    }
}
