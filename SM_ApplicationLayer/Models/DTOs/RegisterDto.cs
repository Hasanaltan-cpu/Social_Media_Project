using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
   public  class RegisterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get { return "/images/users/default.jpg"; } }

        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
