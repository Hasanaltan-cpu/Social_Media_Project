using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.Models.DTOs
{
    public class ProfileSummaryDto
    {

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingsCount { get; set; }

        public int PostsCount { get; set; }

    }
}
