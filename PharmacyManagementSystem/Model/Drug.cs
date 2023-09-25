using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Model
{
    public class Drug
    {
        [Key]
        public int DrugId { get; set; }

        [Required(ErrorMessage = "Please enter the drug name")]
        public string DrugName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the expiry date")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please enter the quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the price")]
        public int Price { get; set; }
        public int Medicine_Available { get; set; }
    }
}
