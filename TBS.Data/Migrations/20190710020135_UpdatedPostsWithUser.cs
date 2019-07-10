using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class UpdatedPostsWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipperId",
                table: "ShipperPosts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "CarrierPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_ShipperId",
                table: "ShipperPosts",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierPosts_CarrierId",
                table: "CarrierPosts",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrierPosts_Carriers_CarrierId",
                table: "CarrierPosts",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperPosts_Shippers_ShipperId",
                table: "ShipperPosts",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrierPosts_Carriers_CarrierId",
                table: "CarrierPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperPosts_Shippers_ShipperId",
                table: "ShipperPosts");

            migrationBuilder.DropIndex(
                name: "IX_ShipperPosts_ShipperId",
                table: "ShipperPosts");

            migrationBuilder.DropIndex(
                name: "IX_CarrierPosts_CarrierId",
                table: "CarrierPosts");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "ShipperPosts");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "CarrierPosts");
        }
    }
}
