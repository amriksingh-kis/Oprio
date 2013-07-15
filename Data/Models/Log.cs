using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Log : HasIntId
    {
        public System.DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
    }
}
