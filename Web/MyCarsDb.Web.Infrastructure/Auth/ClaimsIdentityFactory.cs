namespace MyCarsDb.Web.Infrastructure.Auth
{    
    using System;
    using System.Security.Claims;

    using MyCarsDb.Business.Models;

    public class ClaimsIdentityFactory
    {
        public static ClaimsIdentity Create(User user, string authenticationType, params string[] roles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var id = new ClaimsIdentity(authenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32));
            id.AddClaim(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email));
            id.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));

            foreach (var role in roles)
            {
                id.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return id;
        }

        public static ClaimsIdentity Create(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var newIdentity = new ClaimsIdentity(identity);


            return newIdentity;
        }
    }
}