using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class OrganisationDomain : HasIntId
    {
        public int OrgID { get; set; }
        public string Domain { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
