using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolioApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectUtilitiesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectUtilities",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UtilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUtilities", x => new { x.ProjectId, x.UtilityId });
                    table.ForeignKey(
                        name: "FK_ProjectUtilities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUtilities_Utilities_UtilityId",
                        column: x => x.UtilityId,
                        principalTable: "Utilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUtilities_UtilityId",
                table: "ProjectUtilities",
                column: "UtilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUtilities");
        }
    }
}
