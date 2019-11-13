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
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("TBS.Data.Models.Bids.Carrier.CarrierBid", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<double>("BidAmount");

                    b.Property<int>("BidStatus");

                    b.Property<DateTime>("DateBidPlaced");

                    b.Property<DateTime>("DropoffDate");

                    b.Property<byte[]>("DropoffLocationId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("PickupDate");

                    b.Property<byte[]>("PickupLocationId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("PostId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("ShipperId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("VehicleId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("carrierReviewID")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("shipperReviewID")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("DropoffLocationId");

                    b.HasIndex("PickupLocationId");

                    b.HasIndex("PostId");

                    b.HasIndex("ShipperId");

                    b.HasIndex("VehicleId");

                    b.HasIndex("carrierReviewID");

                    b.HasIndex("shipperReviewID");

                    b.ToTable("CarrierBids");
                });

            modelBuilder.Entity("TBS.Data.Models.Bids.Shipper.ShipperBid", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<double>("BidAmount");

                    b.Property<int>("BidStatus");

                    b.Property<byte[]>("CarrierId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("DateBidPlaced");

                    b.Property<byte[]>("PostId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("carrierReviewID")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("shipperReviewID")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("PostId");

                    b.HasIndex("carrierReviewID");

                    b.HasIndex("shipperReviewID");

                    b.ToTable("ShipperBids");
                });

            modelBuilder.Entity("TBS.Data.Models.General.Address", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

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

            modelBuilder.Entity("TBS.Data.Models.Posts.Carrier.CarrierPost", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("CarrierId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

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

            modelBuilder.Entity("TBS.Data.Models.Posts.Shipper.ShipperPost", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("DatePosted");

                    b.Property<byte[]>("DropoffContactId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("DropoffDate");

                    b.Property<byte[]>("DropoffLocationId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("PickupContactId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("PickupDate");

                    b.Property<byte[]>("PickupLocationId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<int>("PostStatus");

                    b.Property<byte[]>("ShipperId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<decimal>("StartingBid");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<byte[]>("VehicleId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("DropoffContactId");

                    b.HasIndex("DropoffLocationId");

                    b.HasIndex("PickupContactId");

                    b.HasIndex("PickupLocationId");

                    b.HasIndex("ShipperId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ShipperPosts");
                });

            modelBuilder.Entity("TBS.Data.Models.Reviews.CarrierReviews", b =>
                {
                    b.Property<byte[]>("ID")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("CarrierId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("date");

                    b.Property<int>("rating");

                    b.Property<string>("review");

                    b.Property<string>("reviewer")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("CarrierId");

                    b.ToTable("CarrierReview");
                });

            modelBuilder.Entity("TBS.Data.Models.Reviews.ShipperReview", b =>
                {
                    b.Property<byte[]>("ID")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("date");

                    b.Property<int>("rating");

                    b.Property<string>("review");

                    b.Property<string>("reviewer")
                        .IsRequired();

                    b.Property<byte[]>("shipperId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("ID");

                    b.HasIndex("shipperId");

                    b.ToTable("ShipperReviews");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Carrier", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("CompanyId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("DriversLicense")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserFirebaseId")
                        .IsRequired();

                    b.Property<byte[]>("VehicleId")
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Information.Company", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("AddressId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("ContactId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Information.Contact", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Shipper", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<byte[]>("CompanyId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

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
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

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
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

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

            modelBuilder.Entity("TBS.Data.Models.Bids.Carrier.CarrierBid", b =>
                {
                    b.HasOne("TBS.Data.Models.General.Address", "DropoffLocation")
                        .WithMany()
                        .HasForeignKey("DropoffLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.General.Address", "PickupLocation")
                        .WithMany()
                        .HasForeignKey("PickupLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Posts.Carrier.CarrierPost", "Post")
                        .WithMany("Bids")
                        .HasForeignKey("PostId");

                    b.HasOne("TBS.Data.Models.Users.Shipper", "Shipper")
                        .WithMany()
                        .HasForeignKey("ShipperId");

                    b.HasOne("TBS.Data.Models.Vehicle.PostedVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Reviews.CarrierReviews", "carrierReview")
                        .WithMany()
                        .HasForeignKey("carrierReviewID");

                    b.HasOne("TBS.Data.Models.Reviews.ShipperReview", "shipperReview")
                        .WithMany()
                        .HasForeignKey("shipperReviewID");
                });

            modelBuilder.Entity("TBS.Data.Models.Bids.Shipper.ShipperBid", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId");

                    b.HasOne("TBS.Data.Models.Posts.Shipper.ShipperPost", "Post")
                        .WithMany("Bids")
                        .HasForeignKey("PostId");

                    b.HasOne("TBS.Data.Models.Reviews.CarrierReviews", "carrierReview")
                        .WithMany()
                        .HasForeignKey("carrierReviewID");

                    b.HasOne("TBS.Data.Models.Reviews.ShipperReview", "shipperReview")
                        .WithMany()
                        .HasForeignKey("shipperReviewID");
                });

            modelBuilder.Entity("TBS.Data.Models.Posts.Carrier.CarrierPost", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Carrier", "Carrier")
                        .WithMany("Posts")
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("TBS.Data.Models.Posts.Shipper.ShipperPost", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Information.Contact", "DropoffContact")
                        .WithMany()
                        .HasForeignKey("DropoffContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.General.Address", "DropoffLocation")
                        .WithMany()
                        .HasForeignKey("DropoffLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Users.Information.Contact", "PickupContact")
                        .WithMany()
                        .HasForeignKey("PickupContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.General.Address", "PickupLocation")
                        .WithMany()
                        .HasForeignKey("PickupLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Users.Shipper", "Shipper")
                        .WithMany("Posts")
                        .HasForeignKey("ShipperId");

                    b.HasOne("TBS.Data.Models.Vehicle.PostedVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TBS.Data.Models.Reviews.CarrierReviews", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Carrier", "Carrier")
                        .WithMany("Reviews")
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("TBS.Data.Models.Reviews.ShipperReview", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Shipper", "shipper")
                        .WithMany("Reviews")
                        .HasForeignKey("shipperId");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Carrier", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Information.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Vehicle.Carrier.CarrierVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Information.Company", b =>
                {
                    b.HasOne("TBS.Data.Models.General.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TBS.Data.Models.Users.Information.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TBS.Data.Models.Users.Shipper", b =>
                {
                    b.HasOne("TBS.Data.Models.Users.Information.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
