using System;

namespace Project.Api.Domain.Typed
{
    public class UserRepo
    {
        public User GetUserOrNull(UserId userId, UnverifiedTenantId tenantId)
        {
            if(userId.Equals(Guid.Parse("10000000-0000-0000-0000-000000000000"))
                && tenantId.Equals(Guid.Parse("20000000-0000-0000-0000-000000000000")))
            {
                return new User(userId, tenantId, "Chester");
            }

            return null;
        }
    }
}
