using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
    public class FollowMap:BaseMap<Follow>
    {
        public override void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(x => new { x.FollowerId, x.FollowingId });

            base.Configure(builder);
        }
    }
}
