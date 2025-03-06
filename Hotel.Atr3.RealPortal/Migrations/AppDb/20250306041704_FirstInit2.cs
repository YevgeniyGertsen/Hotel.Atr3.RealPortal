using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Atr3.RealPortal.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class FirstInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_languages",
                table: "languages");

            migrationBuilder.RenameTable(
                name: "languages",
                newName: "Language");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultCulture",
                table: "Language",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "IsDefaultCulture",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "languages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_languages",
                table: "languages",
                column: "Id");
        }
    }
}
