using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JetWeb.Models
{
    public class JsonMessage
    {
        public bool Success { get; set; }
        public String Message { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public string RedirectUrl { get; set; }
        public string Name { get; set; }
    }
}