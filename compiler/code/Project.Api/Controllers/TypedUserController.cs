using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<User> Get(Guid tenantIdParam, Guid userIdParam)
        {
            try
            {
                //TenantIdExistsQueryHandler.Execute(new TenantIdExistsQuery(tenantId));
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }

            try
            {
                //var user = UserQueryHandler.Execute(new UserQuery(tenantId, userId));
                //return new JsonResult(user);
            }
            catch (Exception)
            {
            }
            return NotFound();
        }
    }
}
