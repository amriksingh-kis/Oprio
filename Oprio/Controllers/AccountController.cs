using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using JetWeb.Models;
using Oprio.Models;
using System.Web.Script.Serialization;
//using JetModel.TinyModels;
using Newtonsoft.Json.Linq;
using Oprio.Utils.Filters;
using WebMatrix.WebData;
using JetWeb.Repositories;
namespace JetWeb.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : BaseController 
    {
        [AllowAnonymous]
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult LogOn(string userInfo)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            LogOnModel model = serializer.Deserialize<LogOnModel>(userInfo);
            JsonMessage message = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                    {
                        if (WebSecurity.IsConfirmed(model.UserName))
                        {
                            var person = Dc.People.FirstOrDefault(x => x.UserName == model.UserName);
                            string fullName = string.Empty;
                            if (person != null)
                            {
                                fullName = string.Format("{0} {1}", person.FirstName, person.LastName);
                            }
                            message = new JsonMessage { FailCount = 0, Message = WebSecurity.GetUserId(model.UserName).ToString(), Success = true, SuccessCount = 0, RedirectUrl = "#/conversations/priority", Name = fullName };
                            return Json(message);
                        }
                        else
                        {
                            message = new JsonMessage { FailCount = 0, Message = "Your aren't authorised to login", Success = false, SuccessCount = 0 };
                            TempData["Error"] = "Your are not authorised to login";
                            return Json(message);
                           
                        }
                    }
                    else
                    {

                        TempData["Error"] = "The user name or password provided is incorrect.";
                        message = new JsonMessage { FailCount = 0, Message = "The user name or password provided is incorrect.", Success = false, SuccessCount = 0 };
                        return Json(message);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    message = new JsonMessage { FailCount = 0, Message = ex.Message, Success = false, SuccessCount = 0 };
                }
            }

            return Json(message);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult LogOff()
        {
            //return RedirectToAction("Index", "Home");
            WebSecurity.Logout();

            Session.Abandon();
            Session.Clear();
            HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();

            return Json(new { success = true });
        }

        //
        // GET: /Account/Register






        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword (logged in user)
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, error = "The current password is incorrect or the new password is invalid." });
                }
            }
            return Json(new { success = false, error = "Invalid Request." });
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult ResetPasswordRequest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResetPasswordRequest(string email)
        {
            //check vaild mail
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ResetPasswordModel model2 = serializer.Deserialize<ResetPasswordModel>(email);
            ResetPasswordModel model = new ResetPasswordModel(model2.Email);
            model.ResetRequest();
            JsonMessage message = new JsonMessage { FailCount = 0, Message = model.StatusMessage, Success = !model.IsError, SuccessCount = 0 };
            return Json(message);
        }

        //id: ticket id
        [HttpPost]


        public JsonResult ResetPasswordValidate(int id)
        {
            //check if ticket is valid and show reset form if it is
            Ticket t = ResetPasswordModel.GetActiveTicket(id);

            if (t != null)
            {
                JsonMessage message = new JsonMessage { FailCount = 0, Message = "id was found", Success = true, SuccessCount = 0 };
                return Json(message);
            }
            else
            {
                JsonMessage message = new JsonMessage { FailCount = 0, Message = "id is not found", Success = false, SuccessCount = 0 };
                return Json(message);
            }
        }

        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResetPassword(string password, int ticketID)
        {
            NewPasswordModel model = new NewPasswordModel(ticketID);
            model.NewPassword = password;
            model.Reset();
            JsonMessage message = new JsonMessage { FailCount = 0, Message = model.StatusMessage, Success = !model.IsError, SuccessCount = 0 };
            return Json(message);
        }

        /// <summary>
        /// Register Get
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View(new RegisterVM());
        }

        /// <summary>
        /// Check if Valid BusinessEmail
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public JsonResult CheckDomains(string Email)
        {
            string Domain = string.Empty;
            if (!Email.Contains('@'))
                return Json("Wrong email format");

            Domain = Email.Split('@')[1].ToLower();
            //OrganisationRepository OrganisationRepository = new OrganisationRepository(Dc);
            if (Dc.FreeDomains.Any(o => o.Domain.ToLower() == Domain))
                return Json("free");
            else
            {
                var org = Dc.OrganisationDomains.FirstOrDefault(o => o.Domain.ToLower() == Domain).Organisation;
                return Json(new Data.TinyModels.Organisation() { Id = org.Id, Name = org.Name });
            }
        }


        /// <summary>
        /// Request Access for a business account to an exisiting organization
        /// </summary>
        /// <param name="PersonalInformationJson"></param>
        /// <returns></returns>
        public JsonResult RequestAccessToJoinOrganisation(string PersonalInformationJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RegisterVM personalInformation = serializer.Deserialize<RegisterVM>(PersonalInformationJson);
            UserRepository userRespository = new UserRepository(Dc);
            return Json(userRespository.RegisterOrganizationUser(personalInformation));
        }

        /// <summary>
        /// Submit NonBusiness Account
        /// </summary>
        /// <param name="PersonalInformationJson"></param>
        /// <param name="SubscriptionTypeJson"></param>
        /// <param name="AccountInformationJson"></param>
        /// <returns></returns>
        public JsonResult SubmitNonBusinessAccount(string PersonalInformationJson, string SubscriptionTypeJson, string AccountInformationJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RegisterVM personalInformation = serializer.Deserialize<RegisterVM>(PersonalInformationJson);
            dynamic subscription = JObject.Parse(SubscriptionTypeJson);
            personalInformation.SubscriptionTypeID = Int32.Parse(subscription["SelectedSubscriptionType"]["ID"].ToString());

            RegisterVM accountInformation = serializer.Deserialize<RegisterVM>(AccountInformationJson);
            dynamic account = JObject.Parse(AccountInformationJson);
            accountInformation.PaymentMethodID = int.Parse(account["PaymentMethodID"]["ID"].ToString());
            accountInformation.PaymentFrequencyID = int.Parse(account["PaymentFrequencyID"]["ID"].ToString());
            accountInformation.PaymentTermsID = int.Parse(account["PaymentTermsID"]["ID"].ToString());
            accountInformation.SubscriptionTypeID = personalInformation.SubscriptionTypeID;
            UserRepository userResposity = new UserRepository(Dc);
            //return Json(userResposity.RegisterNonBusinessUser(personalInformation, accountInformation));
            return null;
        }

        /// <summary>
        /// Submit Business Account
        /// </summary>
        /// <param name="PersonalInformationJson"></param>
        /// <param name="SubscriptionTypeJson"></param>
        /// <param name="AccountInformationJson"></param>
        /// <returns></returns>
        public JsonResult SubmitBusinessAccount(string PersonalInformationJson, string SubscriptionTypeJson, string AccountInformationJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RegisterVM personalInformation = serializer.Deserialize<RegisterVM>(PersonalInformationJson);
            dynamic subscription = JObject.Parse(SubscriptionTypeJson);
            personalInformation.SubscriptionTypeID = Int32.Parse(subscription["SelectedSubscriptionType"]["ID"].ToString());
            RegisterVM accountInformation = serializer.Deserialize<RegisterVM>(AccountInformationJson);

            dynamic account = JObject.Parse(AccountInformationJson);
            accountInformation.PaymentMethodID = int.Parse(account["PaymentMethodID"]["ID"].ToString());
            accountInformation.PaymentFrequencyID = int.Parse(account["PaymentFrequencyID"]["ID"].ToString());
            accountInformation.PaymentTermsID = int.Parse(account["PaymentTermsID"]["ID"].ToString());
            accountInformation.SubscriptionTypeID = personalInformation.SubscriptionTypeID;
            UserRepository userResposity = new UserRepository(Dc);
            return Json(userResposity.RegisterBusinessAccount(personalInformation, accountInformation));
        }


    }
}
