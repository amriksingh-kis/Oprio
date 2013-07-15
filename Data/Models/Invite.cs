using Data.Base;
using System;
using System.Collections.Generic;

namespace Oprio.Models
{
    public partial class Invite : HasIntId
    {
        public Invite()
        {
            SendDate = DateTime.Now;
        }
      
        public string Email { get; set; }
        public int InvitedByPersonID { get; set; }
        public int TagID { get; set; }
        
        public System.DateTime SendDate { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
