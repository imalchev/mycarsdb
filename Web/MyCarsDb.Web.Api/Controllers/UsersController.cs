namespace MyCarsDb.Web.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using MyCarsDb.Business.Models;
    using MyCarsDb.Business.Managers.Contracts;
    using MyCarsDb.Web.Api.Controllers.Base;

    public class UsersController : BaseController
    {
        private IIdentityManager _identityManager;

        public UsersController(IIdentityManager identityManager)
        {
            _identityManager = identityManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterUser model)
        {
            // TO DO: this should go through ModelValidationAttribute or validation should go to business layer?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IdentityResult result = await _identityManager.CreateAsync(model);
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
