using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Ticket : HasIntId
    {
        public Ticket()
        {
            IssueDate = DateTime.Now;
        }
        public int TicketTypeID { get; set; }
        public System.DateTime IssueDate { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public string TicketRef { get; set; }
        public Nullable<int> PersonID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public virtual Person Person { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketType TicketType { get; set; }
    }
}
