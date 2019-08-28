using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels.Models
{
    public class LoggedInUserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

    }
}
