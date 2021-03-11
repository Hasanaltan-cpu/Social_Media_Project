using Microsoft.AspNetCore.Mvc;
using SM_ApplicationLayer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM_PresentationLayer.ViewComponents
{
    public class ProfileSummary:ViewComponent
    {
        private readonly IAppUserService _userService;
        public ProfileSummary(IAppUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var user = await _userService.GetByName(userName);

            return View(user);
        }
    }
}
