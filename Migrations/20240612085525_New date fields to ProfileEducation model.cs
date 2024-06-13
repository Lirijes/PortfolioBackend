using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class NewdatefieldstoProfileEducationmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tableExists = migrationBuilder.Sql("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PhoneNumberRequest') SELECT 1 ELSE SELECT 0").ToString() == "1";

            if (tableExists)
            {
                migrationBuilder.DropTable(
                    name: "PhoneNumberRequest");
            }

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "ProfileEducations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "ProfileEducations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProfileEducations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProfileEducations");

            migrationBuilder.CreateTable(
                name: "PhoneNumberRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberRequest", x => x.Id);
                });
        }
    }
}
