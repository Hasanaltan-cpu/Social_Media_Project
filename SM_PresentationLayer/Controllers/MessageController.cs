using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Context;

namespace SM_PresentationLayer.Controllers
{
    public class MessageController : Controller
    {
       
        public readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public MessageController(UserManager<AppUser> userManager,
                                 ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            
            var messages = await _context.Messages.ToListAsync();
            return View();
        }


        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Opps something has been wrong..");
        }

    }
}
