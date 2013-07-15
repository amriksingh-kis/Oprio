using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class SubscriptionType : HasIntId
    {
        public SubscriptionType()
        {
            this.Subscriptions = new List<Subscription>();
        }

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
