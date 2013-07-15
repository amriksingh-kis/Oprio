using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Threading;
using System.Net.Http.Headers;

namespace Oprio.Utils.Filters
{
    public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("HttpActionContext");
            }

            if (actionContext.Request.Method != HttpMethod.Get)
            {
                return ValidateAntiForgeryToken(actionContext, cancellationToken, continuation);
            }

            return continuation();
        }

        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }

        private Task<HttpResponseMessage> ValidateAntiForgeryToken(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            //try
            //{
            //    CookieState cookieToken = null ;
            //    string formToken = "";
            //    IEnumerable<string> tokenHeaders;
            //    if (actionContext.Request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
            //    {
            //        formToken = tokenHeaders.First();
            //        var headers = actionContext.Request.Headers;
            //        cookieToken = headers.GetCookies().Select((c) => c[AntiForgeryConfig.CookieName]).FirstOrDefault();
                    
            //    }
            //    AntiForgery.Validate(cookieToken != null ? cookieToken.Value : null, formToken);
            //}
            //catch (System.Web.Mvc.HttpAntiForgeryException ex)
            //{
            //    actionContext.Response = new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.Forbidden,
            //        RequestMessage = actionContext.ControllerContext.Request
            //    };
            //    return FromResult(actionContext.Response);
            //}
            return continuation();
        }
    }
}