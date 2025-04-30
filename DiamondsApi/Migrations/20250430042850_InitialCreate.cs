using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondsApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diamonds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Carat = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cut = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Clarity = table.Column<string>(type: "TEXT", nullable: false),
                    Depth = table.Column<decimal>(type: "TEXT", nullable: false),
                    Table = table.Column<decimal>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    X = table.Column<decimal>(type: "TEXT", nullable: false),
                    Y = table.Column<decimal>(type: "TEXT", nullable: false),
                    Z = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diamonds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diamonds");
        }
    }
}
