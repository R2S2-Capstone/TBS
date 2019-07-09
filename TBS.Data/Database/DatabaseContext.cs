using Microsoft.EntityFrameworkCore;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Shipper;
using TBS.Data.Models.User;

namespace TBS.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<CarrierPost> CarrierPosts { get; set; }
        public DbSet<ShipperPost> ShipperPosts { get; set; }
    }
}
