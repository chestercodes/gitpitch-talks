using System;

namespace Project.Typed
{
    public class TenantIdExistsQuery
    {
        public TenantIdExistsQuery(UnverifiedTenantId tenantId)
        {
            TenantId = tenantId;
        }

        public UnverifiedTenantId TenantId { get; }
    }

    public class TenantIdExistsQueryHandler
    {
        public UnverifiedTenantId Execute(TenantIdExistsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is null.");
            }

            if(query.TenantId.Equals(Guid.Parse("20000000-0000-0000-0000-000000000000")))
            {
                return query.TenantId;
            } 
            else 
            {
                throw new Exception($"TenantId '{query.TenantId}' is unknown");
            }
        }
    }
}
