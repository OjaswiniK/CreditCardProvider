using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardProvider.Models.Domain
{
    public class CCTransactions
    {
        public int Id { get; set; }        
        public int CCAvailbleBalance { get; set; }

        [Required]
        [ForeignKey("CCCustomer")]
        public int CCCustomerId { get; set; }
        public virtual CCCustomer CCCustomers { get; set; }
    }
}
