using AutoMapper;
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
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork unitOfWork,
                                IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;

        }
        public async  Task Like(LikeDto model)
        {
            var isLiked= await _unitOfWork.Like.FirstOrDefault(x=>x.AppUserId==model.AppUserId&&x.PostId==model.PostId);
            if (isLiked==null)
            {
                var like = _mapper.Map<LikeDto, Like>(model);
                await _unitOfWork.Like.Add(like);
                await _unitOfWork.Commit();
            }
            
        }

        public  async Task Unlike(LikeDto model)
        {
            var isLiked = await _unitOfWork.Like.FirstOrDefault(x => x.AppUserId == model.AppUserId && x.PostId == model.PostId);
            _unitOfWork.Like.Delete(isLiked);
            await _unitOfWork.Commit(); ;
        }
    }
}
