using AspCoreData;
using AspCoreData.Contract;
using AspCoreDomainModels.Models.EmployeerProfileAddEdit;
using AspCoreDomainModels.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWithAngular.Controllers
{
    [Produces("application/json")]
    public class EmployerProfileController : BaseController
    {
        private readonly ICommand<EmployeerProfileModel> _command;
        private readonly IQueryWithParameters<QueryResult<EmployeerProfileModel>, EmployerProfileParameters> _query;
        

        public EmployerProfileController(ICommand<EmployeerProfileModel> command, 
            IQueryWithParameters<QueryResult<EmployeerProfileModel>, EmployerProfileParameters> query)
        {
            _command = command;
            _query = query;
        }

        [HttpPost]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/EmployerProfile/save")]
        public IActionResult Post([FromBody]EmployeerProfileModel model)
        {
            var result = _command.Execute(model);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/EmployerProfile")]

        public IActionResult Get()
        {
            var result = _query.Execute(new EmployerProfileParameters
            {
                LoggedInUser = LoggedInUserInfo()
            });
            return Ok(result);
        }
    }
}