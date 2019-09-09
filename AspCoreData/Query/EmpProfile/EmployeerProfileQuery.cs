using AspCoreData.Contract;
using AspCoreDomainModels.Models.EmployeerProfileAddEdit;
using AspCoreDomainModels.Parameters;
using System.Linq;

namespace AspCoreData.Query.EmpProfile
{
    public class EmployeerProfileQuery : IQueryWithParameters<QueryResult<EmployeerProfileModel>, EmployerProfileParameters>
    {
        public readonly IRepository<EmployeerProfile> _empProfileRepository;
        public EmployeerProfileModel _empProfile = null;
        public EmployeerProfileQuery(IUnitOfWork unitOfWork)
        {
            _empProfileRepository = unitOfWork.GetRepository<EmployeerProfile>();
            _empProfile = new EmployeerProfileModel();
        }

        public QueryResult<EmployeerProfileModel> Execute(EmployerProfileParameters parameters)
        {
            try
            {
                if (parameters.LoggedInUser.UserId > 0)
                    _empProfile = _empProfileRepository.Find(n => n.UserId == parameters.LoggedInUser.UserId)
                        .Select(n => new EmployeerProfileModel
                        {
                            Id = n.Id,
                            About = n.About,
                            Address = n.Address,
                            Name = n.Name
                        }).FirstOrDefault();
                
                return new QueryResult<EmployeerProfileModel>()
                {
                    Data = _empProfile,
                    IsExecuted = true,
                    Status = CommandQueryStatus.Executed,
                    Message = _empProfile != null ? "Query Executed Successfully" : "Records Not Found"

                };
            }
            catch (System.Exception ex)
            {
                return new QueryResult<EmployeerProfileModel>()
                {
                    Data = new EmployeerProfileModel(),
                    IsExecuted = true,
                    Status = CommandQueryStatus.Failed,
                    ErrorMessage = ex.ToString(),
                    Message = "Error Occured While Processing Your Request"
                };
            }
        }
    }
}
