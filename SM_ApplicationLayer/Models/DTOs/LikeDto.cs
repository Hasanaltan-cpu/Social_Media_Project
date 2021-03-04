using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
    public class LikeDto
    {

        public int AppUserId { get; set; }

        public int PostId { get; set; }

        public bool isExist { get; set; }
    }
}
