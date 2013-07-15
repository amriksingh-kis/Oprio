using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Trackable : HasIntId
    {
        public int ItemID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int AssigneePersonID { get; set; }
        public virtual Item Item { get; set; }
    }
}
