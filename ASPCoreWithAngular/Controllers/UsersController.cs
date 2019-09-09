using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspCoreData.Contract;
using AspCoreDomainModels.UserAddEdit;
using Microsoft.AspNetCore.Cors;
using AspCoreDomainModels.Models;
using AspCoreDomainModels.Parameters;
using AspCoreData;
using AspCoreDomainModels.Models.UserList;
using Microsoft.Extensions.Primitives;

namespace ASPCoreWithAngular.Controllers
{
    [Produces("application/json")]

    public class UsersController : BaseController
    {

        private readonly IUserList _userList;
        private readonly ICommand<UserAddEditModel> _command;
        private readonly ICommand<int> _deleteCommand;
        private readonly IQueryWithParameters<UserAddEditModel, UserByIdParameter> _userGetQuery;
        private readonly IQueryWithParameters<QueryResult<UserListPaginationModel>, UserListParameter> _userListQuery;
        public UsersController(IUserList userList, ICommand<UserAddEditModel> command, IQueryWithParameters<UserAddEditModel, UserByIdParameter> userGetQuery,
            ICommand<int> deleteCommand, IQueryWithParameters<QueryResult<UserListPaginationModel>, UserListParameter> userListQuery)
        {
            _userList = userList;
            _command = command;
            _userGetQuery = userGetQuery;
            _deleteCommand = deleteCommand;
            _userListQuery = userListQuery;
        }

        [HttpGet]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/Users")]

        public IActionResult Get()
        {
            var result = _userListQuery.Execute(new UserListParameter
            {
                PageSize = 5,
                PageNumber = PageNumber(),
                Direction = false,
                OrderBy = "Id",
                SearchKey = ""
            });
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/Users/{id}")]

        public IActionResult Get(int id)
        {
            var queryParams = new UserByIdParameter { Id = id };
            var result = _userGetQuery.Execute(queryParams);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [EnableCors("Cors")]
        [Route("api/Users/save")]
        public IActionResult Post([FromBody]UserAddEditModel model)
        {
            var result = _command.Execute(model);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Put([FromBody]UserAddEditModel model)
        {
            var result = _command.Execute(model);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = _deleteCommand.Execute(id);
            return Ok(result);
        }
    }
}