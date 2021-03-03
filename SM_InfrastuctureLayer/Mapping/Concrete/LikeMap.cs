using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
  public class LikeMap :BaseMap<Like>
    {
        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => new { x.AppUserId, x.PostId });
            base.Configure(builder);
        }
    }
}
