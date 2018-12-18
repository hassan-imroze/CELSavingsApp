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
            return View();
        }
    }
}