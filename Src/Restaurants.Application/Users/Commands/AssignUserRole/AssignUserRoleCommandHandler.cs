using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Model;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning userRole : {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail!)
                ?? throw new Exception();

            var role = await roleManager.FindByNameAsync(request.RoleName!)
                ?? throw new Exception();

            await userManager.AddToRoleAsync(user, role.Name!);

        }
    }
}
