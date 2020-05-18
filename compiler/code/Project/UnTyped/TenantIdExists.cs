using System;

namespace Project.UnTyped
{
    public class TenantIdExistsQuery
    {
        public TenantIdExistsQuery(Guid tenantId)
        {
            if(tenantId == default(Guid)){
                throw new ArgumentException("tenantId is default value");
            }

            TenantId = tenantId;
        }

        public Guid TenantId { get; }
    }

    public class TenantIdExistsQueryHandler
    {
        public Guid Execute(TenantIdExistsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is null.");
            }

            if(query.TenantId == Guid.Parse("20000000-0000-0000-0000-000000000000"))
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
