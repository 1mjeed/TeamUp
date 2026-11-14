using Microsoft.AspNetCore.Authorization;

namespace TeamUpAPI.Controllers.Authorization
{
    public class StudentOwnerOrAdminRequirement : IAuthorizationRequirement
    {
    }
}
