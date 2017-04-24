using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Controllers
{
    public class BaseController : Controller
    {

        protected HttpCookie cookieName = null;

        // GET: Base
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            
            ValidateLogin(Request);
            return base.BeginExecuteCore(callback, state);
        }


        protected void ValidateLogin(HttpRequestBase request)
        {
            if (request.Cookies["usuario"] != null)
            {
                cookieName = request.Cookies.Get("usuario");
            }
        }
    }
}