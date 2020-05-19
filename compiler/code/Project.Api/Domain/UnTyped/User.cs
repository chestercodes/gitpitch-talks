using System;

namespace Project.Api.Domain.UnTyped
{
    public class User
    {
        public User(Guid id, Guid tenantId, string name)
        {
            if (id == default)
            {
                throw new ArgumentException("id is the default value");
            }
            UserId = id;

            if (tenantId == default)
            {
                throw new ArgumentException("tenantId is the default value");
            }
            TenantId = tenantId;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty");
            }
            Name = name;
        }

        public Guid UserId { get; }
        public Guid TenantId { get; }
        public string Name { get; }
    }
}
