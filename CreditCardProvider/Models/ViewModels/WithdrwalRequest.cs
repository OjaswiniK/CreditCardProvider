using System.ComponentModel.DataAnnotations;

namespace CreditCardProvider.Models.ViewModels
{
    public class WithdrwalRequest
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int PurchaseAmount { get; set; }

        public string ReturnMessage { get; set; }

    }
}
