using System;

namespace Project.Api.Domain.UnTyped
{
    public class UserQuery
    {
        public UserQuery(Guid id, Guid tenantId)
        {
            if (id == default)
            {
                throw new ArgumentException("id is default value.");
            }

            if (tenantId == default)
            {
                throw new ArgumentException("id is default value.");
            }
            
            Id = id;
            TenantId = tenantId;
        }

        public Guid Id { get; }
        public Guid TenantId { get; }
    }

    public class UserQueryHandler
    {
        private UserRepo Repo = new UserRepo();

        public User Execute(UserQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is null.");
            }

            return Repo.GetUserOrNull(query.Id, query.TenantId)
                        ?? throw new Exception($"Cannot find user '{query.Id}'");
        }
    }
}
