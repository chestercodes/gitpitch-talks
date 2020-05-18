using System;

namespace Project.UnTyped
{
    public class User
    {
        public User(Guid id, Guid tenantId, string name, string title)
        {
            if (id == default(Guid))
            {
                throw new ArgumentException("id is the default value");
            }
            UserId = id;

            if (tenantId == default(Guid))
            {
                throw new ArgumentException("tenantId is the default value");
            }
            TenantId = tenantId;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty");
            }
            Name = name;
            
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("title is null or empty");
            }
            Title = title;
        }

        public Guid UserId { get; }
        public Guid TenantId { get; }
        public string Title { get; }
        public string Name { get; }
    }
}
