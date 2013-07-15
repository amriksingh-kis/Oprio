using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class PersonSession
    {
        public int ConnectionID { get; set; }
        public int PersonID { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public System.DateTime Connect33 { get; set; }
        public Nullable<System.DateTime> DisconnectTimestamp { get; set; }
        public virtual Person Person { get; set; }
    }
}
