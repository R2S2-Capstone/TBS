using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class AddedEmailFieldToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Shippers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Carriers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Shippers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Carriers");
        }
    }
}
