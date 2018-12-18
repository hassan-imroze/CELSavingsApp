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

            CreateMap<SavingAccount, PayableSavingAccountDto>()
                .ForMember(dto => dto.SavingsAccountId, opt => opt.MapFrom(m => m.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(m => m.Name))
                .ForMember(dto => dto.PaymentMonth, 
                           opt => opt.MapFrom(
                               m => m.LastPaymentMonthDate == null ? 
                               GlobalConstants.STSTEMSTARTMONTH.FormattedMonth() : 
                               m.LastPaymentMonthDate.Value.AddDays(2).FormattedMonth()));

            CreateMap<Payment, PaymentListDto>()
               .ForMember(dto => dto.AccountNo, opt => opt.MapFrom(m => m.SavingAccount.AccountNo))
               .ForMember(dto => dto.PaymentMonth, opt => opt.MapFrom(m => m.PaymentMonth))
               .ForMember(dto => dto.PaymentAmount,opt => opt.MapFrom(m => m.Amount));


        }
    }
}