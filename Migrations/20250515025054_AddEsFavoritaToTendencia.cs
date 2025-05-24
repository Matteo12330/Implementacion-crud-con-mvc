using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteSpot.Migrations
{
    /// <inheritdoc />
    public partial class AddEsFavoritaToTendencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsFavorita",
                table: "Tendencias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsFavorita",
                table: "Tendencias");
        }
    }
}
