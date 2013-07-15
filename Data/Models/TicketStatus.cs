using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class TicketStatus : HasIntId
    {
        public TicketStatus()
        {
            this.Tickets = new List<Ticket>();
        }

        public string StatusName { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
