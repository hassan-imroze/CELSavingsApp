using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class SavingAccountsController : Controller
    {
        // GET: SavingAccounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            ViewBag.Title = "New Savings Account";

            return View("SavingsAccountForm", new SavingAccount());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SavingAccount savingAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = savingAccount.Id == 0 ? "New Savings Account" : "Edit Savings Account";
                    return View("SavingsAccountForm", savingAccount);
                }
                //using (ICustomerRepository customerRepository = new CustomerRepository())
                //{
                //    customerRepository.Save(customer);
                //}
                return savingAccount.Id == 0 ? RedirectToAction("New") : RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}