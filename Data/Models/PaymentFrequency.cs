using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class PaymentFrequency : HasIntId
    {
        public PaymentFrequency()
        {
            this.Accounts = new List<Account>();
        }

        public string Name { get; set; }
        public int PeriodMonths { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
