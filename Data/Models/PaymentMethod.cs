using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class PaymentMethod : HasIntId
    {
        public PaymentMethod()
        {
            this.Accounts = new List<Account>();
        }

        public string Name { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
