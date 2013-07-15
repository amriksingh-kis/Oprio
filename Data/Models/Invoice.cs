using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Invoice:HasIntId
    {
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
       
        public int InvoiceNumber { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public Nullable<decimal> NetInvoiceAmount { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public Nullable<decimal> GrossInvoiceAmount { get; set; }
        public System.DateTime InvoideDueDate { get; set; }
        public int AccountID { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
