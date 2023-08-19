using FluentValidation;
using Notificator.Tests.Customer;

namespace Notificator.Tests.Validator
{
    public class CustomerTestValidator
        : AbstractValidator<CustomerTest>
    {
        public CustomerTestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The customer's name is necessary")
                .WithSeverity(Severity.Error);
        }
    }
}