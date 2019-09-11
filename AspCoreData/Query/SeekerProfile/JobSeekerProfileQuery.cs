using AspCoreData.Contract;
using AspCoreDomainModels.Models.JobSeekerProfile;
using AspCoreDomainModels.Parameters;
using System.Linq;
using System.Collections.Generic;

namespace AspCoreData.Query.SeekerProfile
{
    public class JobSeekerProfileQuery : IQueryWithParameters<QueryResult<JobSeekerProfileModel>, JobSeekerProfileParameters>
    {

        public readonly IRepository<JobSeekerProfile> _seekerProfileRepository;
        public readonly IRepository<WorkExperience> _wrkExpRepository;
        public JobSeekerProfileModel _seekerProfile = null;
        public JobSeekerProfileQuery(IUnitOfWork unitOfWork)
        {
            _seekerProfileRepository = unitOfWork.GetRepository<JobSeekerProfile>();
            _wrkExpRepository = unitOfWork.GetRepository<WorkExperience>();
            _seekerProfile = new JobSeekerProfileModel();
        }

        public QueryResult<JobSeekerProfileModel> Execute(JobSeekerProfileParameters parameters)
        {
            try
            {
                if (parameters.LoggedInUser.UserId > 0)
                    _seekerProfile = _seekerProfileRepository.Find(n => n.UserId == parameters.LoggedInUser.UserId)
                        .Select(n => new JobSeekerProfileModel
                        {
                            Id = n.Id,
                            HeadLine = n.HeadLine,
                            ProfileSummary = n.ProfileSummary,
                            UserId = n.UserId,
                            KeySkills = n.KeySkills,
                            Gender = n.Gender,
                            PermanentAddress = n.PermanentAddress,
                            CurrentAddress = n.CurrentAddress
                        }).FirstOrDefault();

                if (_seekerProfile != null)
                {
                    _seekerProfile.WorkExperience = _wrkExpRepository.Find(n => n.ProfileId == _seekerProfile.Id)
                        .Select(n => new WorkExperienceModel
                        {
                            Id = n.Id,
                            Designation = n.Designation,
                            Organisation = n.Organisation,
                            IsCurrent = n.IsCurrent,
                            StartDate = n.StartDate,
                            EndDate = n.EndDate,
                            JobProfile = n.JobProfile,
                            ProfileId = n.ProfileId
                        }).ToList();
                }

                return new QueryResult<JobSeekerProfileModel>()
                {
                    Data = _seekerProfile != null ? _seekerProfile : new JobSeekerProfileModel { WorkExperience = new List<WorkExperienceModel>() },
                    IsExecuted = true,
                    Status = CommandQueryStatus.Executed,
                    Message = _seekerProfile != null ? "Query Executed Successfully" : "Records Not Found"

                };
            }
            catch (System.Exception ex)
            {
                return new QueryResult<JobSeekerProfileModel>()
                {
                    Data = new JobSeekerProfileModel { WorkExperience = new List<WorkExperienceModel>() },
                    IsExecuted = true,
                    Status = CommandQueryStatus.Failed,
                    ErrorMessage = ex.ToString(),
                    Message = "Error Occured While Processing Your Request"
                };
            }
        }
    }
}
