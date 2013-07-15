using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Item:HasIntId
    {
        public Item()
        {
            this.Files = new List<File>();
            this.ItemStatusHistories = new List<ItemStatusHistory>();
            this.ChildItems = new List<Item>();
            this.TagPersonItems = new List<TagPersonItem>();

            CreationTimestamp = DateTime.Now;
        }

       
        public string ItemText { get; set; }
        public Nullable<int> ParentItemID { get; set; }
        public byte ItemLevel { get; set; }
        public int ItemTypeID { get; set; }
        public System.DateTime CreationTimestamp { get; set; }
        public System.DateTime ModifiedTimestamp { get; set; }
        public Nullable<int> CreatorPersonID { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<ItemStatusHistory> ItemStatusHistories { get; set; }
        public virtual ICollection<Item> ChildItems { get; set; }
        public virtual Item ParentItem { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<TagPersonItem> TagPersonItems { get; set; }
        public virtual Trackable Trackable { get; set; }
    }
}
