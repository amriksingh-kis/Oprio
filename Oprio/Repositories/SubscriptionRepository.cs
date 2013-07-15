using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oprio.Data;
using JetWeb.Models;

namespace JetWeb.Repositories
{
	public class SubscriptionRepository:RepositoryBase
	{
        public SubscriptionRepository(OprioEntities DataContext=null)
        {
            Dc = DataContext;
        }
		public Subscription CreateSubscriptionPlan(RegisterVM SubscriptionInfo, Guid LastModifiedByID)
		{
			Subscription subscription = new Subscription();
			subscription.SubscriptionID = Guid.NewGuid();
			//subscription.AccountID = (Guid)SubscriptionInfo.AccountID;

            SubscriptionType type = Dc.SubscriptionTypes.Where(o => o.ID == SubscriptionInfo.SubscriptionTypeID).FirstOrDefault();


            subscription.CreatedByPersonID = LastModifiedByID;
            subscription.SubscriptionTypeID = SubscriptionInfo.SubscriptionTypeID;

            subscription.ModifiedByPersonID = LastModifiedByID;
			subscription.MinUsers = type.MinUsers;
			subscription.MaxUsers = type.MaxUsers;
			subscription.StartDate = DateTime.Now;
			// To be Configured
			subscription.EndDate = DateTime.Now;
			// To be Configured
			subscription.IsTrial = false;
			subscription.PriceUserMonth = type.PriceUserMonth;
			subscription.UserCount = 1;
			subscription.MinTermMonths = type.MinTermMonths;
			// To be Configured
			subscription.IsRolling = false;
			subscription.BaseStorageMB = type.BaseStorageMB;
			subscription.AdditinalStorageUserMB = type.AdditinalStorageUserMB;
			subscription.MaxConversationItems = type.MaxConversationItems;
			subscription.EnabledFeatures = type.EnabledFeatures;
			// To be Configured
			subscription.DateCreated = DateTime.Now;
			subscription.DateModified = DateTime.Now;
			// To be Configured
			subscription.CancellationCutoffDate = DateTime.Now;
			subscription.CancellationCutoffDays = type.CancellationCutoffDays;

            return subscription;
			
			
		}
        public void UpdateSubscriptionPlan(Guid? orgaisationID,int Count)
        {
            Guid? AccountID = (Dc.Organisations.Where(o =>o.ID==orgaisationID).FirstOrDefault()).AccountID;
            Subscription Subscription = Dc.Subscriptions.Where(s => s.AccountID == AccountID).OrderByDescending(s => s.DateModified).FirstOrDefault();
            int RemaingOldCount =Subscription.MinUsers- Subscription.UserCount;
            int RemainingUsers =0;
            Subscription NewSubscription = null;
            if (Count <= RemaingOldCount)
            {
                Subscription.UserCount += Count;
            }
            else
            {
                RemainingUsers = Count - RemaingOldCount;
                Subscription.UserCount = Subscription.MinUsers;
                NewSubscription = new Subscription();
                NewSubscription.SubscriptionID = Guid.NewGuid();
                NewSubscription.AccountID = Subscription.AccountID;
                NewSubscription.AdditinalStorageUserMB = Subscription.BaseStorageMB;
                NewSubscription.CancellationCutoffDate = Subscription.CancellationCutoffDate;
                NewSubscription.CancellationCutoffDays = Subscription.CancellationCutoffDays;
                NewSubscription.CreatedByPersonID = Subscription.CreatedByPersonID;
                NewSubscription.DateCreated = Subscription.DateCreated;
                NewSubscription.DateModified = Subscription.DateModified;
                NewSubscription.EnabledFeatures = Subscription.EnabledFeatures;
                NewSubscription.EndDate = Subscription.EndDate;
                NewSubscription.IsRolling = Subscription.IsRolling;
                NewSubscription.IsTrial = Subscription.IsTrial;
                NewSubscription.MaxConversationItems = Subscription.MaxConversationItems;
                NewSubscription.MaxUsers = Subscription.MaxUsers;
                NewSubscription.MinTermMonths = Subscription.MinTermMonths;
                NewSubscription.MinUsers = RemainingUsers;
                NewSubscription.ModifiedByPersonID = Subscription.ModifiedByPersonID;
                NewSubscription.PriceUserMonth = Subscription.PriceUserMonth;
                NewSubscription.StartDate = Subscription.StartDate;
                NewSubscription.TrialEndDate = Subscription.TrialEndDate;
                NewSubscription.UserCount = RemainingUsers;
                NewSubscription.SubscriptionTypeID = Subscription.SubscriptionTypeID;
            }

            try
            {
                if (NewSubscription != null)
                {
                    Dc.Subscriptions.Add(NewSubscription);
                }
                Dc.SaveChanges();
            }
            catch(Exception e)
            {

            }
            
        }
	}
}