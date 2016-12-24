﻿namespace MyCarsDb.Server.WebApi.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using MyCarsDb.Data.Models;
    using MyCarsDb.Server.WebApi.Auth;
    using MyCarsDb.Server.WebApi.Controllers.Base;
    using MyCarsDb.Server.WebApi.DataTransferModels;

    public class UsersController : BaseController
    {
        private MyCarsDbUserManager _userManager;

        public UsersController()
        {
            // TO DO: this should go through dependency injection containter
            _userManager = Request.GetOwinContext().GetUserManager<MyCarsDbUserManager>();            
        }

        public UsersController(MyCarsDbUserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TO DO: this should go through automapper in future .....
            var newUser = new User { Email = model.Email, UserName = model.Name };
            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return Ok();
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("errorMessages", errorMessage);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);               
            }

            return null;
        }
    }
}