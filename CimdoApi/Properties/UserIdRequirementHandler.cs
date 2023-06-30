using Cimdo2._0.InnerClass;
using Microsoft.AspNetCore.Authorization;

namespace Cimdo2._0.Properties;

public class UserIdRequirementHandler : AuthorizationHandler<UserIdRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIdRequirement requirement)
    {
        // Получаем userId из контекста пользователя
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIntId))
        {
            // Проверяем, совпадает ли userId с требуемым значением
            if (userIntId == requirement.UserId)
            {
                context.Succeed(requirement); // Доступ разрешен
            }
        }

        return Task.CompletedTask;
    }
}