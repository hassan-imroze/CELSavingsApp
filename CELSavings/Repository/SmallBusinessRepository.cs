using CELSavings.Dto;
using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using CELSavings.ViewModels;

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

                if (smallBusiness.InitialPayment > 0)
                {
                    var tempPaymentAmount = smallBusiness.InitialPayment;
                    bool allPaid = true;
                    foreach (var item in smallBusiness.Installments.OrderBy(x => x.DueDate))
                    {
                        if (tempPaymentAmount < item.Amount)
                        {
                            smallBusiness.PaymentDueDate = item.DueDate;
                            allPaid = false;
                            break;
                        }
                        tempPaymentAmount -= item.Amount;
                    }
                    if (allPaid)
                    {
                        smallBusiness.PaymentDueDate = null;
                    }
                }

                _context.SmallBusinesses.Add(smallBusiness);

            }
            _context.SaveChanges();
        }

        internal void ReceivePayment(SmallBusinessPaymentViewModel viewModel)
        {
            var smallBusiness = _context.SmallBusinesses
                                        .Where(x => x.Id == viewModel.SmallBusinessId)
                                        .Include(x => x.Installments)
                                        .Include(x => x.Payments)
                                        .Include(x => x.InvolvedSavingAccounts)
                                        .SingleOrDefault();
            if (smallBusiness != null && 
                (smallBusiness.SellingPrice >= smallBusiness.PaymentReceived + viewModel.PaidAmount) &&
                viewModel.PaidAmount > 0)
            {
                smallBusiness.PaymentReceived += viewModel.PaidAmount;
                var tempPaymentAmount = smallBusiness.PaymentReceived;
                bool allPaid = true;
                foreach (var item in smallBusiness.Installments.OrderBy(x => x.DueDate))
                {
                    if (tempPaymentAmount < item.Amount)
                    {
                        smallBusiness.PaymentDueDate = item.DueDate;
                        allPaid = false;
                        break;
                    }
                    tempPaymentAmount -= item.Amount;
                }
                if (allPaid)
                {
                    smallBusiness.PaymentDueDate = null;
                }

                var oSmallBusinessPayment = new SmallBusinessPayment();
                oSmallBusinessPayment.Amount = viewModel.PaidAmount;
                
                var individualAmount = viewModel.PaidAmount / (decimal)smallBusiness.InvolvedSavingAccounts.Count();
                oSmallBusinessPayment.Transactions = smallBusiness.InvolvedSavingAccounts
                                                              .Select(x => new Transaction
                                                              {
                                                                  SavingAccountId = x.SavingAccountId,
                                                                  Amount = individualAmount,
                                                                  Description = string.Format("Small Business Payment:- Product:{0:N2}, Received: {1:N2} , Total Received:{2:N2} of {3:N2}, Total accounts involved:{4}", smallBusiness.Product, viewModel.PaidAmount,smallBusiness.PaymentReceived,smallBusiness.SellingPrice, smallBusiness.InvolvedSavingAccounts.Count),
                                                                  TransactionDate = DateTime.Today,
                                                                  Type = TransactionType.Small_Business_Payment,
                                                                  TransactionSide = TransactionSide.Credit
                                                              })
                                                              .ToList();
                smallBusiness.Payments.Add(oSmallBusinessPayment);
                _context.SaveChanges();
            }
        }

        public List<SmallBusiness> GetAll()
        {
            return _context.SmallBusinesses.Include(x => x.CustomerOrGuarantor).ToList();
        }

        public SmallBusiness GetById(int id)
        {
            return _context.SmallBusinesses.Where(x => x.Id == id).SingleOrDefault();
        }

        public SmallBusinessPaymentViewModel GetPaymentViewModelById(int id)
        {
            SmallBusinessPaymentViewModel smallBusinessPaymentViewModel = null;

            var smallBusiness = _context.SmallBusinesses.Where(x => x.Id == id).Include(x => x.Installments).SingleOrDefault();
            if (smallBusiness != null)
            {
                smallBusinessPaymentViewModel = new SmallBusinessPaymentViewModel
                {
                    Product = smallBusiness.Product,
                    SmallBusinessId = smallBusiness.Id,
                    CustomerName = smallBusiness.CustomerName,
                    SellingPrice = smallBusiness.SellingPrice,
                    PaymentReceived = smallBusiness.PaymentReceived
                };

                decimal dueAmount = smallBusiness.Installments.Where(x => x.DueDate <= DateTime.Today).Sum(x => x.Amount);

                smallBusinessPaymentViewModel.PaymentDue = smallBusiness.PaymentReceived >= dueAmount ? 0 : dueAmount - smallBusiness.PaymentReceived;

            }

            return smallBusinessPaymentViewModel;
        }
    }
}