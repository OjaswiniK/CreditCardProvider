using CreditCardProvider.Data;
using CreditCardProvider.Models.CCPurchase;
using CreditCardProvider.Models.Domain;
using CreditCardProvider.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CreditCardProvider.Controllers
{
    public class CCWithdrwalController : Controller
    {
        private readonly CCDBContext _ccDBContext;
        public CCWithdrwalController(CCDBContext ccDbContext)
        {
                _ccDBContext = ccDbContext;
        }

        [HttpGet]
        public IActionResult CreditCardWithdrwal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreditCardWithdrwal(WithdrwalRequest withdrawRequest)
        {
            PurchaseRequest purchaseRequest = new PurchaseRequest(_ccDBContext);
                        
            string creditCardNumber = withdrawRequest.CreditCardNumber;
            string lastName = withdrawRequest.LastName;
            int purchaseAmount = withdrawRequest.PurchaseAmount;
            withdrawRequest.ReturnMessage = string.Empty;

            withdrawRequest.ReturnMessage = purchaseRequest.AddPurchaseTranscation(creditCardNumber, lastName, purchaseAmount);
                                    
            return View("CreditCardWithdrwal");
        }

        [HttpPost]
        public IActionResult ListTransaction(ViewStatement viewStatement)
        {
            var creditCardNumber = viewStatement.CreditCardNumber;
            var lastName = viewStatement.LastName;

            var customer = new CCCustomer
            {
                CardNumber = Convert.ToInt32(viewStatement.CreditCardNumber),
                LastName = viewStatement.LastName
            };

            var ccCustomer = _ccDBContext.CCCustomers.Where(t => t.CardNumber == customer.CardNumber).FirstOrDefault(); // .Select(t => t.CardNumber).ToList();
            if (ccCustomer != null)
            {
                //ccCustomer.CardNumber = ccCustomer.CardNumber;
                var customerId = ccCustomer.ID;

                return RedirectToAction("ViewStatement", new { customerId });
            }
            else
                return RedirectToAction("ListTransaction");

            
          
        }

        [HttpGet]
        public IActionResult ListTransaction()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewStatement(int customerID)
        {  

            var ccTransaction = _ccDBContext.CCTransactions.ToList().Where(c => c.CCCustomerId == customerID).ToList();

            //Adding futher columns to customer statement

            //var ccListTransactions = _ccDBContext.CCTransactions.Join(_ccDBContext.CCCustomers,
            //     transaction => transaction.CCCustomerId,
            //     cust => cust.ID,
            //     (transaction, cust) => new
            //     {
            //         CardNumber = cust.CardNumber.ToString(),
            //         CustomerName = cust.FirstName + " " + cust.LastName,
            //         CardLimit = cust.CardLimit.ToString(),
            //         AvailableBalance = transaction.CCAvailbleBalance,
            //         CustomerId = cust.ID
            //     }).Where( t => t.CustomerId == customerID);
           
            
            return View(ccTransaction);
        }
    }
}



