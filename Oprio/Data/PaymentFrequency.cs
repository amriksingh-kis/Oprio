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
    
    public partial class PaymentFrequency
    {
        public PaymentFrequency()
        {
            this.Accounts = new HashSet<Account>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int PeriodMonths { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
