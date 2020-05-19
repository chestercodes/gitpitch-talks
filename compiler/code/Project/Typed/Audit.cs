using System;

namespace Project.Typed
{
    public class Audit
    {
        public void UserQueryRequestFailed(Guid userId, Guid tenantId)
        {
            Console.WriteLine("");
        }
    }
}
