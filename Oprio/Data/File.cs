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
    
    public partial class File
    {
int ID {get; set; }
        public int RevisionNumber { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public System.Guid ItemID { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
