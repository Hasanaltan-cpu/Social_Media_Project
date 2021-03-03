using SM_DomainLayer.Entities.Concrete;
using SM_DomainLayer.Repositories;
using SM_InfrastuctureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Repositories
{
    public class PostRepository:BaseRepository<Post>,IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
