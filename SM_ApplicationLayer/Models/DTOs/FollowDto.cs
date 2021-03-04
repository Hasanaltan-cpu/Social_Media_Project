using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
   public class FollowDto
    {

        public int FollowerId { get; set; }

        public int FollowingId { get; set; }
        public bool isExist { get; set; }


    }
}
