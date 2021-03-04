using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
   public  class MentionDto
    {

        public int Id { get; set; }

        public string Text { get; set; }

        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }

        public DateTime dateTime { get; set; }

        public string UserImage { get; set; }
    }
}
