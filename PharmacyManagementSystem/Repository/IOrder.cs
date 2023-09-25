using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public class IOrder : IOrderInterface
    {
        private DataContext context;
        private int drugid;

        public int TotalAmount { get; private set; }

        public IOrder(DataContext context)
        {
            this.context = context;
        }

        public int Buy(int id, int Quantity, int userId)
        {
            Drug medicine = context.Drugs.Find(id);
            if (medicine == null)
            {
                return 1;
            }
            else
            {
                if (medicine.Quantity >= Quantity)
                {
                    long TotalAmount = ((long)(medicine.Price * Quantity));
                    Order order = new Order
                    {
                        DrugId = id,
                        UserId = userId,
                        DrugName = medicine.DrugName,
                        is_verified = false,
                        is_picked_up = false,
                        Date_Time = DateTime.Now,
                        Total_Amount = (int)TotalAmount,
                        Payment_Status = "Payment Pending",
                        QuantityBooked = Quantity

                    };
                    context.orders.Add(order);
                    context.SaveChanges();
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        public List<Order> GetOrder(int userId)
        {
            var bookings = context.orders.Where(u => u.UserId == userId);
            return bookings.ToList();
        }
        public bool Cancel(int id)
        {
            Order order = context.orders.Find(id);
            if (order == null)
            {
                return false;
            }
            else
            {
                var medicine = context.Drugs.Find(order.DrugId);
                context.orders.Remove(order);
                context.SaveChanges();
                medicine.Quantity += order.QuantityBooked;
                context.SaveChanges();
                return true;
            }
        }
        public int Checkout(int id, int Payment)
        {
            var order = context.orders.Find(id);
            if (order == null)
            {
                return 1;
            }
            else
            {
                var drug = context.Drugs.Find(order.DrugId);
                if (drug.Quantity >= order.QuantityBooked)
                {
                    if (Payment == order.Total_Amount)
                    {
                        order.Payment_Status = "Payment Successful";
                        drug.Quantity -= order.QuantityBooked;
                        context.SaveChanges();
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 4;
                }
            }
        }
    }
}
