using Data;
using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Oprio.Models
{
    public partial class TagPerson : HasIntId
    {
        public TagPerson()
        {
            this.TagPersonItems = new List<TagPersonItem>();
            CreationTimestamp = DateTime.Now;
        }

        public int TagID { get; set; }
        public int PersonID { get; set; }
        public System.DateTime CreationTimestamp { get; set; }
        public int CreatorPersonID { get; set; }
        public Nullable<System.DateTime> LastAccessed { get; set; }
        public int Rank { get; set; }
        public double Importance { get; set; }
        public Nullable<System.DateTime> Pinned { get; set; }
        public Nullable<System.DateTime> Archived { get; set; }
        public Nullable<System.DateTime> Deferred { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person CreatorPerson { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual ICollection<TagPersonItem> TagPersonItems { get; set; }

        //[NotMapped]
        //public int MessageCount {get { return TagPersonItems.Where(x => x.Item.ItemTypeID == (int)ItemTypes.Message).Count(); }}
        //[NotMapped]
        //public int NewMessageCount { get { return TagPersonItems.Where(x => x.Item.ItemTypeID == (int)ItemTypes.Message && !x.IsViewed).Count(); } }
    }
}
