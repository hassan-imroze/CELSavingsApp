using CELSavings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CELSavings.Controllers.Api
{
    public class SmallBusinessesController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public IHttpActionResult CreateSmallBusiness(SaveSmallBusinessDto newSmallBusinessDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            return Ok();

        }
    }
}
