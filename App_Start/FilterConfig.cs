using System;
using System.Web;
using System.Web.Mvc;

namespace SmartRestaurantWasteManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        internal static void RegisterGlobalFilters(object filters)
        {
            throw new NotImplementedException();
        }
    }
}
