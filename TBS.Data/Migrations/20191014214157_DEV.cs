using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TBS.Data.Migrations
{
    public partial class DEV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "carrierReviewID",
                table: "ShipperBids",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "shipperReviewID",
                table: "ShipperBids",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "carrierReviewID",
                table: "CarrierBids",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "shipperReviewID",
                table: "CarrierBids",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarrierReview",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    CarrierId = table.Column<byte[]>(nullable: true),
                    rating = table.Column<int>(nullable: false),
                    review = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierReview", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarrierReview_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipperReviews",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    shipperId = table.Column<byte[]>(nullable: true),
                    rating = table.Column<int>(nullable: false),
                    review = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShipperReviews_Shippers_shipperId",
                        column: x => x.shipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperBids_carrierReviewID",
                table: "ShipperBids",
                column: "carrierReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperBids_shipperReviewID",
                table: "ShipperBids",
                column: "shipperReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_carrierReviewID",
                table: "CarrierBids",
                column: "carrierReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierBids_shipperReviewID",
                table: "CarrierBids",
                column: "shipperReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierReview_CarrierId",
                table: "CarrierReview",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperReviews_shipperId",
                table: "ShipperReviews",
                column: "shipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrierBids_CarrierReview_carrierReviewID",
                table: "CarrierBids",
                column: "carrierReviewID",
                principalTable: "CarrierReview",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrierBids_ShipperReviews_shipperReviewID",
                table: "CarrierBids",
                column: "shipperReviewID",
                principalTable: "ShipperReviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperBids_CarrierReview_carrierReviewID",
                table: "ShipperBids",
                column: "carrierReviewID",
                principalTable: "CarrierReview",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperBids_ShipperReviews_shipperReviewID",
                table: "ShipperBids",
                column: "shipperReviewID",
                principalTable: "ShipperReviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrierBids_CarrierReview_carrierReviewID",
                table: "CarrierBids");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrierBids_ShipperReviews_shipperReviewID",
                table: "CarrierBids");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperBids_CarrierReview_carrierReviewID",
                table: "ShipperBids");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperBids_ShipperReviews_shipperReviewID",
                table: "ShipperBids");

            migrationBuilder.DropTable(
                name: "CarrierReview");

            migrationBuilder.DropTable(
                name: "ShipperReviews");

            migrationBuilder.DropIndex(
                name: "IX_ShipperBids_carrierReviewID",
                table: "ShipperBids");

            migrationBuilder.DropIndex(
                name: "IX_ShipperBids_shipperReviewID",
                table: "ShipperBids");

            migrationBuilder.DropIndex(
                name: "IX_CarrierBids_carrierReviewID",
                table: "CarrierBids");

            migrationBuilder.DropIndex(
                name: "IX_CarrierBids_shipperReviewID",
                table: "CarrierBids");

            migrationBuilder.DropColumn(
                name: "carrierReviewID",
                table: "ShipperBids");

            migrationBuilder.DropColumn(
                name: "shipperReviewID",
                table: "ShipperBids");

            migrationBuilder.DropColumn(
                name: "carrierReviewID",
                table: "CarrierBids");

            migrationBuilder.DropColumn(
                name: "shipperReviewID",
                table: "CarrierBids");
        }
    }
}
