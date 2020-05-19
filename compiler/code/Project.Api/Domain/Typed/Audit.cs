using System;

namespace Project.Api.Domain.Typed
{
    public class Audit
    {
        public void UserQueryRequestFailed(Guid userId, Guid tenantId)
        {
            Console.WriteLine("");
        }
    }
}
