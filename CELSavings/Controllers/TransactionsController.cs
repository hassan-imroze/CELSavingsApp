using CELSavings.Models;
using CELSavings.Repository;
using CELSavings.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions

        public ActionResult AccountStatement(int Id)
        {
            SavingAccount savingAccount = null;
            using (var repository = new SavingAccountRepository())
            {
                savingAccount = repository.GetByIdWithTransactions(Id);
            }

            if (savingAccount == null)
                return HttpNotFound();

            if (!User.IsInRole(RoleName.CanManageSavingAccounts))
            {
                if (User.Identity.Name.ToLower().Trim() != savingAccount.Email.ToLower().Trim())
                    return HttpNotFound();
            }

            var viewModel = AccountStatementViewModel.CreateObject(savingAccount);

            return View(viewModel);
        }
    }
}