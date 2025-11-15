using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProyect.Migrations
{
    /// <inheritdoc />
    public partial class AddFileUploadToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Products");
        }
    }
}
