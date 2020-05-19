using System;

namespace Project.Api.Domain.UnTyped
{
    public class Audit
    {
        public void UserQueryRequestFailed(Guid userId, Guid tenantId)
        {
            Console.WriteLine("");
        }
    }
}
