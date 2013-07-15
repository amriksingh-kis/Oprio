using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class File : HasIntId
    {
        public int RevisionNumber { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
    }
}
