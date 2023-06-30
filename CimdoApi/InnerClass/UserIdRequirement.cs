using Microsoft.AspNetCore.Authorization;

namespace Cimdo2._0.InnerClass;

public class UserIdRequirement :IAuthorizationRequirement
{
    public int UserId { get; }

    public UserIdRequirement(int userId)
    {
        UserId = userId;
    }
}