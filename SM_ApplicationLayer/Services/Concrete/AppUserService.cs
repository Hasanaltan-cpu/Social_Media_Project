using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Services.Abstract;
using SM_DomainLayer.Entities.Concrete;
using SM_DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Concrete
{
    public class AppUserService : IAppUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IFollowService _followService;

        public AppUserService(IUnitOfWork unitOfWork,IMapper mapper,UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IFollowService followService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _followService = followService;

        }

        public async Task DeleteUser(params object[] parameters)
        {
            await _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }

        public async Task EditUser(EditProfileDto model)
        {
            var user = await _unitOfWork.AppUser.GetById(model.Id);
            if (user != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    user.ImagePath = ("/images/users/" + user.UserName + ".jpg");
                    _unitOfWork.AppUser.Update(user);
                    await _unitOfWork.Commit();
                }

                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }
                if (model.UserName != user.UserName)
                {
                    var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                    if (isUserNameExist == null)
                    {
                        await _userManager.SetUserNameAsync(user, model.UserName);
                        user.UserName = model.UserName;
                        await _signInManager.SignInAsync(user, isPersistent: true);
                    }
                }
                if (model.Name != user.Name)
                {
                    user.Name = model.Name;
                    _unitOfWork.AppUser.Update(user);
                    await _unitOfWork.Commit();
                }
                if (model.Email != user.Email)
                {
                    var isEmailExist = await _userManager.FindByEmailAsync(model.Email);
                    if (isEmailExist == null)
                        await _userManager.SetEmailAsync(user, model.Email);
                }

            };
        }

        public AuthenticationProperties ExternalLogin(string provider, string redirectUrl)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> ExternalLoginSignIn(string provider, string key)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<EditProfileDto> GetById(int id)
        {
            var user = await _unitOfWork.AppUser.GetById(id);

            return _mapper.Map<EditProfileDto>(user);
        }

        public async Task<ProfileSummaryDto> GetByName(string userName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstorDefault(
                selector: y => new ProfileSummaryDto
                {
                    UserName = y.UserName,
                    Name = y.Name,
                    ImagePath = y.ImagePath,
                    PostsCount = y.Posts.Count,
                    FollowersCount = y.Followers.Count,
                    FollowingsCount = y.Followings.Count
                },
                predicate: x => x.UserName == userName);

            return user; ;
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            throw new NotImplementedException();
        }

        public async Task<SignInResult> Login(LoginDto model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result; ;
        }

        public async Task<List<SearchUserDto>> SearchUser(string keyword, int pageIndex)
        {
            var users = await _unitOfWork.AppUser.GetFilteredList(
                selector: x => new SearchUserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserName = x.UserName,
                    ImagePath = x.ImagePath
                },
                predicate: x => x.UserName.Contains(keyword) || x.Name.Contains(keyword),
                pageIndex: pageIndex,
                pageSize: 10);

            return users; 
        }

        public async Task<int> UserIdFromName(string UserName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstorDefault(
                selector: x => x.Id,
                predicate: x => x.UserName == UserName);

            return user; ;
        }

        public async Task<List<FollowListVm>> UsersFollowers(int id, int pageIndex)
        {
            List<int> followers = await _followService.FollowerList(id);

            var followersList = await _unitOfWork.AppUser.GetFilteredList(selector: y => new FollowListVm
            {
                Id = y.Id,
                ImagePath = y.ImagePath,
                UserName = y.UserName,
                Name = y.Name,
            },
                predicate: x => followers.Contains(x.Id),
                include: x => x
               .Include(z => z.Followers),
                pageIndex: pageIndex);
            return followersList; ;
        }

        public async Task<List<FollowListVm>> UsersFollowings(int id, int pageIndex)
        {
            List<int> followings = await _followService.FollowingList(id);

            var followingsList = await _unitOfWork.AppUser.GetFilteredList(selector: y => new FollowListVm
            {
                Id = y.Id,
                ImagePath = y.ImagePath,
                UserName = y.UserName,
                Name = y.Name,
            },
                predicate: x => followings.Contains(x.Id),
                include: x => x
               .Include(z => z.Followers),
                pageIndex: pageIndex);
            return followingsList; ;
        }
    }
}
