using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Context
{
    public class ApplicationDbContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Mention> Mentions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageMap());
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new MentionMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new ShareMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new FollowMap());
            base.OnModelCreating(builder);
        }

    }
}
