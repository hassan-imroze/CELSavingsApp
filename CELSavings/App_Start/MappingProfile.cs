using AutoMapper;
using CELSavings.Dto;
using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<SavingAccount, SavingAccountDto>();
            //CreateMap<MembershipType, MembershipTypeDto>();
            //.ForMember(m=> m.Id,opt=> opt.Ignore());
        }
    }
}