using System;
using ValueOf;

namespace Project.Api.Domain.Typed
{
    public class UserId : ValueOf<Guid, UserId>
    {
        protected override void Validate()
        {
            if (Value == default)
            {
                throw new ArgumentException("Value cannot be null or empty");
            }
        }
    }
}
