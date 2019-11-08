using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reviewer",
                table: "ShipperReviews",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reviewer",
                table: "CarrierReview",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewer",
                table: "ShipperReviews");

            migrationBuilder.DropColumn(
                name: "reviewer",
                table: "CarrierReview");
        }
    }
}
