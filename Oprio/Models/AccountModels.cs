using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using Oprio.Models;
using WebMatrix.WebData;
using Oprio.Models;

namespace JetWeb.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Enter you account email")]
        public string Email { get; set; }
        public bool IsError { get; private set; }
        public string StatusMessage { get; private set; }

        public ResetPasswordModel() { }
        public ResetPasswordModel(string email)
        {
            Email = email;
        }

        public void ResetRequest()
        {
            if (Email != null && Email != "")
            {
                JetContext db = new JetContext();
                var person = db.People.FirstOrDefault(x => x.Email.Equals(Email, StringComparison.InvariantCultureIgnoreCase));
                if (person != null)
                {
                    IsError = true;
                    StatusMessage = "We couldn't find a record for that email address!";
                }
                else
                {
                    //create passwordreset ticket
                    //Repositories.TicketRepository ticketRepo = new Repositories.TicketRepository();
                    //Repositories.UserRepository userRepo = new Repositories.UserRepository();

                    //CHANGE EMAIL TO USERNAME after profile table creation
                    string token = WebSecurity.GeneratePasswordResetToken(person.Email);

                    Ticket t = new Ticket();
                    t.IssueDate = DateTime.Now;
                    t.Person = person;
                    t.StatusID = (int)Constants.TicketStatus.Pending;
                    t.TicketRef = token;
                    t.TicketTypeID = (int)Constants.TicketType.PasswordReset;


                    //send ticket email with reset link
                    JetWeb.Email emailUtil = new Email(Email, EmailTemplates.PwdReset, new { Host = HttpContext.Current.Request.Url.Host, ID = t.TicketRef });
                    emailUtil.Send();
                    StatusMessage = "Please check your email to reset your password.";
                }
            }
        }

        public static Ticket GetActiveTicket(int id)
        {
            //Repositories.TicketRepository repo = new Repositories.TicketRepository();
            JetContext dc = new JetContext();
            Ticket t = dc.Tickets.FirstOrDefault(x => x.Id == id);
            if (t != null)
            {
                //valid
                if (t.ExpireDate > DateTime.Now)
                {
                    return t;
                }
                else
                {
                    //delete expired ticket
                    dc.Tickets.Remove(t);
                    dc.SaveChanges();
                }
            }
            return null;
        }
    }

    public class NewPasswordModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        public bool IsError { get; private set; }
        public string StatusMessage { get; private set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int TicketID { get; set; }

        public NewPasswordModel(int tid)
        {
            TicketID = tid;
        }

        public void Reset()
        {
            //Repositories.TicketRepository repo = new Repositories.TicketRepository();
            //Ticket t = repo.GetByID(TicketID);

            JetContext dc = new JetContext();
            Ticket t = dc.Tickets.FirstOrDefault(x => x.Id == TicketID);

            if (t == null)
            {
                IsError = true;
                StatusMessage = "Invalid Ticket.";
                return;
            }

            //MembershipUser u = System.Web.Security.Membership.GetUser(System.Web.Security.Membership.GetUserNameByEmail(t.TicketRef));
            if (WebSecurity.ResetPassword(t.TicketRef, NewPassword))
            {
                dc.Tickets.Remove(t);
                StatusMessage = "Password Reset Successfully.  You'll now be redirected.";
            }
            else
            {
                IsError = true;
                StatusMessage = "Reset Password Failed.";
            }
        }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterVM
    {

        // SubscriptionType
        [Required]
        [Display(Name = "Subscription Type")]
        public int SubscriptionTypeID { get; set; }

        // EmailValidationGroup
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        //PersonalInformation
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // AccountInformation
        [Required]
        [Display(Name = "Organization")]
        public string Organization { get; set; }

        public Guid? OrganizationID { get; set; }

        [Required]
        [Display(Name = "Billing Name")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string BillingName { get; set; }

        [Display(Name = "Address 1")]
        [StringLength(100, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string AddressLine1 { get; set; }

        [Required]
        [Display(Name = "Address 2")]
        [StringLength(100, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(5, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        [StringLength(100, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Contact Phone")]
        [StringLength(20, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string ContactPhone { get; set; }

        [Required]
        [Display(Name = "Contact Email")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string ContactEmail { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string DisplayName { get; set; }



        [Required]
        [Display(Name = "Technical Contact Name")]
        [StringLength(100, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string TechnicalContactName { get; set; }

        [Required]
        [Display(Name = "Technical Contact Phone")]
        [StringLength(20, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string TechnicalContactPhone { get; set; }

        [Required]
        [Display(Name = "Technical Contact Email")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]

        public string TechnicalContactEmail { get; set; }





        [Required]
        [Display(Name = "Payment Method")]

        public int PaymentMethodID { get; set; }

        [Required]
        [Display(Name = "Payment Frequency")]

        public int PaymentFrequencyID { get; set; }

        [Required]
        [Display(Name = "Payment Terms")]

        public int PaymentTermsID { get; set; }

        [Required]
        [Display(Name = "Storage Usage")]

        public int StorageUsageMB { get; set; }


        [Display(Name = "Sales Tax Ref")]
        [StringLength(64, ErrorMessage = "The {0} must not exceed {1} characters long.")]
        public string SalesTaxRef { get; set; }

        [HiddenInput]
        public string OrganisationID { get; set; }


        public bool IsBusinessAccount { get; set; }

        public Guid? AccountID { get; set; }


    }
}
