﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CELSavings.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            ViewBag.Title = "Payment Status";
            return View();
        }

        public ActionResult AddPayment()
        {
            ViewBag.Title = "Add Payment";

            return View("AddPayment");
        }
    }
}