using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM_ApplicationLayer.Extensions;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Services.Abstract;

namespace SM_PresentationLayer.Controllers
{

    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddPost(SendPostDto model)
         {
            if (ModelState.IsValid) 
            {
                if (model.AppUserId==User.GetUserId())
                {
                    await _postService.AddPost(model);
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");

                }
            }
            else
            {
                return BadRequest(String.Join(Environment.NewLine, ModelState.Values.SelectMany(h => h.Errors).Select(h => h.ErrorMessage + "" + h.Exception)));
            }
        }
        
        public async Task<IActionResult> PostDetail(int id)
        {
            var post = await _postService.PostDetail(id, User.GetUserId());
            return View(post);
        }

        [HttpPost]

        public async Task<IActionResult> GetPosts(int pageIndex,int pageSize,string userName=null)
        {
            if (userName==null)
            {
                var posts = await _postService.GetTimeline(User.GetUserId(), pageIndex);
                return Json(posts, new JsonSerializerSettings());
            }
            else
            {
                var posts = await _postService.UsersPosts(userName, User.GetUserId(), pageIndex);
                return Json(posts, new JsonSerializerSettings());
            }
        }

        [HttpPost]

        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id, User.GetUserId());

            return Json("");

        }
    }
}
