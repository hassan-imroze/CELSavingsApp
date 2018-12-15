using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class HomeController : Controller
    {
       

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else if(User.IsInRole(RoleName.CanManageSavingAccounts))
            {
                return View();
            }
            else
            {
                ViewBag.UserEmail = User.Identity.Name;
                return View("AccountHolderDashboard");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts";

            return View();
        }
    }
}