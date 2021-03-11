using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SM_ApplicationLayer.Extensions;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Services.Abstract;

namespace SM_PresentationLayer.Controllers
{
    public class FollowController : Controller
    {
        private IFollowService _followService;
        public FollowController(IFollowService followService) => this._followService = followService;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Follow(FollowDto model)
        {
            if (!model.isExist)
            {
                if (model.FollowerId == User.GetUserId())
                {
                    await _followService.Follow(model);
                    return Json("Success");

                }
                else
                {
                    return Json("Failed");
                }
            }
            else
            {
                if (model.FollowerId == User.GetUserId())
                {
                    await _followService.Unfollow(model);
                    return Json("Success");


                }
                else return Json("Failed");

            }
        }
    }
}
