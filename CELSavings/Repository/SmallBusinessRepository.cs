using CELSavings.Dto;
using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace CELSavings.Repository
{
    public class SmallBusinessRepository : RepositoryBase
    {

        public void Create(SmallBusiness smallBusiness)
        {

            smallBusiness.InvolvedSavingAccounts = _context.SavingAccounts.Where(x => x.Status == MemberStatus.Live)
                                                           .ToList()
                                                           .Select(x => new SmallBusinessSavingAccount { SavingAccountId = x.Id })
                                                           .ToList();

            if (smallBusiness.InvolvedSavingAccounts.Count > 0)
            {
                smallBusiness.Installments = GlobalFunctions.CreateInstallments(smallBusiness.SellingPrice, smallBusiness.InstallmentStartDate)
                                                            .Select(x => new SmallBusinessInstallment
                                                            {
                                                                Amount = x.Amount,
                                                                DueDate = x.DueDate
                                                            })
                                                            .ToList();

                if (smallBusiness.BuyingPrice > smallBusiness.InitialPayment)
                {
                    var totalCost = smallBusiness.BuyingPrice - smallBusiness.InitialPayment;
                    var individualCost = totalCost / (decimal)smallBusiness.InvolvedSavingAccounts.Count();

                    smallBusiness.Transactions = smallBusiness.InvolvedSavingAccounts
                                                              .Select(x => new Transaction
                                                              {
                                                                  SavingAccountId = x.SavingAccountId,
                                                                  Amount = individualCost,
                                                                  Description = string.Format("Small Business:- Product:{0}, Customer:{1}, Total Cost:{2} , Total accounts involved:{3} ", smallBusiness.Product, smallBusiness.CustomerName, totalCost.ToString("#,###.00"), smallBusiness.InvolvedSavingAccounts.Count),
                                                                  TransactionDate = DateTime.Today,
                                                                  Type = TransactionType.Small_Business,
                                                                  TransactionSide = TransactionSide.Debit
                                                              })
                                                              .ToList();
                }

                if(smallBusiness.InitialPayment > 0)
                {
                    var tempPaymentAmount = smallBusiness.InitialPayment;

                    foreach (var item in smallBusiness.Installments.OrderBy(x=> x.DueDate))
                    {
                        if(tempPaymentAmount < item.Amount)
                        {
                            smallBusiness.PaymentDueDate = item.DueDate;
                            break;
                        }
                        tempPaymentAmount -= item.Amount;
                    }
                }

                _context.SmallBusinesses.Add(smallBusiness);
                
            }
            _context.SaveChanges();
        }

        public List<SmallBusiness> GetAll()
        {
            return _context.SmallBusinesses.Include(x=>x.CustomerOrGuarantor).ToList();
        }
       
    }
}