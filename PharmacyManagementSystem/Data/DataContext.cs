using PharmacyManagementSystem.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PharmacyManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User2 { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
