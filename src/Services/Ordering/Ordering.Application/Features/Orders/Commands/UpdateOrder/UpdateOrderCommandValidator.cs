using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        //Validator
        public UpdateOrderCommandValidator()
        {
            //Set rule for UpdateOrderCommand.UserName
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is requied")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");
            //Set rule for UpdateOrderCommand.EmailAddress
            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is requied");
            //Set rule for UpdateOrderCommand.TotalPrice
            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is requied")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero");
        }
    }
}
