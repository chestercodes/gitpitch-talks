using System;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Domain.Typed;

namespace Project.Api.Controllers
{
    [ApiController]
    public class TypedUserController : ControllerBase
    {
        private TenantIdExistsQueryHandler TenantIdExistsQueryHandler = new TenantIdExistsQueryHandler();
        private UserQueryHandler UserQueryHandler = new UserQueryHandler();

        [HttpGet]
        [Route("tenant2/{tenantId}/user/{userId}")]
        public ActionResult<User> Get(Guid tenantId, Guid userId)
        {
            var unverifiedTenantId = UnverifiedTenantId.From(tenantId);
            var userIdVal = UserId.From(userId);

            try
            {
                var knownTenantId = TenantIdExistsQueryHandler.Execute(new TenantIdExistsQuery(unverifiedTenantId));

                try
                {
                    var user = UserQueryHandler.Execute(new UserQuery(userIdVal, knownTenantId));

                    return new JsonResult(user);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }
        }
    }
}
