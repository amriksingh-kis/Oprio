using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class RelationshipType : HasIntId
    {
        public RelationshipType()
        {
            this.Relationships = new List<Relationship>();
        }

        public string Name { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
    }
}
