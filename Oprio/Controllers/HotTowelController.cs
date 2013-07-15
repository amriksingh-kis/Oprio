using JetWeb.Controllers;
using Oprio.Models;
using Oprio.Utils.Filters;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Oprio.Controllers
{
    [InitializeSimpleMembership]
    public class HotTowelController : BaseController
    {


        //
        // GET: /HotTowel/
        public ActionResult Index()
        {
            //ViewBag.NGon.isAuthenticated = WebSecurity.IsAuthenticated;
            //if (WebSecurity.IsAuthenticated)
            //{
            //    ViewBag.NGon.userId = WebSecurity.CurrentUserId;
            //    ViewBag.NGon.userName = WebSecurity.CurrentUserName;
            //}

            return View();
        }

    }
}
