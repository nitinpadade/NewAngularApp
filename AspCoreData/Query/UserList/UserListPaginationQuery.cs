using AspCoreData.Contract;
using AspCoreDomainModels.Models;
using AspCoreDomainModels.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreData.Query.UserList
{
    public class UserListPaginationQuery : IQueryWithParameters<QueryResultList<UserListModel>, UserListParameter>
    {
        public readonly IRepository<User> _userRepository;
        public UserListPaginationQuery(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User>();
        }

        public QueryResultList<UserListModel> Execute(UserListParameter parameters)
        {
            try
            {
                var result = _userRepository.All()
               .Select(n => new UserListModel
               {
                   Id = n.Id,
                   FirstName = n.FirstName,
                   MiddleName = n.MiddleName,
                   LastName = n.LastName,
                   Email = n.Email,
                   Mobile = n.Mobile,
                   DateOfBirth = string.Format("{0:dd/MM/yyyy}", n.DateOfBirth)
               }).ToList();

                return new QueryResultList<UserListModel>()
                {
                    Data = result,
                    TotalCount = result.Count,
                    TotalPages = int.Parse("1"),
                    IsExecuted = true,
                    Status = CommandQueryStatus.Executed,
                    Message = result.Count != 0 ? "Query Executed Successfully" : "Records Not Found"

                };
            }
            catch (Exception ex)
            {

                return new QueryResultList<UserListModel>()
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
