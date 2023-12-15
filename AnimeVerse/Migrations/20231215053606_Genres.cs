using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeVerse.Migrations
{
    /// <inheritdoc />
    public partial class Genres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Series",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Demographic",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "Demographic",
                table: "Series",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Character_SeriesID",
                table: "Characters",
                column: "SeriesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Series",
                table: "Characters",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Series",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Character_SeriesID",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Demographic",
                table: "Series");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Series",
                table: "Characters",
                column: "ID",
                principalTable: "Series",
                principalColumn: "ID");
        }
    }
}
