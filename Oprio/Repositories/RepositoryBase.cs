using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Oprio.Models;

namespace JetWeb.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        private JetContext _dc;

        protected JetContext Dc
        {
            get 
            {
                if (_dc == null)
                    _dc = new JetContext();
                return _dc;
            }
            set { _dc = value; }
        }

        protected static IPrincipal CurrentUser
        {
            get { return System.Threading.Thread.CurrentPrincipal; }
        }

        public void Dispose()
        {
            _dc.Dispose();
        }


    }
}