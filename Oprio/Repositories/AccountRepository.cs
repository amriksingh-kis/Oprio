using System;using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
//using JetModel;
using Oprio.Data;
using JetWeb.Models;

namespace JetWeb.Repositories
{
	public class AccountRepository : RepositoryBase
	{
        public AccountRepository(OprioEntities DataContext)
        {
            Dc = DataContext;
        }
		public Account CreateAccount(RegisterVM AccountInformation)
		{
            OprioEntities dc = new OprioEntities();
			Account account = new Account();
			account.ID = Guid.NewGuid();
			account.BillingName = AccountInformation.BillingName;
			account.AddressLine1 = AccountInformation.AddressLine1;
			account.AddressLine2 = AccountInformation.AddressLine2;
			account.City = AccountInformation.City;
			account.State = AccountInformation.State;
			account.ZipCode = AccountInformation.ZipCode;
			account.ContactName = AccountInformation.ContactName;
			account.ContactPhone = AccountInformation.ContactPhone;
			account.ContactEmail = AccountInformation.ContactEmail;
			account.IsActive = true;
			account.IsBusinessAccount = AccountInformation.IsBusinessAccount;
			account.DisplayName = AccountInformation.DisplayName;
			account.TechnicalContactName = AccountInformation.TechnicalContactName;
			account.TechnicalContactPhone = AccountInformation.TechnicalContactPhone;
			account.TechnicalContactEmail = AccountInformation.TechnicalContactEmail;
            SubscriptionType type = Dc.SubscriptionTypes.Where(o => o.ID == AccountInformation.SubscriptionTypeID).FirstOrDefault();
            if (type.PriceUserMonth == 0)
            {
                account.PaymentMethodID = null;
                account.PaymentFrequencyID = null;
                account.PaymentTermsID = null;
            }
            else
            {
                account.PaymentMethodID = AccountInformation.PaymentMethodID;
                account.PaymentFrequencyID = AccountInformation.PaymentFrequencyID;
                account.PaymentTermsID = AccountInformation.PaymentTermsID;
            }
			

			account.StorageUsageMB = AccountInformation.StorageUsageMB;
			account.ChargeSalesTax = false;
			account.SalesTaxRef = AccountInformation.SalesTaxRef;
			return account ;
		}
	}
}