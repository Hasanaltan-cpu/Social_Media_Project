using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
    public class LoginDto
    {

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Display(Name ="Remember Me")]

        public bool RememberMe { get; set; }

    }
}
