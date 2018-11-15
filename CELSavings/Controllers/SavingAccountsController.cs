﻿using CELSavings.Models;
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
        public ActionResult Index()
        {
            ViewBag.Title = "Savings Account Members";
            return View();
        }

        public ActionResult New()
        {
            ViewBag.Title = "New Savings Account";

            return View("SavingsAccountForm", new SavingAccount());
        }

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
                return isNew ? RedirectToAction("New") : RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}