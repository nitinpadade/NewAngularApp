using AspCoreData.Contract;
using AspCoreDomainModels.UserAddEdit;
using System;
using System.Linq;

namespace AspCoreData.Command.UserAddEdit
{
    public class UserAddEditCommand : ICommand<UserAddEditModel>
    {
        public readonly IRepository<User> _userRepository;
        public readonly IUnitOfWork _unitOfWork;
        public UserAddEditCommand(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _unitOfWork = unitOfWork;
        }

        public UserAddEditModel Execute(UserAddEditModel obj)
        {           

            if (obj.Id == 0)
            {
               var userObj = MapToTable(obj); 
                _userRepository.Add(userObj);
                _unitOfWork.Commit();
                obj.Id = userObj.Id;
            }
            else
            {
                var userObj = _userRepository.Find(n => n.Id == obj.Id).FirstOrDefault();
                userObj.FirstName = obj.FirstName;
                userObj.MiddleName = obj.MiddleName;
                userObj.LastName = obj.LastName;
                userObj.DateOfBirth = obj.DateOfBirth;
                userObj.Email = obj.Email;
                userObj.Mobile = obj.Mobile;
                userObj.UserName = obj.UserName;
                userObj.Password = obj.Password;
                userObj.RoleId = obj.RoleId;
                _userRepository.Update(userObj);
                _unitOfWork.Commit();
            }
            return obj;
        }

        User MapToTable(UserAddEditModel obj)
        {
            return new User
            {
                FirstName = obj.FirstName,
                MiddleName = obj.MiddleName,
                LastName = obj.LastName,
                DateOfBirth = obj.DateOfBirth,
                Email = obj.Email,
                Mobile = obj.Mobile,
                UserName = obj.UserName,
                Password = obj.Password,
                RoleId = obj.RoleId
            };
        }
    }
}
