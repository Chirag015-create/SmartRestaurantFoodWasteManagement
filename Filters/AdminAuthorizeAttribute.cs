using System.Web.Mvc;

namespace SmartRestaurantWasteManagement.Filters
{
    /// <summary>
    /// Only lets Session["Role"] == "Admin" through.
    /// Everyone else gets bounced to /Admin/Login.
    /// </summary>
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            bool isAdmin = session != null
                        && session["Role"] != null
                        && session["Role"].ToString() == "Admin";

            if (!isAdmin)
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}