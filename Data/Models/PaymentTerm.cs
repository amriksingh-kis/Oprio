using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class PaymentTerm : HasIntId
    {
        public PaymentTerm()
        {
            this.Accounts = new List<Account>();
        }

        public int PeriodDays { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
