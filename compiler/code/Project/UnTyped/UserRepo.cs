using System;

namespace Project.UnTyped
{
    public class UserRepo
    {
        public User GetUserOrNull(Guid userId, Guid tenantId)
        {
            if (userId == default(Guid))
            {
                throw new ArgumentException("userId is default value.");
            }

            if (tenantId == default(Guid))
            {
                throw new ArgumentException("tenantId is default value.");
            }

            // faking a db or whatever
            return new User(userId, tenantId, "Dr", "Tester");
        }

        public void SaveUser(User user)
        {
            Console.WriteLine($"Saving user {user.UserId}");
        }
    }
}
