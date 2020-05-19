using System;

namespace Project.Api.Domain.Typed
{
    public class UserRepo
    {
        public User GetUserOrNull(UserId userId, KnownTenantId tenantId)
        {
            if(userId.Value == Guid.Parse("10000000-0000-0000-0000-000000000000")
                && tenantId.Value == Guid.Parse("20000000-0000-0000-0000-000000000000"))
            {
                return new User(userId, tenantId, Name.From("Chester"));
            }

            return null;
        }
    }
}
