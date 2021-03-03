using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Concrete;
using SM_InfrastuctureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Concrete
{
    public class MentionMap:BaseMap<Mention>
    {
        public override void Configure(EntityTypeBuilder<Mention> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasColumnType("varchar(200)").IsRequired();
            base.Configure(builder);
        }
    }
}
