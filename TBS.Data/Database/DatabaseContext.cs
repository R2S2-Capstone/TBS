using Microsoft.EntityFrameworkCore;
using TBS.Data.Models.User.Account;

namespace TBS.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
    }
}
