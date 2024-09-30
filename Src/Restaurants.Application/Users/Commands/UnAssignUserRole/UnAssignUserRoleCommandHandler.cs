using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Model;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole
{
    internal class UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UnAssigning userRole : {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail!)
                ?? throw new Exception();

            var role = await roleManager.FindByNameAsync(request.RoleName!)
                ?? throw new Exception();

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
