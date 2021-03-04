using FluentValidation;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Validation.FluentValidation
{
   public  class PostValidation:AbstractValidator<SendPostDto>
    {

        public PostValidation()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Can not be empty").MaximumLength(200).WithMessage("Less than 200 Character please.");
        }
    }
}
