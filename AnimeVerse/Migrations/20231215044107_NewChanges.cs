using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeVerse.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genre",
                table: "Series");
            migrationBuilder.DropColumn(
                name: "description",
                table: "Character");
            migrationBuilder.AddColumn<string>(
                name: "Demographic",
                table: "Character",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.RenameTable(
               name: "Character",          // Old table name
               newName: "Characters"       // New table name
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Demographic",
               table: "Character");

            migrationBuilder.RenameTable(
                name: "Characters",       // New table name
                newName: "Character"      // Old table name
            );

            migrationBuilder.AddColumn<string>(
                name: "genre",
                table: "Series",
                type: "string",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Character",
                type: "string",
                nullable: false,
                defaultValue: "");
        }
    }
}
