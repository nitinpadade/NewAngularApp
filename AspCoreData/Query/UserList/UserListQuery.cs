using AspCoreData.Contract;
using AspCoreDomainModels.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData.Query.UserList
{
    public class UserListQuery : IUserList
    {
        public readonly IRepository<User> _userRepository;
        public UserListQuery(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public List<UserListModel> Get(LoggedInUserModel userInfo)
        {
            return _userRepository.All()
                .Select(n => new UserListModel
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
    }
}
