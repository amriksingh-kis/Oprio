//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Oprio.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trackable
    {
        public System.Guid ItemID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public System.Guid AssigneePersonID { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
