using System;

namespace Project.Api.Domain.Typed
{
    public readonly struct UserIdFromPost : IComparable<UserIdFromPost>, IEquatable<UserIdFromPost>
    {
        public Guid Value { get; }

        public UserIdFromPost(Guid value)
        {
            Value = value;
        }

        public static UserIdFromPost New() => new UserIdFromPost(Guid.NewGuid());

        public bool Equals(UserIdFromPost other) => this.Value.Equals(other.Value);
        public int CompareTo(UserIdFromPost other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UserIdFromPost other && Equals(other);
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        public static bool operator ==(UserIdFromPost a, UserIdFromPost b) => a.CompareTo(b) == 0;
        public static bool operator !=(UserIdFromPost a, UserIdFromPost b) => !(a == b);
    }
}
