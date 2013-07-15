using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class PersonPref : HasIntId
    {
        public int PersonID { get; set; }
        public string PrefKey { get; set; }
        public string PrefValue { get; set; }
        public string Description { get; set; }
        public virtual Person Person { get; set; }
    }
}
