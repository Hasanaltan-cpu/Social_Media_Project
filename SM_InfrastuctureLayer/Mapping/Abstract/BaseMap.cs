using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM_DomainLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_InfrastuctureLayer.Mapping.Abstract
{
    public abstract class BaseMap<T>: IEntityTypeConfiguration<T> where T: class,IBaseEntity
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
        }
    }
}
