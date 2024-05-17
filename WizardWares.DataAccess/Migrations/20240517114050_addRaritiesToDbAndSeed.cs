using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WizardWares.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addRaritiesToDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rarities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rarities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rarities",
                columns: new[] { "Id", "ColorCode", "Description", "Name", "ValueOrder" },
                values: new object[] { 1, "1111111", null, "Common", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rarities");
        }
    }
}
