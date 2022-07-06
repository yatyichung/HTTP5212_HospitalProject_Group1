using System.Web;
using System.Web.Mvc;

namespace HTTP5212_HospitalProject_Team1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
