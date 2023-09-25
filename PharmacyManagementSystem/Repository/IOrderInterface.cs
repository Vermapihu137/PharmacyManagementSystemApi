using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public interface IOrderInterface
    {
        int Buy(int id, int Quantity, int userId);
        bool Cancel(int id);
        int Checkout(int id, int Payment);
        List<Order> GetOrder(int userId);
    }
}
