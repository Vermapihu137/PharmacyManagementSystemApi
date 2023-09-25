using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Model
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public int UserId { get; set; }
        public int DrugId { get; set; }
        public int AdminId { get; set; }
        public string DrugName { get; set; }
        public bool is_verified { get; set; }
        public bool is_picked_up { get; set; }
        public DateTime Date_Time { get; set; }
        public string Payment_Status { get; set; }
        public int Total_Amount { get; set; }
        public int QuantityBooked { get; set; }
    }
}
