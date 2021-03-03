using SM_DomainLayer.Entities.Concrete;
using SM_DomainLayer.Repositories;
using SM_InfrastuctureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Repositories
{
    public class ShareRepository:BaseRepository<Share>,IShareRepository
    {
        public ShareRepository(ApplicationDbContext context) : base (context)
        {

        }
    }
}
