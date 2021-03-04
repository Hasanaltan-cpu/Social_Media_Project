using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Abstract
{
    public interface IAppUserService
    {
        Task DeleteUser(params object[] parameters);

        Task<IdentityResult> Register(RegisterDto model);

        Task<SignInResult> Login(LoginDto model);

        Task LogOut();

        Task<int> UserIdFromName(string UserName);

        AuthenticationProperties ExternalLogin(string provider, string redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<SignInResult> ExternalLoginSignIn(string provider, string key);

        Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDto model);

        Task<EditProfileDto> GetById(int id);

        Task EditUser(EditProfileDto id);

        Task<ProfileSummaryDto> GetByName(string userName);

        Task<List<SearchUserDto>> SearchUser(string keyword,int pageIndex);

        Task<List<FollowListVm>> UsersFollowings(int id, int pageIndex);

        Task<List<FollowListVm>> UsersFollowers(int id, int pageIndex);


    }
}
