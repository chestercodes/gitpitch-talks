using System;

namespace Project.Typed
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
}
