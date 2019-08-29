using AspCoreData.Contract;
using AspCoreDomainModels.Models;
using AspCoreDomainModels.Models.UserList;
using AspCoreDomainModels.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreData.Query.UserList
{
    public class UserListPaginationQuery : IQueryWithParameters<QueryResult<UserListPaginationModel>, UserListParameter>
    {
        public readonly IRepository<User> _userRepository;
        UserListPaginationModel result = null;

        public UserListPaginationQuery(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            result = new UserListPaginationModel();
        }

        public QueryResult<UserListPaginationModel> Execute(UserListParameter parameters)
        {
            try
            {
                result.TotalCount = _userRepository.Find(n => n.FirstName.ToLower().Contains(parameters.SearchKey.ToLower())).Count();
                result.TotalPages = Math.Ceiling((double)result.TotalCount / parameters.PageSize);

                result.Data = _userRepository.All()
               .Select(n => new UserListModel
               {
                   Id = n.Id,
                   FirstName = n.FirstName,
                   MiddleName = n.MiddleName,
                   LastName = n.LastName,
                   Email = n.Email,
                   Mobile = n.Mobile,
                   DateOfBirth = string.Format("{0:dd/MM/yyyy}", n.DateOfBirth)
               }).Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

                return new QueryResult<UserListPaginationModel>()
                {
                    Data = result,
                    IsExecuted = true,
                    Status = CommandQueryStatus.Executed,
                    Message = result.Data.Count != 0 ? "Query Executed Successfully" : "Records Not Found"

                };
            }
            catch (Exception ex)
            {

                return new QueryResult<UserListPaginationModel>()
                {
                    Data = null,
                    IsExecuted = true,
                    Status = CommandQueryStatus.Failed,
                    ErrorMessage = ex.ToString(),
                    Message = "Error Occured While Processing Your Request"
                };
            }
        }
    }
}
