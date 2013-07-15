using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Payment : HasIntId
    {
        public Payment()
        {
            this.Invoices = new List<Invoice>();
            PaymentDate = DateTime.Now;
        }


        public System.DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionRef { get; set; }
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
