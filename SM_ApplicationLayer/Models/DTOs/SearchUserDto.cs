using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
    public class SearchUserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
