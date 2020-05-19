using System;
using ValueOf;

namespace Project.Api.Domain.Typed
{
    public class UnverifiedTenantId : ValueOf<Guid, UnverifiedTenantId>
    {
        protected override void Validate()
        {
            if (Value == default)
            {
                throw new ArgumentException("Value cannot be the default");
            }
        }
    }

    public class KnownTenantId : ValueOf<Guid, KnownTenantId>
    {
        protected override void Validate()
        {
            if (Value == default)
            {
                throw new ArgumentException("Value cannot be the default");
            }
        }
    }
}
