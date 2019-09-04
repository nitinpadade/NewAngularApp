﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels.UserAddEdit
{
    public class UserAddEditModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
