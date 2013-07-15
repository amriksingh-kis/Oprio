using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class ItemStatusHistory : HasIntId
    {
        public ItemStatusHistory()
        {
            SetAtTimestamp = DateTime.Now;
        }
        public Nullable<int> ItemID { get; set; }
        public int ItemStatusID { get; set; }
        public System.DateTime SetAtTimestamp { get; set; }
        public int SetByPersonID { get; set; }
        public string Value { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemStatus ItemStatus { get; set; }
        public virtual Person Person { get; set; }
    }
}
