using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_DomainLayer.Entities.Abstract
{
    public interface IBaseEntity
    {
        DateTime CreateDate { get; }

        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }

        Status Status { get; set; }
    }
}
