using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
    public class EditProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }


        [Display(Name="UserName")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
