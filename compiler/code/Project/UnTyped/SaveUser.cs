using System;

namespace Project.UnTyped
{
    public class SaveUserCommand
    {
        public SaveUserCommand(User user)
        {
            User = user ?? throw new ArgumentNullException("user is null.");
        }

        public User User { get; }
    }

    public class SaveUserCommandHandler
    {
        public void Execute(SaveUserCommand cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentException("cmd is default value.");
            }

            var repo = new UserRepo();

            repo.SaveUser(cmd.User);
        }
    }
}
