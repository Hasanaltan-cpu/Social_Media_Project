using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM_ApplicationLayer.Services.Abstract;

namespace SM_PresentationLayer.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAppUserService _userService;

        public SearchController(IAppUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(string userName)
        {
            ViewBag.SearchKeyword = userName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userName,int pageIndex)
        {
            if (!String.IsNullOrEmpty(userName))
            {
                var users = await _userService.SearchUser(userName, pageIndex);
                return Json(users, new JsonSerializerSettings());

            }
            else
            {
                return NotFound();
            }
        }
    }
}
