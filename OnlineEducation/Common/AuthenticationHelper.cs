using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineEducation.Common
{
    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeUserAttribute(params ClaimType[] claimType) : base(typeof(AuthorizeRoleFilter))
        {
            string permission = string.Join(",", claimType);
            Arguments = new object[] { new Claim(ClaimTypes.Role, permission) };
        }

        public AuthorizeUserAttribute(string permission, ClaimType type) : base(typeof(AuthorizeRoleFilter))
        {
            var typeStr = type.ToString();
            Arguments = new object[] { new Claim(typeStr, permission) };
        }
    }

    public class AuthorizeRoleFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public AuthorizeRoleFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            return;
            var claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == _claim.Value);
            if (claim == null)
            {
                context.Result = new ForbidResult();
            }
            else
            {
                var claims = claim.Value.Split(',');
                //var organizationId = context.RouteData.Values["organizationId"]?.ToString();
                //  if(claims.Contains())
            }
        }
    }

    public enum ClaimType
    {
        Student,
        Professor
    }
}