using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Organisation : HasIntId
    {
        public Organisation()
        {
            this.OrganisationDomains = new List<OrganisationDomain>();
            this.OrgPrefs = new List<OrgPref>();
            this.People = new List<Person>();
        }

        public string Name { get; set; }
        public System.DateTime SignupDate { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Handle { get; set; }
        public Nullable<int> AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<OrganisationDomain> OrganisationDomains { get; set; }
        public virtual ICollection<OrgPref> OrgPrefs { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
