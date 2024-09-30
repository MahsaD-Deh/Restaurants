using FluentValidation;

namespace Restaurants.Application.Commands.Restaurant.Create
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required!!");

            RuleFor(dto => dto.Category)
                .NotEmpty().WithMessage("Category is required!!");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");


            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Postal code must be in the format XX-XXX.");
        }
    }
}
