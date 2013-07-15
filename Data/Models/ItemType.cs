using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class ItemType : HasIntId
    {
        public ItemType()
        {
            this.Items = new List<Item>();
            this.ItemStatus = new List<ItemStatus>();
        }

        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemStatus> ItemStatus { get; set; }
    }
}
