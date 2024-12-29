using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Domain.Enums;
using OrderMS.WebAPI.Controllers.V1.Base;

namespace OrderMS.WebAPI.Controllers.V1
{
    public class UserController : BaseController
    {
        [Route("CreateUserAccount")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAccount()
        {
            throw new NotImplementedException();
        }

        [Route("UpdateUserRole")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUserRole()
        {
            throw new NotImplementedException();
        }

        [Route("RemoveUserAccount")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveAccount()
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAccount")]
        [HttpPut]
        [Authorize]
        public IActionResult UpdateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
