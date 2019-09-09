using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels.Models.EmployeerProfileAddEdit
{
    public class EmployeerProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public string Address { get; set; }
    }
}
