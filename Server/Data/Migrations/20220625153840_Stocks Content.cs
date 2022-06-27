using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAbpd.Server.Data.Migrations
{
    public partial class StocksContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Ticker = table.Column<string>(type: "TEXT", nullable: false),
                    HomepageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryExchange = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Close = table.Column<double>(type: "REAL", nullable: false),
                    High = table.Column<double>(type: "REAL", nullable: false),
                    Low = table.Column<double>(type: "REAL", nullable: false),
                    Open = table.Column<double>(type: "REAL", nullable: false),
                    Volume = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => new { x.StockId, x.Date });
                    table.ForeignKey(
                        name: "FK_Price_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStocks",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    StockId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStocks", x => new { x.StockId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserStocks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStocks_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStocks_UserId",
                table: "UserStocks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "UserStocks");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
