using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class imagespropertyaddedtoproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Projects",
                newName: "Image6");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Image6",
                table: "Projects",
                newName: "Image");
        }
    }
}
