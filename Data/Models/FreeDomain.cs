using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class FreeDomain : HasIntId
    {
        public string Domain { get; set; }
    }
}
