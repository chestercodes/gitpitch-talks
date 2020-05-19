using System;

namespace Project.Api.Domain.Typed
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
        public KnownTenantId Execute(TenantIdExistsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is null.");
            }
            if(query.TenantId.Value == Guid.Parse("20000000-0000-0000-0000-000000000000"))
            {
                return KnownTenantId.From(query.TenantId.Value);
            } 
            else 
            {
                throw new Exception($"TenantId '{query.TenantId}' is unknown");
            }
        }
    }
}
