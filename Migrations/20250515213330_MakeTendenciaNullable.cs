using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteSpot.Migrations
{
    /// <inheritdoc />
    public partial class MakeTendenciaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tendencias_TendenciaId",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "TendenciaId",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tendencias_TendenciaId",
                table: "Productos",
                column: "TendenciaId",
                principalTable: "Tendencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tendencias_TendenciaId",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "TendenciaId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tendencias_TendenciaId",
                table: "Productos",
                column: "TendenciaId",
                principalTable: "Tendencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
