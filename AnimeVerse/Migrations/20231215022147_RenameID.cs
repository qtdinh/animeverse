using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeVerse.Migrations
{
    /// <inheritdoc />
    public partial class RenameID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                table: "Series",           // Table name (without schema)
                name: "SeriesID",          // Old column name
                newName: "ID"               // New column name
            );
            migrationBuilder.RenameColumn(
                table: "Character",           // Table name (without schema)
                name: "CharacterID",          // Old column name
                newName: "ID"               // New column name
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                table: "Series",           // Table name (without schema)
                name: "ID",                // New column name from Up method
                newName: "SeriesID"        // Old column name to revert to
            );

            migrationBuilder.RenameColumn(
                table: "Character",        // Table name (without schema)
                name: "ID",                // New column name from Up method
                newName: "CharacterID"     // Old column name to revert to
            );
        }
    }
}
