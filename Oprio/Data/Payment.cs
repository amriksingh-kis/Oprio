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
    
    public partial class Payment
    {
        public Payment()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
int ID {get; set; }
        public System.DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionRef { get; set; }
        public System.Guid AccountID { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
