using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class ItemStatus : HasIntId
    {
        public ItemStatus()
        {
            this.ItemStatusHistories = new List<ItemStatusHistory>();
        }

        public string Name { get; set; }
        public int ItemTypeID { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<ItemStatusHistory> ItemStatusHistories { get; set; }
    }
}
