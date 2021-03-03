using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
   public class AppUserMap:BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasColumnType("nvarchar(50)");
            builder.Property(x => x.NormalizedUserName).HasColumnType("nvarchar(50)");
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired(false);
            builder.Property(x => x.ImagePath).HasColumnType("varchar(100)").IsRequired(false);

            builder.HasMany(x => x.Posts).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Likes).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Shares).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Mentions).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Followers).WithOne(x => x.Follower).HasForeignKey(x => x.Follower).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Followings).WithOne(x => x.Following).HasForeignKey(x => x.FollowingId).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
