using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
   public  class ShareMap:BaseMap<Share>
    {
        public override void Configure(EntityTypeBuilder<Share> builder)
        {

            builder.HasKey(x => new { x.PostId, x.AppUserId });
            base.Configure(builder);
        }
    }
}
