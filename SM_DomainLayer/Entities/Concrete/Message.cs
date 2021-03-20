using SM_DomainLayer.Entities.Abstract;
using SM_DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SM_DomainLayer.Entities.Concrete
{
    public class Message:IBaseEntity
    {
        public int Id { get; set; }


        [Required]
        public string UserName { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime When { get; set; }

        public int UserId { get; set; }

        public virtual AppUser Sender { get; set; }

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status { get; set; }
        public Message()
        {
            When = DateTime.Now;
        }
    }
}
