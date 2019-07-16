using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class AddedContactToShipperPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DropoffContactId",
                table: "ShipperPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickupContactId",
                table: "ShipperPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Contact",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contact",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_DropoffContactId",
                table: "ShipperPosts",
                column: "DropoffContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPosts_PickupContactId",
                table: "ShipperPosts",
                column: "PickupContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperPosts_Contact_DropoffContactId",
                table: "ShipperPosts",
                column: "DropoffContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperPosts_Contact_PickupContactId",
                table: "ShipperPosts",
                column: "PickupContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipperPosts_Contact_DropoffContactId",
                table: "ShipperPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperPosts_Contact_PickupContactId",
                table: "ShipperPosts");

            migrationBuilder.DropIndex(
                name: "IX_ShipperPosts_DropoffContactId",
                table: "ShipperPosts");

            migrationBuilder.DropIndex(
                name: "IX_ShipperPosts_PickupContactId",
                table: "ShipperPosts");

            migrationBuilder.DropColumn(
                name: "DropoffContactId",
                table: "ShipperPosts");

            migrationBuilder.DropColumn(
                name: "PickupContactId",
                table: "ShipperPosts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Contact",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
