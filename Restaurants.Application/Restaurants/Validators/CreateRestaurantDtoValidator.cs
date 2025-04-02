using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> _validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);
        
        // No need to check if value is provided for non-nullable fields as FluentValidation handles it (eg: for name)
        
        // Custom validation
        // RuleFor(dto => dto.Category)
        //     .Custom((category, context) =>
        //     {
        //         var isValidCategory = _validCategories.Contains(category);
        //         if (!isValidCategory)
        //         {
        //             context.AddFailure("Category", "Category is not valid");
        //         }
        //     });
        RuleFor(dto => dto.Category)
            .Must(_validCategories.Contains)
            .WithMessage("Category is not valid. Please choose a valid category.");
        
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please enter a valid email address");
        
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{6}$").WithMessage("Please enter a valid postal code");
    }
}