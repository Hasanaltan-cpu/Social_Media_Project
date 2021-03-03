using SM_DomainLayer.Entities.Abstract;
using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM_DomainLayer.Entities.Concrete
{
    public class Like : IBaseEntity
    {
        public int PostId { get; set; }

        public Post Post { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status { get; set; }
    }
}
