
using FluentValidation;
using RapidPayAPI.Constants;
using RapidPayAPI.Domain;
using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Features.Cards.Validators
{
    public class AddCardValidator : AbstractValidator<Card>
    {
        public AddCardValidator() 
        {
            RuleFor(request => request.Number)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Card number es required");
            RuleFor(x => x.Number)
                .Must(x => new RegularExpressionAttribute(ValidationConstants.CardNumberRegex)
                .IsValid(x?.TrimEnd())).WithMessage("Card number must be 15 digits");            ;

            RuleFor(request => request.CardHolderName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Card holder name es required");
            RuleFor(request => request.CardHolderName)
                .Length(1, 200).WithMessage("Card holder name must have between 1 and 200 characters");

            RuleFor(request => request.ExpirationMonth)
                .InclusiveBetween(1,12).WithMessage("Expiration month must be between 1 and 12");

            RuleFor(request => request.ExpirationtYear)
                .InclusiveBetween(2023, 2050).WithMessage("Expiration year must be between 2023 and 2050");

            RuleFor(request => request.CVC)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Card CVC es required");
            RuleFor(x => x.CVC)
                .Must(x => new RegularExpressionAttribute(ValidationConstants.CardCVCRegex)
                .IsValid(x?.TrimEnd())).WithMessage("Card CVC must be 3 digits");

            //used only with case 1 (when must verify card's balance)
            //RuleFor(request => request.Balance)
            //    .GreaterThan(0).WithMessage("Balance must be positive");

        }

    }
}
