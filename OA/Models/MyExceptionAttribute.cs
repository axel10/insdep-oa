
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OA.Models
{
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            ExceptionQueue.Enqueue(ex);
            filterContext.HttpContext.Response.Redirect("/error.html");
        }
    }
}