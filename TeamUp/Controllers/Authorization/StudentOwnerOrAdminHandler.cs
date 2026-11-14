using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeamUpAPI.Controllers.Authorization
{
    
    public class StudentOwnerOrAdminHandler : AuthorizationHandler<StudentOwnerOrAdminRequirement, int>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            StudentOwnerOrAdminRequirement requirement,
            int requestedStudentId) 
        {

            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement); 
                return Task.CompletedTask;
            }

            // (Ownership Check)
            var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out int authenticatedStudentId))
            {
                if (authenticatedStudentId == requestedStudentId)
                {
                    context.Succeed(requirement); 
                }
            }
            return Task.CompletedTask;
        }
    }
}