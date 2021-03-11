using Microsoft.AspNetCore.Mvc;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM_PresentationLayer.ViewComponents
{
    public class FollowUser:ViewComponent
    {
        private readonly IAppUserService _userService;
        private readonly IFollowService _followService;
        public FollowUser(IAppUserService userService,
                          IFollowService followService)
        {
            _userService = userService;
            _followService = followService;

        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            int userId = await _userService.UserIdFromName(userName);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int followerId = Convert.ToInt32(claim.Value);

            var followDto = new FollowDto { FollowerId = followerId, FollowingId = userId };
            followDto.isExist = await _followService.isFollowing(followDto);

            return View(followDto);
        }
    }
}
