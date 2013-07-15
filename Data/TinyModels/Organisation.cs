using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.TinyModels
{
    public class Organisation : HasIntId
    {
        public String Name { get; set; }
    }
}
