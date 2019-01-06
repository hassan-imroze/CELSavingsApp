using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class SmallBusinessesController : Controller
    {

        // GET: SmallBusinesses
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult Index()
        {
            ViewBag.Title = "Small Businesses";
            return View();
        }
        
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult New()
        {
            ViewBag.Title = "Create Small Business";

            return View();
        }

        
        public ActionResult Details(int id)
        {
            if (!User.IsInRole(RoleName.CanManageSavingAccounts))
                return HttpNotFound();

            ViewBag.Title = "Small Business Details";
            return View();
        }

        public ActionResult AddPayment(int id)
        {
            if (!User.IsInRole(RoleName.CanManageSavingAccounts))
                return HttpNotFound();

            ViewBag.Title = "Add Small Business Payment";
            return View();
        }
    }
}