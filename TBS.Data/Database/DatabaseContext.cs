using Microsoft.EntityFrameworkCore;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Shipper;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Posts.Shipper;
using TBS.Data.Models.Reviews;
using TBS.Data.Models.Users;

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
        public DbSet<CarrierBid> CarrierBids { get; set; }
        public DbSet<ShipperPost> ShipperPosts { get; set; }
        public DbSet<ShipperBid> ShipperBids { get; set; }
        public DbSet<ShipperReview> ShipperReviews { get; set; }
        public DbSet<CarrierReviews> CarrierReview { get; set; }
    }
}
