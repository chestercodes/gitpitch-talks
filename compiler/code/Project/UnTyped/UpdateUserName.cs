using System;

namespace Project.UnTyped
{
    public class UpdateUserNameCommand
    {
        public UpdateUserNameCommand(User user, string name, string title)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty");
            }
            Name = name;

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("title is null or empty");
            }
            Title = title;
            
            User = user ?? throw new ArgumentException("user is null");
        }

        public string Title { get; }
        public string Name { get; }
        public User User { get; }
    }

    public class UpdateUserNameCommandHandler
    {
        public User Execute(UpdateUserNameCommand cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd is null");
            }

            return new User(
                cmd.User.UserId,
                cmd.User.TenantId,
                cmd.Title,
                cmd.Name);
        }
    }
}
