using AutoMapper;
using SM_ApplicationLayer.Models.DTOs;
using SM_DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_ApplicationLayer.AutoMapper
{
   public  class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, ExternalLoginDto>().ReverseMap();
            CreateMap<AppUser, EditProfileDto>().ReverseMap();
            CreateMap<AppUser, ProfileSummaryDto>().ReverseMap();
            CreateMap<Follow, FollowDto>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<Post, SendPostDto>().ReverseMap();

            CreateMap<Mention, MentionDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
                .ReverseMap();

            CreateMap<Mention, AddMentionDto>().ReverseMap();
        }
        


    }
}
