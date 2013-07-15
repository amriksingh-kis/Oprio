using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class TicketType : HasIntId
    {
        public TicketType()
        {
            this.Tickets = new List<Ticket>();
        }

        public string TicketTypeName { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
