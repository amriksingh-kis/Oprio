using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Account:HasIntId
    {
        public Account()
        {
            this.Invoices = new List<Invoice>();
            this.Organisations = new List<Organisation>();
            this.Payments = new List<Payment>();
            this.People = new List<Person>();
            this.Subscriptions = new List<Subscription>();

            CreateTimestamp = DateTime.Now;
        }

         
        public DateTime CreateTimestamp { get; set; }
        public string BillingName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public bool IsBusinessAccount { get; set; }
        public string DisplayName { get; set; }
        public string TechnicalContactName { get; set; }
        public string TechnicalContactPhone { get; set; }
        public string TechnicalContactEmail { get; set; }
        public Nullable<int> PaymentMethodID { get; set; }
        public Nullable<int> PaymentFrequencyID { get; set; }
        public Nullable<int> PaymentTermsID { get; set; }
        public int StorageUsageMB { get; set; }
        public bool ChargeSalesTax { get; set; }
        public string SalesTaxRef { get; set; }
        public virtual PaymentFrequency PaymentFrequency { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual PaymentTerm PaymentTerm { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
