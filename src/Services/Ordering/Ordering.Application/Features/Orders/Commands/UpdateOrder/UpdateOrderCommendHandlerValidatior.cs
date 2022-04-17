﻿using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommendHandlerValidatior : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommendHandlerValidatior()
        {
            RuleFor(r => r.UserName)
               .NotEmpty().WithMessage("{UserName} is required")
               .NotNull()
               .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("{EmailAddress} is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }
    }
}