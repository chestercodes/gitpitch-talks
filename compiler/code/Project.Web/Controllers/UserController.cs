using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project;
using Project.UnTyped;

namespace Project.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private TenantIdExistsQueryHandler TenantIdExistsQueryHandler = new TenantIdExistsQueryHandler();
        private UserQueryHandler UserQueryHandler = new UserQueryHandler();

        [HttpGet]
        [Route("tenant/{tenantId}/user/{userId}")] 
        public ActionResult<User> Get(Guid tenantId, Guid userId)
        {
            try
            {
                TenantIdExistsQueryHandler.Execute(new TenantIdExistsQuery(tenantId));
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }

            try
            {
                var user = UserQueryHandler.Execute(new UserQuery(tenantId, userId));
                return new JsonResult(user);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
