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
    
    public partial class SubscriptionType
    {
        public SubscriptionType()
        {
            this.Subscriptions = new HashSet<Subscription>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal PriceUserMonth { get; set; }
        public int MinUsers { get; set; }
        public int MaxUsers { get; set; }
        public int MinTermMonths { get; set; }
        public bool IsRolling { get; set; }
        public int BaseStorageMB { get; set; }
        public int AdditinalStorageUserMB { get; set; }
        public int MaxConversationItems { get; set; }
        public string EnabledFeatures { get; set; }
        public int CancellationCutoffDays { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsBusiness { get; set; }
    
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
