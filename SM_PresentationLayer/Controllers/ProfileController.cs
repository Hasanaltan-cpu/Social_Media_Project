using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM_ApplicationLayer.Services.Abstract;

namespace SM_PresentationLayer.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        private readonly IAppUserService _userService;
        public ProfileController(IAppUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }

        public IActionResult Followings(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }

        public IActionResult Followers(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Followers(string userName,int pageIndex)
        {
            var findUser = await _userService.UserIdFromName(userName);
            if (findUser>0)
            {
                var followers = await _userService.UsersFollowers(findUser, pageIndex);
                return Json(followers, new JsonSerializerSettings());

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Followings(string userName,int pageIndex)
        {
            var findUser = await _userService.UserIdFromName(userName);
            if (findUser>0)
            {
                var followings = await _userService.UsersFollowings(findUser, pageIndex);

                return Json(followings, new JsonSerializerSettings());

            }
            else
            {
                return NotFound();
            }

        }
    }
}
