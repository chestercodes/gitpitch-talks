using System;

namespace Project.Api.Domain.Typed
{
    public class ApiModel
    {
        public UserId userId { get; set; }
        public UnverifiedTenantId tenantId { get; set; }
    }
}
