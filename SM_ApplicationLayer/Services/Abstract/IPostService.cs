using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Abstract
{
    public interface IPostService { 

     Task<List<TimelineVm>> GetTimeline(int userId, int pageIndex);
    Task AddPost(SendPostDto model);
    Task<PostDetailVm> PostDetail(int id, int userId);
    Task<List<TimelineVm>> UsersPosts(string userName, int id, int pageIndex);
    Task DeletePost (int id, int userId);
}
}
