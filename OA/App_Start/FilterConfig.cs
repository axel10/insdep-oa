using System.Web;
using System.Web.Mvc;
using OA.Models;

namespace OA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());
        }
    }
}
