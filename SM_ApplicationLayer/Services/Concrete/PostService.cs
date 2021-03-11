using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Services.Abstract;
using SM_DomainLayer.Entities.Concrete;
using SM_DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Concrete
{
    public class PostService : IPostService
    {

        private readonly IFollowService _followService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public PostService(IFollowService followService,
                            IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IAppUserService appUserService)

        {
            _followService = followService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appUserService = appUserService;

        }
        public async Task AddPost(SendPostDto model)
        {
            if (model.Image !=null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                if (image.Width! >600)
                {
                    image.Mutate(x => x.Resize(600, 0));
                    Guid name = Guid.NewGuid();
                    image.Save("wwwroot/images/posts/" + name + ".jpg");
                    model.ImagePath = ("/images/posts/" + name + ".jpg");
                }
                var post = _mapper.Map<SendPostDto, Post>(model);
                await _unitOfWork.Post.Add(post);
                await _unitOfWork.Commit();

            }

        }

        public async  Task DeletePost(int id, int userId)
        {
            var post = await _unitOfWork.Post.FirstOrDefault(x => x.Id == id);

            if (userId==post.AppUserId)
            {
                _unitOfWork.Post.Delete(post);
                await _unitOfWork.Commit();
            }
         
        }

        public async Task<List<TimelineVm>> GetTimeline(int userId, int pageIndex)
        {
          List<int> followings=await _followService.FollowingList(userId);

            var posts = await _unitOfWork.Post.GetFilteredList(
                selector: x => new TimelineVm
                {
                    Id = x.Id,
                    Text = x.Text,
                    ImagePath = x.ImagePath,
                    AppUserId = x.AppUserId,
                    LikesCount = x.Likes.Count,
                    MentionsCount = x.Mentions.Count,
                    SharesCount = x.Shares.Count,
                    CreateDate = x.CreateDate,
                    UserName = x.AppUser.UserName,
                    UserImage = x.AppUser.ImagePath,
                    Name = x.AppUser.Name,
                    isLiked = x.Likes.Any(h => h.AppUserId == userId)
                },
                orderBy: z => z.OrderByDescending(a => a.CreateDate),
                predicate: a => followings.Contains(a.AppUserId),
                include: a => a.Include(h => h.AppUser).ThenInclude(h => h.Followers).Include(h => h.Likes),
                pageIndex: pageIndex);



           //This is AutoMapper usage.
            //var posts = await _context.Posts
            //   .Include(x => x.AppUser).ThenInclude(x => x.Followers)
            //   .Include(x => x.Likes)
            //   .ProjectTo<TimelineVm>(_mapper.ConfigurationProvider, new { userId })
            //   .Where(x => followings.Contains(x.AppUserId))
            //   .OrderByDescending(x => x.CreateDate).ToListAsync();


            return posts;
        }

        public async Task<PostDetailVm> PostDetail(int id, int userId)
        {
            var post = await _unitOfWork.Post.GetFilteredFirstorDefault(
                selector: x => new PostDetailVm
                {
                    Id = x.Id,
                    Text = x.Text,
                    ImagePath = x.ImagePath,
                    AppUserId = x.AppUserId,
                    LikesCount = x.Likes.Count,
                    MentionsCount = x.Mentions.Count,
                    SharesCount = x.Shares.Count,
                    CreateDate = x.CreateDate,
                    UserName = x.AppUser.UserName,
                    UserImage = x.AppUser.ImagePath,
                    Name = x.AppUser.Name,
                    Mentions = x.Mentions.Where(a => a.PostId == x.Id).OrderByDescending(h => h.CreateDate).Select(k => new MentionDto
                    {
                        Id = k.Id,
                        Text = k.Text,
                        AppUserId = k.AppUserId,
                        UserName = k.AppUser.UserName,
                        Name = k.AppUser.Name,
                        PostId = k.PostId,
                        CreateDate = k.CreateDate,
                        UserImage = k.AppUser.ImagePath

                    }).ToList(),
                    isLiked = x.Likes.Any(a => a.AppUserId == userId)
                },
                orderBy: h => h.OrderByDescending(k => k.CreateDate),
                predicate: k => k.Id == id,
                include: k => k.Include(h => h.AppUser).ThenInclude(h => h.Followers).Include(h => h.Likes));


            //var post = await _context.Posts.Where(x => x.Id == id)
            //.Include(x => x.Likes)
            //.Include(x => x.AppUser)
            //.Include(x => x.Mentions).ProjectTo<PostDetailVm>(_mapper.ConfigurationProvider, new { userId }).FirstOrDefaultAsync();
            return post;

               
        }

        public async Task<List<TimelineVm>> UsersPosts(string userName, int id, int pageIndex)
        {
            var userId= await _appUserService.UserIdFromName(userName);
            var posts = await _unitOfWork.Post.GetFilteredList(
                selector: x => new TimelineVm
                {
                    Id = x.Id,
                    Text = x.Text,
                    ImagePath = x.ImagePath,
                    AppUserId = x.AppUserId,
                    LikesCount = x.Likes.Count,
                    MentionsCount = x.Mentions.Count,
                    SharesCount = x.Shares.Count,
                    CreateDate = x.CreateDate,
                    UserName = x.AppUser.UserName,
                    UserImage = x.AppUser.ImagePath,
                    Name = x.AppUser.Name,
                    isLiked = x.Likes.Any(h => h.AppUserId == id)
                },
                orderBy: h => h.OrderByDescending(a => a.CreateDate),
                predicate: a => a.AppUserId == userId,
                include: a => a.Include(h => h.AppUser).ThenInclude(h => h.Followers).Include(h => h.Likes),
                pageIndex: pageIndex
                );

            //In this part u can AutoMapper but i did not use because of generic repository pattern.

            //var posts = await _context.Posts.Include(x => x.AppUser).Include(x => x.Likes).Where(x => x.AppUserId == userId).ProjectTo<UsersPostsVm>(_mapper.ConfigurationProvider, new { userId }).OrderByDescending(x => x.CreateDate).toListAsync();
            return posts;
        }
    }
}
