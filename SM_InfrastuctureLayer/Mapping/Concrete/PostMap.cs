using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
   public  class PostMap : BaseMap<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.ImagePath).HasColumnType("varchar(200)").IsRequired(false);


            builder.HasMany(x => x.Mentions).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
            builder.HasMany(x => x.Likes).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
            builder.HasMany(x => x.Shares).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
            
            base.Configure(builder);
        }
    }
}
