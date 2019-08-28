using System.Collections.Generic;
using System.Linq;
using AspCoreDomainModels.Models;

namespace AspCoreData.UserData
{
    public static class UserDataAccess
    {
        public static UserListModel Get(int id)
        {
            var user = new UserListModel();
            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                user = dbcontext.User.Where(n => n.Id == id)
                     .Select(n => new UserListModel
                     {
                         Id = n.Id,
                         FirstName = n.FirstName,
                         MiddleName = n.MiddleName,
                         LastName = n.LastName,
                         Email = n.Email,
                         Mobile = n.Mobile,
                         DateOfBirth = string.Format("{0:dd/MM/yyyy}", n.DateOfBirth)
                     }).FirstOrDefault();
            }
            return user;
        }

        

        public static List<UserListModel> Get(LoggedInUserModel userInfo)
        {
            var user = new List<UserListModel>();
            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                user = dbcontext.User.Select(n => new UserListModel
                {
                    Id = n.Id,
                    FirstName = n.FirstName,
                    MiddleName = n.MiddleName,
                    LastName = n.LastName,
                    Email = n.Email,
                    Mobile = n.Mobile,
                    DateOfBirth = string.Format("{0:dd/MM/yyyy}", n.DateOfBirth)
                }).ToList();
            }
            return user;
        }

        public static UserListModel Save(UserListModel obj)
        {
            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                // //dbcontext.User.Add(obj);
                dbcontext.SaveChanges();
            }
            return obj;
        }

        public static UserListModel Update(UserListModel obj)
        {
            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                var updObj = dbcontext.User.FirstOrDefault(n => n.Id == obj.Id);
                updObj.FirstName = obj.FirstName;
                updObj.LastName = obj.LastName;
                updObj.MiddleName = obj.MiddleName;
                //updObj.DateOfBirth = obj.DateOfBirth;
                updObj.Email = obj.Email;
                dbcontext.User.Update(updObj);
                dbcontext.SaveChanges();
            }
            return obj;
        }
    }
}
