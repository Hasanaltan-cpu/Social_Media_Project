using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Abstract
{
   public  interface IFollowService
    {

        Task Follow(FollowDto model);

        Task Unfollow(FollowDto model);
        Task<bool> isFollowing(FollowDto model);
        Task<List<int>> FollowingList(int id);
        Task<List<int>> FollowerList(int id);
    }
}
