using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public interface IAdmin
    {
        Admin AuthenticateAdmin(string PhoneNo, string password);
    }
}
