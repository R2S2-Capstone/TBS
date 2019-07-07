using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class UpdatedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Condition",
                table: "PostedVehicle",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Carriers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarrierVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_VehicleId",
                table: "Carriers",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carriers_CarrierVehicle_VehicleId",
                table: "Carriers",
                column: "VehicleId",
                principalTable: "CarrierVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carriers_CarrierVehicle_VehicleId",
                table: "Carriers");

            migrationBuilder.DropTable(
                name: "CarrierVehicle");

            migrationBuilder.DropIndex(
                name: "IX_Carriers_VehicleId",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "PostedVehicle");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Carriers");
        }
    }
}
