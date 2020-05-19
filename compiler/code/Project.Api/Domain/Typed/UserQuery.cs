using System;

namespace Project.Api.Domain.Typed
{
    public class UserQuery
    {
        public UserQuery(UserId id, KnownTenantId tenantId)
        {
            Id = id;
            TenantId = tenantId;
        }

        public UserId Id { get; }
        public KnownTenantId TenantId { get; }
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
