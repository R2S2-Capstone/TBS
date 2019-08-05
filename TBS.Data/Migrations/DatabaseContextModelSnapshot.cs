﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TBS.Data.Database;

namespace TBS.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("TBS.Data.Models.General.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("Province")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Bid.Carrier.CarrierBid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BidAmount");

                    b.Property<int>("BidStatus");

                    b.Property<DateTime>("DateBidPlaced");

                    b.Property<int?>("PostId");

                    b.Property<int?>("ShipperId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ShipperId");

                    b.ToTable("CarrierBids");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Bid.Shipper.ShipperBid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BidAmount");

                    b.Property<int>("BidStatus");

                    b.Property<int?>("CarrierId");

                    b.Property<DateTime>("DateBidPlaced");

                    b.Property<int?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("PostId");

                    b.ToTable("ShipperBids");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Carrier.CarrierPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarrierId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<DateTime>("DropoffDate");

                    b.Property<string>("DropoffLocation")
                        .IsRequired();

                    b.Property<DateTime>("PickupDate");

                    b.Property<string>("PickupLocation")
                        .IsRequired();

                    b.Property<int>("PostStatus");

                    b.Property<int>("SpacesAvailable");

                    b.Property<decimal>("StartingBid");

                    b.Property<int>("TrailerType");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("CarrierPosts");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Shipper.ShipperPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("DropoffContactId");

                    b.Property<DateTime>("DropoffDate");

                    b.Property<int>("DropoffLocationId");

                    b.Property<int>("PickupContactId");

                    b.Property<DateTime>("PickupDate");

                    b.Property<int>("PickupLocationId");

                    b.Property<int>("PostStatus");

                    b.Property<int?>("ShipperId");

                    b.Property<decimal>("StartingBid");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("DropoffContactId");

                    b.HasIndex("DropoffLocationId");

                    b.HasIndex("PickupContactId");

                    b.HasIndex("PickupLocationId");

                    b.HasIndex("ShipperId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ShipperPosts");
                });

            modelBuilder.Entity("TBS.Data.Models.User.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("DriversLicense")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserFirebaseId")
                        .IsRequired();

                    b.Property<int?>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("TBS.Data.Models.User.Information.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("ContactId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TBS.Data.Models.User.Information.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("TBS.Data.Models.User.Shipper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("DealerNumber")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("RIN")
                        .IsRequired();

                    b.Property<string>("UserFirebaseId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Shippers");
                });

            modelBuilder.Entity("TBS.Data.Models.Vehicle.Carrier.CarrierVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Make")
                        .IsRequired();

                    b.Property<string>("Model")
                        .IsRequired();

                    b.Property<int>("TrailerType");

                    b.Property<string>("VIN")
                        .IsRequired();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("CarrierVehicle");
                });

            modelBuilder.Entity("TBS.Data.Models.Vehicle.PostedVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Condition");

                    b.Property<string>("Make")
                        .IsRequired();

                    b.Property<string>("Model")
                        .IsRequired();

                    b.Property<string>("VIN")
                        .IsRequired();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("PostedVehicle");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Bid.Carrier.CarrierBid", b =>
                {
                    b.HasOne("TBS.Data.Models.Post.Carrier.CarrierPost", "Post")
                        .WithMany("Bids")
                        .HasForeignKey("PostId");

                    b.HasOne("TBS.Data.Models.User.Shipper", "Shipper")
                        .WithMany()
                        .HasForeignKey("ShipperId");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Bid.Shipper.ShipperBid", b =>
                {
                    b.HasOne("TBS.Data.Models.User.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");

                    b.HasOne("TBS.Data.Models.Post.Shipper.ShipperPost", "Post")
                        .WithMany("Bids")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Carrier.CarrierPost", b =>
                {
                    b.HasOne("TBS.Data.Models.User.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TBS.Data.Models.Post.Shipper.ShipperPost", b =>
                {
                    b.HasOne("TBS.Data.Models.User.Information.Contact", "DropoffContact")
                        .WithMany()
                        .HasForeignKey("DropoffContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.General.Address", "DropoffLocation")
                        .WithMany()
                        .HasForeignKey("DropoffLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.User.Information.Contact", "PickupContact")
                        .WithMany()
                        .HasForeignKey("PickupContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.General.Address", "PickupLocation")
                        .WithMany()
                        .HasForeignKey("PickupLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.User.Shipper", "Shipper")
                        .WithMany()
                        .HasForeignKey("ShipperId");

                    b.HasOne("TBS.Data.Models.Vehicle.PostedVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TBS.Data.Models.User.Carrier", b =>
                {
                    b.HasOne("TBS.Data.Models.User.Information.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Vehicle.Carrier.CarrierVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("TBS.Data.Models.User.Information.Company", b =>
                {
                    b.HasOne("TBS.Data.Models.General.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.User.Information.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TBS.Data.Models.User.Shipper", b =>
                {
                    b.HasOne("TBS.Data.Models.User.Information.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
