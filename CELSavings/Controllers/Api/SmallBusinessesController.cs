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
    public class SmallBusinessesController : ApiController
    {
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageSavingAccounts)]
        public IHttpActionResult CreateSmallBusiness(SaveSmallBusinessDto newSmallBusinessDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var smallBusiness = Mapper.Map<SaveSmallBusinessDto,SmallBusiness>(newSmallBusinessDto);

            using (var repo = new SmallBusinessRepository())
            {
                repo.Create(smallBusiness);
            }

            return Ok();

        }
    }
}
