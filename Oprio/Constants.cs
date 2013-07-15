using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JetWeb
{
	public class Constants
	{
		public enum TicketStatus
		{
			Pending = 1,
			Rejected =2,
			Approved =3
		}
        public enum TicketType
        {
            PasswordReset = 1,
            ApprovalRequest
        }
	}
}