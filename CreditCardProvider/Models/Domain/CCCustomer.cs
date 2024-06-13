using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CreditCardProvider.Models.Domain
{
    public class CCCustomer
    {

        public int ID { get; set; }
        
        public int CardNumber { get; set; }
        
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address1 { get; set; }
        public virtual CardType CardTypeId { get; set; }
        public int CardLimit { get; set; }            
        public virtual ICollection<CCTransactions> Transactions { get; set; }


    }
}
