using Microsoft.AspNetCore.Identity;
using SM_DomainLayer.Entities.Abstract;
using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_DomainLayer.Entities.Concrete
{
    public class AppRole:IdentityRole<int>,IBaseEntity
    {

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status{ get; set; }

    }
}
