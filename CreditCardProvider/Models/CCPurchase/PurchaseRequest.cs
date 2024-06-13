using CreditCardProvider.Data;
using CreditCardProvider.Models.Domain;
using CreditCardProvider.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CreditCardProvider.Models.CCPurchase
{
    public class PurchaseRequest 
    {
        private readonly CCDBContext _ccDBContext;

        public PurchaseRequest(global::Moq.Mock<CCDBContext> mockCCDBContect)
        {
        }

        public PurchaseRequest(CCDBContext cCDBContext)
        {
            _ccDBContext = cCDBContext;
        }

        //public string AddPurchase(string cardNumber, string lastName, int purchaseAmount)
        //{
        //    _ccDBContext.Add(CCTransactions);
        //    _ccDBContext.SaveChanges();
        //}

        public string AddPurchaseTranscation(string cardNumber, string lastName, int purchaseAmount)
        {            

            string returnMessage = string.Empty;

            var ccCustomer = _ccDBContext.CCCustomers.Where(t => t.CardNumber == Convert.ToInt32(cardNumber) && t.LastName == lastName).FirstOrDefault();

            if (ccCustomer != null)
            {
                //Get the lastest transaction by CustomerID
                var ccTransactions = _ccDBContext.CCTransactions.Where(t => t.CCCustomerId == ccCustomer.ID).FirstOrDefault();

                if (ccTransactions != null)
                {
                    var ccTransactionLatest = _ccDBContext.CCTransactions.Where(t => t.CCCustomerId == ccCustomer.ID).OrderBy(t => t.Id).Last();

                    if ((int)(ccTransactionLatest.CCAvailbleBalance + purchaseAmount) > ccCustomer.CardLimit)
                    {
                        returnMessage = "Purchase declined. Not enough balance";
                    }
                    else
                    {
                        var CCTransactions = new CCTransactions
                        {
                            CCCustomerId = ccCustomer.ID,
                            CCAvailbleBalance = (int)(ccTransactionLatest.CCAvailbleBalance + purchaseAmount) 
                        };

                        _ccDBContext.Add(CCTransactions);
                        _ccDBContext.SaveChanges();

                        returnMessage = "Purchase sucessful.";
                    }
                }
                else
                {
                    //If no transactions, then check with CardLimit 
                    if (purchaseAmount < ccCustomer.CardLimit)
                    {
                        var CCTransactions = new CCTransactions
                        {
                            CCCustomerId = ccCustomer.ID,
                            CCAvailbleBalance = (int)(purchaseAmount)
                        };

                        _ccDBContext.Add(CCTransactions);
                        _ccDBContext.SaveChanges();

                        returnMessage = "Purchase sucessful.";
                    }
                    else
                    {
                        returnMessage = "Purchase is declined.";
                    }
               
                }
            }
            else
            {
                return returnMessage = "Invalid card";
            }

            return returnMessage;
        }

        public int ReturnMinimumPayment(string cardNumber, string lastName)
        {
            string returnMessage = string.Empty;
            var minPaymentRequired = 0;

            //GetCustomer by CardNumber and LastName
            var ccCustomer = _ccDBContext.CCCustomers.Where(t => t.CardNumber == Convert.ToInt32(cardNumber) && t.LastName == lastName).FirstOrDefault();

            if (ccCustomer != null)
            {
                //Get the lastest transaction by CustomerID
                var ccTransactionLatest = _ccDBContext.CCTransactions.Where(t => t.CCCustomerId == ccCustomer.ID).OrderBy(t => t.Id).Last();

                if (ccTransactionLatest != null)
                {
                    var minPayment = (ccTransactionLatest.CCAvailbleBalance / 100) * 20;
                    if (minPayment > 20)
                        return minPayment;
                    else
                        return 20;

                }
                else
                {
                    return minPaymentRequired;
                }
            }
            else
            {
                return minPaymentRequired;
            }


        }

    }   
}
