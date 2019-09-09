
using Microsoft.EntityFrameworkCore;
using System;

namespace AspCoreData
{
    public class SchoolDataContext : DbContext
    {
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<EmployeerProfile> EmployeerProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=NGL010910;Initial Catalog=School;User ID=sa;Password=Password1;MultipleActiveResultSets=True;App=EntityFramework;");
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\VS.2017\\NewAngularApp\\AspCoreData\\App_Data\\MyJobs.mdf;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework;");
        }

    }
}
