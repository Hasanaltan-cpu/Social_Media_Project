using SM_DomainLayer.Entities.Abstract;
using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SM_DomainLayer.Entities.Concrete
{
    public class Follow : IBaseEntity

    {
        public int FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        [InverseProperty("Followers")]

        public AppUser Follower { get; set; }

        public int FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        [InverseProperty("Following")]

        public AppUser Following { get; set; }

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status { get; set; }
    }
}
