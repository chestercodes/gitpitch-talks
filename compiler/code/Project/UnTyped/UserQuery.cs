using System;

namespace Project.UnTyped
{
    public class UserQuery
    {
        public UserQuery(Guid id, Guid tenantId)
        {
            if (id == default(Guid))
            {
                throw new ArgumentException("id is default value.");
            }

            if (tenantId == default(Guid))
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
        public User Execute(UserQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is default value.");
            }

            var repo = new UserRepo();

            return repo.GetUserOrNull(query.Id, query.TenantId);
        }
    }
}
