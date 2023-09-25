using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public class IUser : IUserInterface
    {
        private DataContext context;
        public IUser(DataContext context)
        {
            this.context = context;
        }
        public void Add(User user)
        {
            context.User2.Add(user);
            context.SaveChanges();
        }
        public User Authenticate(string PhoneNo, string Pass)
        {
            return context.User2.Where(u => u.PhoneNo.Equals(PhoneNo) && u.Password == Pass).FirstOrDefault();
        }
        public void Delete(int id)
        {
            var user = context.User2.Find(id);
            context.User2.Remove(user);
            context.SaveChanges();
        }

        public List<User> GetUserList()
        {
            return context.User2.ToList();
        }

        public User GetUser(int id)
        {
            var user1 = context.User2.Find(id);
            return user1;
        }
        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
