using System;

namespace Project.Api.Domain.UnTyped
{
    public class UserRepo
    {
        public User GetUserOrNull(Guid userId, Guid tenantId)
        {
            if(userId == Guid.Parse("10000000-0000-0000-0000-000000000000")
                && tenantId == Guid.Parse("20000000-0000-0000-0000-000000000000"))
            {
                return new User(userId, tenantId, "Chester");
            }

            return null;
        }
    }
}
