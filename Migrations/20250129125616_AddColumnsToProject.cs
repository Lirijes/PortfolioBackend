using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Todo",
                table: "Projects",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Improvements",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LessonsLearned",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Improvements",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LessonsLearned",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Projects",
                newName: "Todo");
        }
    }
}
