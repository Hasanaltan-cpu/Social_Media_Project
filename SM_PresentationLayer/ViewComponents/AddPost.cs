using Microsoft.AspNetCore.Mvc;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM_PresentationLayer.ViewComponents
{
    public class AddPost : ViewComponent

    {

        public IViewComponentResult Invoke()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);
            var post = new SendPostDto();
            
            
            return View(post);
        }
    }
}
