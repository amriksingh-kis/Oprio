using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oprio.Models;

namespace JetWeb.Controllers
{
    public class BaseController : Controller
    {
        private JetContext _DataContext;

        public JetContext Dc
        {
            get 
            {
                if (_DataContext == null)
                    _DataContext = new JetContext();
                return _DataContext;    
            }
        }
    }
}