using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WizardWares.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRarityToRarityIdInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "RarityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "RarityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "RarityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "RarityId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Products_RarityId",
                table: "Products",
                column: "RarityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Rarities_RarityId",
                table: "Products",
                column: "RarityId",
                principalTable: "Rarities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Rarities_RarityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_RarityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RarityId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Rarity",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rarity",
                value: "Inferior");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Rarity",
                value: "Inferior");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rarity",
                value: "Inferior");
        }
    }
}
