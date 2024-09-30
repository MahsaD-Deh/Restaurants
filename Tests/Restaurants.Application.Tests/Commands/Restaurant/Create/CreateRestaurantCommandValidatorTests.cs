using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Commands.Restaurant.Create.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationError()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "name",
                Category = "category",
                Description = "description",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result =validator.TestValidate(command);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

      
        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "Ne",
                Category = "cy",
                Description = "de",
                ContactEmail = "@test.com",
                PostalCode = "12345",
            };

            var validator = new CreateRestaurantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}