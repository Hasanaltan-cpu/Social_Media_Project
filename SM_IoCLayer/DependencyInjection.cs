using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SM_ApplicationLayer.AutoMapper;
using SM_ApplicationLayer.Services.Abstract;
using SM_ApplicationLayer.Services.Concrete;
using SM_DomainLayer.Entities.Concrete;
using SM_DomainLayer.UnitOfWork;
using SM_InfrastuctureLayer.Context;
using SM_InfrastuctureLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_IoCLayer
{
   public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IMentionService, MentionService>();


            services.AddIdentity<AppUser, AppRole>(x =>
             {
                 x.SignIn.RequireConfirmedPhoneNumber = false;
                 x.SignIn.RequireConfirmedAccount = false;
                 x.SignIn.RequireConfirmedEmail = false;
                 x.User.RequireUniqueEmail = true;
                 x.Password.RequiredLength = 1;
                 x.Password.RequiredUniqueChars = 0;
                 x.Password.RequireUppercase = false;
                 x.Password.RequireNonAlphanumeric = false;
                 x.Password.RequireLowercase = false;
             }).AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

    }
}
