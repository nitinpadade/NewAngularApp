using AspCoreData.Contract;

namespace AspCoreData.Command.UserDelete
{
    public class UserDeleteCommand : ICommand<int>
    {
        public readonly IRepository<User> _userRepository;
        public readonly IUnitOfWork _unitOfWork;
        public UserDeleteCommand(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _unitOfWork = unitOfWork;
        }

        public int Execute(int obj)
        {
            var _userToDelete = _userRepository.GetById(obj);
            if (_userToDelete != null)
            {
                _userRepository.Delete(_userToDelete);
                _unitOfWork.Commit();
            }
            return -1;
        }
    }
}
