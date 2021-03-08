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
    public class MentionController : Controller
    {
        private IMentionService _mentionService;

        public MentionController(IMentionService mentionService)
        {
            _mentionService = mentionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMention(AddMentionDto model)
        {
            model.AppUserId = User.GetUserId();
            await _mentionService.AddMention(model);
            return Json("Success");
        }

    }
}
