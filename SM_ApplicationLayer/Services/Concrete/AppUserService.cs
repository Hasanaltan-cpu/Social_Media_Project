using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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

        public Task DeleteUser(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task EditUser(EditProfileDto id)
        {
            throw new NotImplementedException();
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

        public Task<EditProfileDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileSummaryDto> GetByName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> Login(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(RegisterDto model)
        {
            throw new NotImplementedException();
        }

        public Task<List<SearchUserDto>> SearchUser(string keyword, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<int> UserIdFromName(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<List<FollowListVm>> UsersFollowers(int id, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<List<FollowListVm>> UsersFollowings(int id, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
