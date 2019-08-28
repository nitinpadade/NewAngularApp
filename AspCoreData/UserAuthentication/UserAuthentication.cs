using AspCoreData.Contract;
using AspCoreDomainModels.Models;
using System.Linq;

namespace AspCoreData.UserAuthentication
{
    public class UserAuthentication : IUserAuthentication
    {
        public readonly IRepository<User> _userRepository;
        public UserAuthentication(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public UserAuthenticationModel Get(string userName, string password)
        {
            var user = new UserAuthenticationModel();

            user = _userRepository
                .Find(n => n.UserName.Equals(userName) && n.Password.Equals(password))
                .Select(n => new UserAuthenticationModel
                {
                    Id = n.Id,
                    Name = n.FirstName + " " + n.LastName,
                    RoleId = n.RoleId,
                    IsAuthenticated = true
                }).FirstOrDefault();


            if (user != null && user.IsAuthenticated)
                return user;
            else
                return new UserAuthenticationModel
                {
                    IsAuthenticated = false
                };
        }

        public UserAuthenticationModel Get(int id)
        {
            var user = new UserAuthenticationModel();

            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                user = dbcontext.User
                    .Where(n => n.Id == id)
                    .Select(n => new UserAuthenticationModel
                    {
                        Id = n.Id,
                        Name = n.FirstName + " " + n.LastName,
                        RoleId = n.RoleId,
                        IsAuthenticated = true
                    }).FirstOrDefault();
            }

            if (user != null && user.IsAuthenticated)
                return user;
            else
                return new UserAuthenticationModel
                {
                    IsAuthenticated = false
                };
        }

        /*public UserAuthenticationModel Get(string userName, string password)
        {
            var user = new UserAuthenticationModel();

            user = _userRepository
                .Find(n => n.UserName.Equals(userName) && n.Password.Equals(password))
                .Select(n => new UserAuthenticationModel
                {
                    Id = n.Id,
                    Name = n.FirstName + " " + n.LastName,
                    RoleId = n.RoleId,
                    IsAuthenticated = true
                }).FirstOrDefault();
           

            if (user != null && user.IsAuthenticated)
                return user;
            else
                return new UserAuthenticationModel
                {
                    IsAuthenticated = false
                };
        }

        public static UserAuthenticationModel Get(int id)
        {
            var user = new UserAuthenticationModel();

            using (SchoolDataContext dbcontext = new SchoolDataContext())
            {
                user = dbcontext.User
                    .Where(n => n.Id == id)
                    .Select(n => new UserAuthenticationModel
                    {
                        Id = n.Id,
                        Name = n.FirstName + " " + n.LastName,
                        RoleId = n.RoleId,
                        IsAuthenticated = true
                    }).FirstOrDefault();
            }

            if (user != null && user.IsAuthenticated)
                return user;
            else
                return new UserAuthenticationModel
                {
                    IsAuthenticated = false
                };
        }*/



    }
}
