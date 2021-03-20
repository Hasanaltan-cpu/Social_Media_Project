using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
    public class MessageMap : BaseMap<Message>
    {

        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne<AppUser>(a => a.Sender).WithMany(h => h.Messages).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
       
    }
}
