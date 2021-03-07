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
    public class MentionService : IMentionService
    {

        private IMapper _mapper { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        public MentionService(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task AddMention(AddMentionDto model)
        {
            var mention =_mapper.Map<AddMentionDto,Mention>(model);
            await _unitOfWork.Mention.Add(mention);
            await _unitOfWork.Commit();

        }
    }
}
