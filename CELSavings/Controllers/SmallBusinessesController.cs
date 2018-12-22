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
        public ActionResult New()
        {
            ViewBag.Title = "Create Small Business";

            return View();
        }
    }
}