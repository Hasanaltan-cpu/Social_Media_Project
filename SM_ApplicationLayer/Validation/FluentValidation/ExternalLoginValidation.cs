using FluentValidation;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Validation.FluentValidation
{
   public  class ExternalLoginValidation:AbstractValidator<ExternalLoginDto>
    {
        public ExternalLoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a Email adress").EmailAddress().WithMessage("Please Enter a valid E-mail adress.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a Name").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, Maximum 50 character please.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a UserName").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3 , Maximum 50 character please.");

        }
    }
}
