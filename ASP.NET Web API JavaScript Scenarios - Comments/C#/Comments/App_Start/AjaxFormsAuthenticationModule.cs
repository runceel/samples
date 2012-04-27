using System;
using System.Web;

namespace Comments.App_Start
{
    public class AjaxFormsAuthenticationModule : IHttpModule
    {
        private const string FixupKey = "__WEBAPI:Authentication-Fixup";
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostReleaseRequestState += OnPostReleaseRequestState;
            context.EndRequest += OnEndRequest;
        }

        private void OnPostReleaseRequestState(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;
            var request = context.Request;

            bool isAjax = request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if ((response.StatusCode == 401 || response.StatusCode == 403) && isAjax)
            {
                context.Context.Items[FixupKey] = response.StatusCode;
            }
        }

        private void OnEndRequest(object source, EventArgs args)
        {
            var context = (HttpApplication)source;
            var response = context.Response;

            if (context.Context.Items.Contains("__WEBAPI:Authentication-Fixup"))
            {
                response.StatusCode = (int)context.Context.Items[FixupKey];
                response.RedirectLocation = null;
            }
        }
    }
}
