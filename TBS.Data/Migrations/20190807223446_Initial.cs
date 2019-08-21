using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    AddressLine = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Province = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierVehicle",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    VIN = table.Column<string>(nullable: false),
                    TrailerType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierVehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostedVehicle",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    VIN = table.Column<string>(nullable: false),
                    Condition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostedVehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AddressId = table.Column<byte[]>(nullable: false),
                    ContactId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    UserFirebaseId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CompanyId = table.Column<byte[]>(nullable: false),
                    DriversLicense = table.Column<string>(nullable: false),
                    VehicleId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carriers_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carriers_CarrierVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "CarrierVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UserFirebaseId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CompanyId = table.Column<byte[]>(nullable: false),
                    RIN = table.Column<string>(nullable: false),
                    DealerNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippers_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierPosts",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CarrierId = table.Column<byte[]>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    PickupLocation = table.Column<string>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    DropoffLocation = table.Column<string>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false),
                    TrailerType = table.Column<int>(nullable: false),
                    SpacesAvailable = table.Column<int>(nullable: false),
                    StartingBid = table.Column<decimal>(nullable: false),
                    PostStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierPosts_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipperPosts",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    ShipperId = table.Column<byte[]>(nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    VehicleId = table.Column<byte[]>(nullable: false),
                    PickupLocationId = table.Column<byte[]>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    PickupContactId = table.Column<byte[]>(nullable: false),
                    DropoffLocationId = table.Column<byte[]>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false),
                    DropoffContactId = table.Column<byte[]>(nullable: false),
                    StartingBid = table.Column<decimal>(nullable: false),
                    PostStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Contact_DropoffContactId",
                        column: x => x.DropoffContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Address_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Contact_PickupContactId",
                        column: x => x.PickupContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Address_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_PostedVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "PostedVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierBids",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    ShipperId = table.Column<byte[]>(nullable: true),
                    PostId = table.Column<byte[]>(nullable: true),
                    VehicleId = table.Column<byte[]>(nullable: false),
                    PickupLocationId = table.Column<byte[]>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    DropoffLocationId = table.Column<byte[]>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false),
                    BidAmount = table.Column<double>(nullable: false),
                    DateBidPlaced = table.Column<DateTime>(nullable: false),
                    BidStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierBids_Address_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierBids_Address_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierBids_CarrierPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "CarrierPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrierBids_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrierBids_PostedVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "PostedVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipperBids",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CarrierId = table.Column<byte[]>(nullable: true),
                    PostId = table.Column<byte[]>(nullable: true),
                    BidAmount = table.Column<double>(nullable: false),
                    DateBidPlaced = table.Column<DateTime>(nullable: false),
                    BidStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipperBids_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipperBids_ShipperPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "ShipperPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_DropoffLocationId",
                table: "CarrierBids",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_PickupLocationId",
                table: "CarrierBids",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_PostId",
                table: "CarrierBids",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_ShipperId",
                table: "CarrierBids",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_VehicleId",
                table: "CarrierBids",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierPosts_CarrierId",
                table: "CarrierPosts",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_CompanyId",
                table: "Carriers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_VehicleId",
                table: "Carriers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AddressId",
                table: "Company",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ContactId",
                table: "Company",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperBids_CarrierId",
                table: "ShipperBids",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperBids_PostId",
                table: "ShipperBids",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_DropoffContactId",
                table: "ShipperPosts",
                column: "DropoffContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_DropoffLocationId",
                table: "ShipperPosts",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_PickupContactId",
                table: "ShipperPosts",
                column: "PickupContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_PickupLocationId",
                table: "ShipperPosts",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_ShipperId",
                table: "ShipperPosts",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_VehicleId",
                table: "ShipperPosts",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_CompanyId",
                table: "Shippers",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierBids");

            migrationBuilder.DropTable(
                name: "ShipperBids");

            migrationBuilder.DropTable(
                name: "CarrierPosts");

            migrationBuilder.DropTable(
                name: "ShipperPosts");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "PostedVehicle");

            migrationBuilder.DropTable(
                name: "CarrierVehicle");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
