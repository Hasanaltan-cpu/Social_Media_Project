using Microsoft.AspNetCore.Identity;
using SM_DomainLayer.Entities.Abstract;
using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SM_DomainLayer.Entities.Concrete
{
    public class AppUser :IdentityUser<int>, IBaseEntity
    {
        public AppUser()
        {
            Posts = new List<Post>();
            Shares = new List<Share>();
            Likes = new List<Like>();
            Mentions = new List<Mention>();
            Followers = new List<Follow>();
            Followings = new List<Follow>();
            Messages = new HashSet<Message>();
        }

        public virtual ICollection<Message> Messages { get; set; }
        public string Name { get; set; }

        public string ImagePath { get; set; } = "/images/users/default.jpg";

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status { get; set; }

        public List<Post> Posts { get; set; }

        public List<Share> Shares { get; set; }

        public List<Like> Likes { get; set; }
        public List<Mention> Mentions { get; set; }

        [InverseProperty("Follower")]
        public List<Follow> Followers { get; set; }

        [InverseProperty("Following")]

        public List<Follow> Followings { get; set; }




    }
}
