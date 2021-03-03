using Microsoft.EntityFrameworkCore;
using SM_DomainLayer.Repositories;
using SM_DomainLayer.UnitOfWork;
using SM_InfrastuctureLayer.Context;
using SM_InfrastuctureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM_InfrastuctureLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            throw new ArgumentNullException("Database can not be null");

        }

        private IPostRepository _postRepository;
        public IPostRepository Post { get { return _postRepository ?? (_postRepository = new PostRepository(_db)); } }


        private IMentionRepository _mentionRepository;

        public IMentionRepository Mention {  get { return _mentionRepository ?? (_mentionRepository = new MentionRepository(_db)); } }

        private IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUser { get { return _appUserRepository ?? (_appUserRepository = new AppUserRepository(_db)); } }

        private IFollowRepository _followRepository;
        public IFollowRepository Follow { get { return _followRepository ?? (_followRepository = new FollowRepository(_db)); } }

        private ILikeRepository _likeRepository;
        public ILikeRepository Like { get { return _likeRepository ?? (_likeRepository = new LikeRepository(_db)); } }

        private IShareRepository _shareRepository;
        public IShareRepository Share { get { return _shareRepository ?? (_shareRepository = new ShareRepository(_db)); } }




        public async  Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        private bool isDisposed = false;
        public async  ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            };
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }
        public async  Task ExecuteSqlRaw(string sql, params object[] parameters)
        {
            await _db.Database.ExecuteSqlRawAsync(sql,parameters);
        }
    }
}
