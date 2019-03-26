using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageSavingAccounts))
            {
                ViewBag.Title = "Payment Status";
                return View();
            }
            else
            {
                ViewBag.Title = "Payments";
                return View();
            }
        }

        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult AddPayment()
        {
            ViewBag.Title = "Add Payment";

            return View("AddPayment");
        }

        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult PaymentDefalters()
        {
            ViewBag.Title = "Payment Defaulters";
            return View();
        }
    }
}