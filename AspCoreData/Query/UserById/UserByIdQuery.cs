using AspCoreData.Contract;
using AspCoreDomainModels.Models;
using AspCoreDomainModels.Parameters;
using AspCoreDomainModels.UserAddEdit;
using System.Linq;

namespace AspCoreData.Query.UserById
{
    public class UserByIdQuery : IQueryWithParameters<UserAddEditModel, UserByIdParameter>
    {
        public readonly IRepository<User> _userRepository;
        public UserByIdQuery(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public UserAddEditModel Execute(UserByIdParameter parameters)
        {
            if (parameters.Id > 0)
                return _userRepository.Find(n => n.Id == parameters.Id)
                    .Select(n => new UserAddEditModel
                    {
                        Id = n.Id,
                        FirstName = n.FirstName,
                        MiddleName = n.MiddleName,
                        LastName = n.LastName,
                        Email = n.Email,
                        Mobile = n.Mobile,
                        DateOfBirth = string.Format("{0:dd/MM/yyyy}", n.DateOfBirth),
                        RoleId = n.RoleId,
                        UserName = n.UserName,
                        Password = n.Password
                    }).FirstOrDefault();
            else
                return new UserAddEditModel();
        }
    }
}
