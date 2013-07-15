using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class TagPersonItem 
    {
        public TagPersonItem()
        {
            CreationTimeStamp = DateTime.Now;
        }
        public int TagPersonID { get; set; }
        public int ItemID { get; set; }
        public bool IsViewed { get; set; }
        public Nullable<System.DateTime> ViewedTimeStamp { get; set; }
        public int CreatorPersonID { get; set; }
        public System.DateTime CreationTimeStamp { get; set; }
        public int Rank { get; set; }
        public double Importance { get; set; }
        public virtual Item Item { get; set; }
        public virtual Person Person { get; set; }
        public virtual TagPerson TagPerson { get; set; }
    }
}
