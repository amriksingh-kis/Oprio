using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oprio.Models;
using System.Web.Security;

namespace JetWeb.Repositories
{
	public class TicketRepository:RepositoryBase
	{
        public TicketRepository(Oprio.Models.JetContext DataContext = null)
        {
            Dc = DataContext;
        }
		public Ticket CreateTicket(Constants.TicketType type, Constants.TicketStatus status, DateTime expires, string reference, int? personId, bool persist)
		{
			Ticket ticket = new Ticket();
			ticket.IssueDate = DateTime.Now;
            ticket.StatusID = (int?)status;
			ticket.TicketTypeID = (int)type;
			ticket.ExpireDate = expires;
			ticket.TicketRef = reference;
            ticket.PersonID = personId;

            if (persist)
            {
                Dc.Tickets.Add(ticket);
                Dc.SaveChanges();
            }
            return ticket;
		}

        public Ticket GetByID(int id)
        {
            return Dc.Tickets.FirstOrDefault(x => x.ID == id);
        }
        public void Delete(Ticket t)
        {
            Dc.Tickets.Remove(t);
            Dc.SaveChanges();
        }

        public List<Ticket> GetUserTickets(int index, int PageSize, out int TotalCount)
        {
            index = index - 1;
            Guid? OrganisationID = GetOrganisationID(GetCurrentUserID());
            List<Ticket> Tickets = Dc.Tickets.Where(x => x.Person.OrganisationID == OrganisationID && x.StatusID == (int)Constants.TicketStatus.Pending).Skip(index * PageSize).Take(PageSize).OrderByDescending(x => x.IssueDate).ToList();
            TotalCount = Dc.Tickets.Where(x => x.Person.OrganisationID == OrganisationID && x.StatusID == (int)Constants.TicketStatus.Pending).Count();
            return Tickets;
        }
        private int GetAvailableSlots(Guid? OrgID)
        {
            int CurrentUsers = 0;
            int MaxAllowedUsers = 0;
            Organisation Organisation = Dc.Organisations.Where(o => o.ID == OrgID).FirstOrDefault();
            if (Organisation != null)
            {
                CurrentUsers = Dc.Subscriptions.Where(x => x.AccountID == Organisation.AccountID).Sum(s => s.UserCount);
                MaxAllowedUsers = Dc.Subscriptions.Where(x => x.AccountID == Organisation.AccountID).Sum(s => s.MinUsers);
            }
            else
            {
                throw new ApplicationException("Bad Organisation");
            }

            return (MaxAllowedUsers - CurrentUsers);
        }

        public object UpdateRequestStatus(string[] Guids, int Status, bool ApprovedPayment, string[] Emails)
        {
            string[] GuidsArray = Guids;
            int AvailableSlots = 0;
            bool ApproveTransaction = true;
            Guid? OrganisationID = GetOrganisationID(GetCurrentUserID());
            if (Status == (int)Constants.TicketStatus.Approved)
            {
                AvailableSlots = GetAvailableSlots(OrganisationID);
                if (AvailableSlots >= GuidsArray.Length)
                {
                    ApproveTransaction = true;
                }
                else
                {
                    ApproveTransaction = false;
                }

            }

            if (Status == (int)Constants.TicketStatus.Rejected)
            {
                OrganisationRepository OrganisationRepository = new OrganisationRepository(Dc);
                string OrganisationName=  OrganisationRepository.GetOrganisationbyFilter(o => o.OrgID == OrganisationID).First().Name;

                for (int i = 0; i < Emails.Length; i++)
                {
                    JetWeb.Email emailUtil = new Email(Emails[i], EmailTemplates.Rejection, new { Organisation = OrganisationName });
                    emailUtil.Send();
                }
            }

            if (ApproveTransaction || ApprovedPayment)
            {
                SubscriptionRepository subscriptionRepository = new SubscriptionRepository(Dc);
                subscriptionRepository.UpdateSubscriptionPlan(GetOrganisationID(GetCurrentUserID()), GuidsArray.Length);
                for (int i = 0; i < GuidsArray.Length; i++)
                {
                    Ticket ticket = Dc.Tickets.Where(x => x.ID == new Guid(GuidsArray[i])).FirstOrDefault();
                    ticket.Person.IsApproved = (Status == (int)Constants.TicketStatus.Approved ? true : false);
                    ticket.StatusID = Status;
                }
            }


            var Result = new
            {
                Success = ApproveTransaction || ApprovedPayment,
                RequiredExtraSlots = (GuidsArray.Length - AvailableSlots),
                UsersCount = GuidsArray.Length
            };

            Dc.SaveChanges();
            return Result;
        }

	}
}