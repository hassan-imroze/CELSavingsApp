using CELSavings.Repository;
using CELSavings.ViewModels;
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

            var repository = new SmallBusinessRepository();
            var viewModel = repository.GetPaymentViewModelById(id);
            repository.Dispose();

            if (viewModel == null)
                return HttpNotFound();

            ViewBag.Title = "Add Small Business Payment";
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult AddPayment(SmallBusinessPaymentViewModel viewModel)
        {
            var repository = new SmallBusinessRepository();
            var smallBusiness = repository.GetById(viewModel.SmallBusinessId);
            repository.Dispose();

            if (smallBusiness == null)
                return HttpNotFound();

            if(smallBusiness.SellingPrice < smallBusiness.PaymentReceived + viewModel.PaidAmount)
            {
                ViewBag.Title = "Add Small Business Payment";
                TempData["ErrorMessage"] = "Total Payment Received exceeded selling price";
                return View(viewModel);
            }

            using (repository = new SmallBusinessRepository())
            {
                repository.ReceivePayment(viewModel);
            }

            TempData["SuccessMessage"] = "Payment Added successfully.";
            return RedirectToAction("Index");
        }
    }
}