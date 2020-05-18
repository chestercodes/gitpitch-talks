using System;

namespace Project.UnTyped
{
    public class Implementation
    {
        public void Go()
        {
            var tenantIdExistsQueryHandler = new TenantIdExistsQueryHandler();
            var userQueryHandler = new UserQueryHandler();
            var updateNameHandler = new UpdateUserNameCommandHandler();
            var saveUserHandler = new SaveUserCommandHandler();
            
            var userId = Guid.Parse("10000000-0000-0000-0000-000000000000");
            var tenantId = Guid.Parse("20000000-0000-0000-0000-000000000000");
            
            var tenantIdExistsQuery = new TenantIdExistsQuery(tenantId);
            var tenantIdExists = tenantIdExistsQueryHandler.Execute(tenantIdExistsQuery);

            var userQuery = new UserQuery(tenantId, userId);
            var user = userQueryHandler.Execute(userQuery);

            var updateCommand = new UpdateUserNameCommand(user, "Mr", "Chester");
            var updatedUser = updateNameHandler.Execute(updateCommand);

            var saveCommand = new SaveUserCommand(updatedUser);
            saveUserHandler.Execute(saveCommand);
        }
    }
}
