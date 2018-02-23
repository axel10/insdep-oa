using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using OA.Models;
using Spring.Web.Mvc;

namespace OA
{
    public class MvcApplication :  Spring.Web.Mvc.SpringMvcApplication  //System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
            },filePath);
        }
    }
}
