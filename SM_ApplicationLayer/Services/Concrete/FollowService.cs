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
  public class FollowService:IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FollowService(IUnitOfWork unitOfWork,
                             IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Follow(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);
            if (isExistFollow==null)
            {
                var follow = _mapper.Map<FollowDto, Follow>(model);
                await _unitOfWork.Follow.Add(follow);
                await _unitOfWork.Commit();


            };
        }

        public async Task Unfollow(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            _unitOfWork.Follow.Delete(isExistFollow);
            await _unitOfWork.Commit();
        }

        public async Task<bool> isFollowing(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.Any(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            return isExistFollow;
        }

        public async Task<List<int>> FollowingList(int id)
        {
            var followingList = await _unitOfWork.Follow.GetFilteredList(
                 selector: y => y.FollowingId,
                 predicate: x => x.FollowerId == id);

            return followingList;
        }

        public async Task<List<int>> FollowerList(int id)
        {
            var followerList = await _unitOfWork.Follow.GetFilteredList(
                selector: y => y.FollowerId,
                predicate: x => x.FollowingId == id);

            return followerList; 
        }

       
    }
}
