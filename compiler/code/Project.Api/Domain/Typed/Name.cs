using System;
using ValueOf;

namespace Project.Api.Domain.Typed
{
    public class Name : ValueOf<string, Name>
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            if (Value.Length > 100)
            {
                throw new ArgumentException("System only handles names of up to 100 chars long.");
            }
        }
    }
}
