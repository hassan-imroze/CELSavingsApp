using AutoMapper;
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
    public class PayableSavingAccountsController : ApiController
    {

        
        public IHttpActionResult Get(string query = null)
        {
            List<SavingAccount> savingsAccounts = new List<SavingAccount>();

            using (var repo = new SavingAccountRepository())
            {
                savingsAccounts = repo.GetSavingsAccounts(query);
            }

            return Ok(savingsAccounts.Select(Mapper.Map<SavingAccount, PayableSavingAccountDto>));
        }
    }
}
