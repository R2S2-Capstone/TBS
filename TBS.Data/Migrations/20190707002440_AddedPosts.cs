using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class AddedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RIN",
                table: "Shippers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DealerNumber",
                table: "Shippers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriversLicense",
                table: "Carriers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CarrierPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    PickupCity = table.Column<string>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    DropoffCity = table.Column<string>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false),
                    TrailerType = table.Column<int>(nullable: false),
                    SpacesAvailable = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PostStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostedVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Year = table.Column<int>(nullable: false),
                    Make = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    VIN = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostedVehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipperPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    VehicleId = table.Column<int>(nullable: false),
                    PickupLocationId = table.Column<int>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    DropoffLocationId = table.Column<int>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    PostStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Address_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_Address_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipperPosts_PostedVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "PostedVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_DropoffLocationId",
                table: "ShipperPosts",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_PickupLocationId",
                table: "ShipperPosts",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_VehicleId",
                table: "ShipperPosts",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierPosts");

            migrationBuilder.DropTable(
                name: "ShipperPosts");

            migrationBuilder.DropTable(
                name: "PostedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RIN",
                table: "Shippers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DealerNumber",
                table: "Shippers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DriversLicense",
                table: "Carriers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
