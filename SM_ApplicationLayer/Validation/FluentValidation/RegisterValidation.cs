using FluentValidation;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Validation.FluentValidation
{
    public class RegisterValidation:AbstractValidator<RegisterDto>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a Email address").EmailAddress().WithMessage("Please enter a valid Email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter a Password");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password doesn't match.Please Check again");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, Maximum 50 character please");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can not be empty.").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3,Maximum 50 character please.");
        }
        
    }
}
