using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JetWeb.Models;

using System.Web.Security;
using System.Security.Principal;
using Oprio.Models;
using WebMatrix.WebData;


namespace JetWeb.Repositories
{
    public class UserRepository : RepositoryBase
    {

        public UserRepository(JetContext DataContext=null)
        {
            Dc = DataContext;
        }

        public JsonMessage RegisterBusinessAccount(RegisterVM personalInformation, RegisterVM accountInformation)
        {
            JsonMessage Message = new JsonMessage();
            //MembershipUser user = null;
            //Person p = new Person();
            //p.FirstName = personalInformation.FirstName;
            //p.LastName = personalInformation.LastName;
            try
            {
                WebSecurity.CreateUserAndAccount(personalInformation.UserName, personalInformation.Password, new { Email = personalInformation.Email });
                Person p = Dc.People.FirstOrDefault(x => x.Email.Equals(personalInformation.Email, StringComparison.InvariantCultureIgnoreCase));
                p.FirstName = personalInformation.FirstName;
                p.LastName = personalInformation.LastName;
                p.IsApproved = true;
                Dc.People.Add(p);
                Dc.SaveChanges();

                //CHANGE Email to UserName
                Roles.AddUserToRole(p.Email, "Admin");
            }
            catch(MembershipCreateUserException e)
            {
                Message.Success = false;
                Message.Message = ErrorCodeToString(e.StatusCode);
                return Message;
            }
            
            //AccountRepository AccountRepository = new AccountRepository(Dc);
            //p.Account = AccountRepository.CreateAccount(accountInformation);
            //SubscriptionRepository subscriptionRepository = new SubscriptionRepository(Dc);
            //p.Account.Subscriptions.Add(subscriptionRepository.CreateSubscriptionPlan(personalInformation,p.ID));
            //OrganisationRepository OrganisationRepository = new Repositories.OrganisationRepository(Dc);
            //Organisation Organisation = OrganisationRepository.CreateOrganisation(personalInformation);
            //p.Account.Organisations.Add(Organisation);
            //p.Organisation = Organisation;
            //try
            //{
            //    Dc.SaveChanges();
            //    FormsAuthentication.SetAuthCookie(personalInformation.UserName, true /* createPersistentCookie */);
            //    Message.Success = true;
            //    Message.Message = "Account created successfully.";
            //    return Message;
            //}
            //catch (Exception ex)
            //{
            //    System.Web.Security.Membership.DeleteUser(personalInformation.UserName, true);
            //    Message.Success = false;
            //    Message.Message = "An unexpected error has occurred. please try again later.";
            //    return Message;
            //}
            return Message;
        }
        public JsonMessage RegisterOrganizationUser(RegisterVM personalInformation)
		{
            JsonMessage Message = new JsonMessage();
			MembershipUser user = null;
			Person p = new Person();
			p.FirstName = personalInformation.FirstName;
			p.LastName = personalInformation.LastName;
			//p.OrganisationID = personalInformation.OrganizationID;
            p.IsApproved = false;
            //try
            //{
            //    user = System.Web.Security.Membership.CreateUser(personalInformation.UserName, personalInformation.Password, personalInformation.Email);
            //}
            //catch (MembershipCreateUserException e)
            //{
            //    Message.Success = false;
            //    Message.Message = ErrorCodeToString(e.StatusCode);
            //    return Message;   
            //}
            //if (user != null)
            //{
            //    p.UserID = (Guid)user.ProviderUserKey;
            //    Roles.AddUserToRole(user.UserName, "User");
            //}
            //TicketRepository TicketRepository = new TicketRepository(Dc);
            //p.Tickets.Add(TicketRepository.CreateTicket(Constants.TicketType.ApprovalRequest, Constants.TicketStatus.Pending, DateTime.Now.AddDays(60), "", p.ID, false));
            //Dc.People.Add(p);
            //try
            //{
            //    Dc.SaveChanges();
            //    //FormsAuthentication.SetAuthCookie(personalInformation.UserName, true /* createPersistentCookie */);
            //    Message.Success = true;
            //    Message.Message = "Account created successfully. kindly note that you won't be able to login untill your Request is Approved";
            //    return Message;
            //}
            //catch (Exception ex)
            //{
            //    System.Web.Security.Membership.DeleteUser(personalInformation.UserName, true);
            //    Message.Success = false;
            //    Message.Message = "An unexpected error has occurred. please try again later.";
            //    return Message;
            //}
            return Message;

		}
        //public JsonMessage RegisterNonBusinessUser(RegisterVM personalInformation, RegisterVM accountInformation)
        //{
        //    JsonMessage Message = new JsonMessage();
        //    MembershipUser user = null;
        //    Person p = new Person();

        //    p.ID = Guid.NewGuid();
        //    p.FirstName = personalInformation.FirstName;
        //    p.LastName = personalInformation.LastName;

        //    try
        //    {
        //        user = System.Web.Security.Membership.CreateUser(personalInformation.UserName, personalInformation.Password, personalInformation.Email);
        //    }
        //    catch (MembershipCreateUserException e)
        //    {
        //        Message.Success = false;
        //        Message.Message = ErrorCodeToString(e.StatusCode);
        //        return Message;
        //    }
        //    if (user != null)
        //    {
        //        p.UserID = (Guid)user.ProviderUserKey;
        //        Roles.AddUserToRole(user.UserName, "User");
        //    }
        //    p.IsApproved = true;
        //    AccountRepository AccountRepository = new AccountRepository(Dc);
        //    p.Account = AccountRepository.CreateAccount(accountInformation);
        //    SubscriptionRepository subscriptionRepository = new SubscriptionRepository(Dc);
        //    p.Account.Subscriptions.Add(subscriptionRepository.CreateSubscriptionPlan(personalInformation, p.ID));
        //    Dc.People.Add(p);
        //    try
        //    {
        //        Dc.SaveChanges();
        //        FormsAuthentication.SetAuthCookie(personalInformation.UserName, true /* createPersistentCookie */);
        //        Message.Success = true;
        //        Message.Message = "Account created successfully.";
        //        return Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Web.Security.Membership.DeleteUser(personalInformation.UserName, true);
        //        Message.Success = false;
        //        Message.Message = "An unexpected error has occurred. please try again later.";
        //        return Message;
        //    }
			
        //}

        //public Guid GetCurrentUserID(string Username)
        //{
        //    //UserRepository UserRepository = new Repositories.UserRepository(Dc);
        //    //MembershipUser user = System.Web.Security.Membership.GetUser(Username);
        //    //return (Guid)user..ProviderUserKey;
        //}
        //public  Person LoadByFilter(Func<Person, bool> predicate)
        //{
        //    var Query = Dc.People.Where(predicate);
        //    Person Person = Query.FirstOrDefault();
        //    return Person;
        //}
        //public bool IsUserApproved(String UserName)
        //{
        //    return LoadByFilter(p => p.UserID == GetCurrentUserID(UserName)).IsApproved;
        //}

        #region Status Codes
        private  string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}