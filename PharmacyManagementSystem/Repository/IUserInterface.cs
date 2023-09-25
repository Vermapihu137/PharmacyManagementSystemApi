using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public interface IUserInterface
    {
        void Add(User user);
        User Authenticate(string phoneNo, string password);
        List<User> GetUserList();
        User GetUser(int id);
        void Delete(int id);
        void Update(User user);

    }
}
