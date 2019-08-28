using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels.Models
{
    public class UserAuthenticationModel
    {
        public string Name { get; set; } 

        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Role { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Token { get; set; }

    }
}
