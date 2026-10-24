using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalUrlHashColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_UrlMappings_ShortCode",
                table: "UrlMappings",
                newName: "UX_UrlMappings_ShortCode");

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrlHash",
                table: "UrlMappings",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalUrlHash",
                table: "UrlMappings");

            migrationBuilder.RenameIndex(
                name: "UX_UrlMappings_ShortCode",
                table: "UrlMappings",
                newName: "IX_UrlMappings_ShortCode");
        }
    }
}
