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
    
    public partial class OrganisationDomain
    {
        public System.Guid OrgID { get; set; }
        public string Domain { get; set; }
int ID {get; set; }
    
        public virtual Organisation Organisation { get; set; }
    }
}
