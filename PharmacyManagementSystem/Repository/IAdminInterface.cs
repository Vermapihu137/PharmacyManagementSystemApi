using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public class IAdminInterface : IAdmin
    {
        private List<Admin> Admins = new List<Admin>
        {
            new Admin { AdminId = 1, PhoneNo = "9808544862", Password = "9808544862" }
        };
        public Admin AuthenticateAdmin(string phoneNo, string password)
        {

            return Admins.SingleOrDefault(x => x.PhoneNo == phoneNo && x.Password == password);
        }
    }
}
