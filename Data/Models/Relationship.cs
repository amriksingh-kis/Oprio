using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Relationship : HasIntId
    {
        public int RelatingPersonID { get; set; }
        public int RelatedPersonID { get; set; }
        public int RelationshipTypeID { get; set; }
        public virtual Person RelatingPerson { get; set; }
        public virtual Person RelatedPerson { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
    }
}
