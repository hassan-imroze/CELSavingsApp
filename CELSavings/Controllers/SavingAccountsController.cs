using CELSavings.Models;
using CELSavings.Repository;
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
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult Index()
        {
            ViewBag.Title = "Savings Account Members";
            if (User.IsInRole(RoleName.CanManageSavingAccounts))
            {
                return View("List");
            }

            return View("ListReadOnly");
        }

        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult New()
        {
            ViewBag.Title = "New Savings Account";

            return View("SavingsAccountForm", new SavingAccount());
        }

        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit Savings Account";

            var repository = new SavingAccountRepository();
            var savingAccount = repository.GetById(Id);
            if (savingAccount == null)
                return HttpNotFound();

            return View("SavingsAccountForm", savingAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public ActionResult Save(SavingAccount savingAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = savingAccount.Id == 0 ? "New Savings Account" : "Edit Savings Account";
                    return View("SavingsAccountForm", savingAccount);
                }
                var isNew = savingAccount.Id == 0;
                using (var repository = new SavingAccountRepository())
                {
                    repository.Save(savingAccount);
                }
                TempData["SuccessMessage"] = string.Format("Member {0} successfully.", isNew ? "saved" : "updated");
                return isNew ? RedirectToAction("New") : RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}