using AspCoreData;
using AspCoreData.Contract;
using AspCoreDomainModels.Models.JobSeekerProfile;
using AspCoreDomainModels.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWithAngular.Controllers
{
    [Produces("application/json")]
   // [Route("api/JobSeekerProfile")]
    public class JobSeekerProfileController : BaseController
    {
        private readonly IQueryWithParameters<QueryResult<JobSeekerProfileModel>, JobSeekerProfileParameters> _query;

        public JobSeekerProfileController(IQueryWithParameters<QueryResult<JobSeekerProfileModel>, JobSeekerProfileParameters> query)
        {
            _query = query;
        }

        [HttpGet]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/JobSeekerProfile")]

        public IActionResult Get()
        {
            var result = _query.Execute(new JobSeekerProfileParameters
            {
                LoggedInUser = LoggedInUserInfo()
            });
            return Ok(result);
        }
    }
}