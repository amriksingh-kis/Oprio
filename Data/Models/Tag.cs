using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Tag : HasIntId
    {
        public Tag()
        {
            this.Invites = new List<Invite>();
            this.TagPersons = new List<TagPerson>();

            CreationTimestamp = DateTime.Now;
        }

        public string Name { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public System.DateTime CreationTimestamp { get; set; }
        public int CreatorPersonID { get; set; }
        public bool IsConversation { get; set; }
        public bool IsSystem { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<TagPerson> TagPersons { get; set; }
        //public bool IsPinned { get; set; }
        //public bool IsArchived { get; set; }
    }
}
