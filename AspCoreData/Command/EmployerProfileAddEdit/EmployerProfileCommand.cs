using AspCoreData.Contract;
using AspCoreDomainModels.Models.EmployeerProfileAddEdit;
using System.Linq;

namespace AspCoreData.Command.EmployerProfileAddEdit
{
    public class EmployerProfileCommand : ICommand<EmployeerProfileModel>
    {
        public readonly IRepository<EmployeerProfile> _empProfileRepository;
        public readonly IUnitOfWork _unitOfWork;
        public EmployerProfileCommand(IUnitOfWork unitOfWork)
        {
            _empProfileRepository = unitOfWork.GetRepository<EmployeerProfile>();
            _unitOfWork = unitOfWork;
        }

        public EmployeerProfileModel Execute(EmployeerProfileModel obj)
        {
            if (obj.Id == 0)
            {
                var employerProfileObj = new EmployeerProfile { Name = obj.Name, Address = obj.Address, About = obj.About };
                _empProfileRepository.Add(employerProfileObj);
                _unitOfWork.Commit();
                obj.Id = employerProfileObj.Id;
            }
            else
            {
                var userObj = _empProfileRepository.Find(n => n.Id == obj.Id).FirstOrDefault();
                userObj.Name = obj.Name;
                userObj.About = obj.About;
                userObj.Address = obj.Address;
                _empProfileRepository.Update(userObj);
                _unitOfWork.Commit();
            }
            return obj;
        }
    }
}
