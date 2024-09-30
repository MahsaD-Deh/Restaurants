using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using System.Net;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirementsHandler(ILogger<MinimumAgeRequirementsHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirements requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("User : {Email}, date of birth : {DB} - Handeling MinAge Requirements",
                currentUser.email,
                currentUser.DateOfBirth);

            if (currentUser.DateOfBirth == null)
            {
                logger.LogInformation("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization Succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;

        }
    }
}
