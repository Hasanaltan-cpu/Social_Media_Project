using SM_ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_ApplicationLayer.Services.Abstract
{
    public interface IMentionService
    {

        Task AddMention(AddMentionDto model);
    }
}
