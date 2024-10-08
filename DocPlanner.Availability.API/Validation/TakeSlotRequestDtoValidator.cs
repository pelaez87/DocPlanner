using DocPlanner.Availability.API.Dto;
using FluentValidation;
using FluentValidation.Validators;

namespace DocPlanner.Availability.API.Validation
{
    public class TakeSlotRequestDtoValidator : AbstractValidator<TakeSlotRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValidator{T}"/> class.
        /// </summary>
        public TakeSlotRequestDtoValidator()
        {
            // We should define how each field should behave:
            // - String lengths
            // - Regex for phone number if needs to be applied
            // - Any other simple data validation like: date should not be in the past

            RuleFor(x => x.SlotDate)
                .NotEmpty();

            RuleFor(x => x.Comments)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.SecondName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Phone)
                .NotEmpty();
        }
    }
}