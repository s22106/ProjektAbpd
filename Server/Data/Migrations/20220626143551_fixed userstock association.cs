using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAbpd.Server.Data.Migrations
{
    public partial class fixeduserstockassociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Stock_StockId",
                table: "Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Stock_StockId",
                table: "Price",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Stock_StockId",
                table: "Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Stock_StockId",
                table: "Price",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
