using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAbpd.Server.Data.Migrations
{
    public partial class Articleskeychanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockArticle_Article_ArticleId",
                table: "StockArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockArticle",
                table: "StockArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "StockArticle");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "ArticleUrl",
                table: "StockArticle",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockArticle",
                table: "StockArticle",
                columns: new[] { "ArticleUrl", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "ArticleUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleUrl",
                table: "Article",
                column: "ArticleUrl",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockArticle_Article_ArticleUrl",
                table: "StockArticle",
                column: "ArticleUrl",
                principalTable: "Article",
                principalColumn: "ArticleUrl",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockArticle_Article_ArticleUrl",
                table: "StockArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockArticle",
                table: "StockArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_ArticleUrl",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleUrl",
                table: "StockArticle");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "StockArticle",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Article",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockArticle",
                table: "StockArticle",
                columns: new[] { "ArticleId", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockArticle_Article_ArticleId",
                table: "StockArticle",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
