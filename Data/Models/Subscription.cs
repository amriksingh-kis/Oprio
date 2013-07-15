using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Subscription : HasIntId
    {
        public Subscription()
        {
            DateCreated = DateTime.Now;
        }
        public int AccountID { get; set; }
        public int SubscriptionTypeID { get; set; }
        public decimal PriceUserMonth { get; set; }
        public int MinUsers { get; set; }
        public int MaxUsers { get; set; }
        public int UserCount { get; set; }
        public int MinTermMonths { get; set; }
        public bool IsRolling { get; set; }
        public int BaseStorageMB { get; set; }
        public int AdditinalStorageUserMB { get; set; }
        public int MaxConversationItems { get; set; }
        public string EnabledFeatures { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedByPersonID { get; set; }
        public System.DateTime DateModified { get; set; }
        public int ModifiedByPersonID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int CancellationCutoffDays { get; set; }
        public System.DateTime CancellationCutoffDate { get; set; }
        public bool IsTrial { get; set; }
        public Nullable<System.DateTime> TrialEndDate { get; set; }
        public virtual Account Account { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
    }
}
