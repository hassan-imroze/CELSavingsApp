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

            CreateMap<SaveSmallBusinessDto, SmallBusiness>()
              .ForMember(m => m.Product, opt => opt.MapFrom(dto => dto.Product))
              .ForMember(m => m.ProductDescription, opt => opt.MapFrom(dto => dto.ProductDescription))
              .ForMember(m => m.CustomerOrGuarantorId, opt => opt.MapFrom(dto => dto.CustomerOrGuarantorId))
              .ForMember(m => m.CustomerName, opt => opt.MapFrom(dto => dto.CustomerName))
              .ForMember(m => m.CustomerPhone, opt => opt.MapFrom(dto => dto.CustomerPhone))
              .ForMember(m => m.BuyingPrice, opt => opt.MapFrom(dto => dto.BuyingPrice))
              .ForMember(m => m.ProfitPercentage, opt => opt.MapFrom(dto => dto.ProfitPercentage))
              .ForMember(m => m.SellingPrice, opt => opt.MapFrom(dto => dto.SellingPrice))
              .ForMember(m => m.InitialPayment, opt => opt.MapFrom(dto => dto.InitialPayment))
              .ForMember(m => m.PaymentReceived, opt => opt.MapFrom(dto => dto.InitialPayment))
              .ForMember(m => m.SellDate, opt => opt.MapFrom(dto => dto.SellDate!=null? dto.SellDate.Value : DateTime.Today))
              .ForMember(m => m.InstallmentStartDate, opt => opt.MapFrom(dto => dto.InstallmentStartDate != null ? dto.InstallmentStartDate.Value : DateTime.Today))
              .ForMember(m=> m.PaymentDueDate,opt => opt.MapFrom(dto => dto.SellingPrice == dto.InitialPayment ? null : dto.InstallmentStartDate))
              ;

            CreateMap<SmallBusiness, SmallBusinessListDto>()
               .ForMember(dto => dto.Status, opt => opt.MapFrom(m => m.SellingPrice == m.PaymentReceived ? "Completed" : (m.PaymentDueDate < DateTime.Today ? "Payment due" : "Pending")))
               .ForMember(dto => dto.StatusColor, opt => opt.MapFrom(m => m.SellingPrice == m.PaymentReceived ? "green" : (m.PaymentDueDate < DateTime.Today ? "red" : "black")))
               .ForMember(dto => dto.DueDate, opt => opt.MapFrom(m => m.PaymentDueDate != null ? m.PaymentDueDate.Value.FormattedDate() : ""));
        }
    }
}