using SM_DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_DomainLayer.UnitOfWork
{
  public  interface IUnitOfWork :IAsyncDisposable
    {
       IPostRepository Post { get; }

       IMentionRepository Mention { get; }

       IAppUserRepository AppUser { get; }

        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }

        Task Commit();

        Task ExecuteSqlRaw(string sql, params object[] parameters);

    }
}
