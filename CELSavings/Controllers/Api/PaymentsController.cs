﻿using AutoMapper;
using CELSavings.Dto;
using CELSavings.Models;
using CELSavings.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CELSavings.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        // POST /api/payments
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public IHttpActionResult CreatePayments(PaymentDto newPaymentDto)
        {
            if (newPaymentDto.SavingsAccountId <= 0)
                return BadRequest("Savings Account is not available");
            else if(newPaymentDto.Amount <=0)
                return BadRequest("Amount must be greater than zero");
            var newPayment = new Payment
            {
                SavingAccountId = newPaymentDto.SavingsAccountId,
                Amount = newPaymentDto.Amount
            };

            using (var repository = new PaymentRepository())
            {
                repository.Create(newPayment);
            }

            return Ok();

        }

        [HttpGet]
        [Route("api/payments/summary/{email}/")]
        public IHttpActionResult GetPaymentSummary(string email)
        {
            List<Payment> payments = new List<Payment>();

            using (var repo = new PaymentRepository())
            {
                payments = repo.GetPayments(true,email);
            }

            var paymentSummary = payments.GroupBy(l => l.SavingAccountId)
                                 .Select(lg => new
                                  {
                                    lg.First().SavingAccount.AccountNo,
                                    lg.First().SavingAccount.Name,
                                    PaymentAmount = lg.Sum(x => x.Amount)
                                  });

            return Ok(paymentSummary);
            
        }

        [HttpGet]
        [Route("api/payments/defaulters/")]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public IHttpActionResult GetPaymentDefaulters()
        {
            List<SavingAccount> savingsAccounts = new List<SavingAccount>();

            using (var repo = new SavingAccountRepository())
            {
                savingsAccounts = repo.GetPayableSavingsAccounts();
            }

            return Ok(savingsAccounts.Select(Mapper.Map<SavingAccount, PaymentDefaultersDto>));

           

        }
    }
}
