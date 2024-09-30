using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Model;

namespace Restaurants.Application.Users.Commands.Update
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
        IUserContext userContext,
        IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user : {UserId}, with {@Request}", user!.id, request);

            var dbUser = await userStore.FindByIdAsync(user!.id, cancellationToken);

            if (dbUser == null)
            {
                throw new Exception("Not Found");
            }

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbUser, cancellationToken);

        }
    }
}
