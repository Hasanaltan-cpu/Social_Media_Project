using FluentValidation;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Validation.FluentValidation
{
    public class LoginValidation: AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a UserName");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please Enter a Password");
        }
    }
}
