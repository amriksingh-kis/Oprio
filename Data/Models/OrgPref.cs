using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class OrgPref : HasIntId
    {
        public int OrgID { get; set; }
        public string PrefKey { get; set; }
        public string PrefValue { get; set; }
        public string Description { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
