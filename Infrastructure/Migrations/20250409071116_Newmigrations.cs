using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Newmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffManagers",
                columns: table => new
                {
                    StaffManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<long>(type: "bigint", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffManagers", x => x.StaffManagerId);
                });

            migrationBuilder.CreateTable(
                name: "StaffManagerCampuses",
                columns: table => new
                {
                    StaffManagerId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffManagerCampuses", x => new { x.StaffManagerId, x.CampusId });
                    table.ForeignKey(
                        name: "FK_StaffManagerCampuses_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffManagerCampuses_StaffManagers_StaffManagerId",
                        column: x => x.StaffManagerId,
                        principalTable: "StaffManagers",
                        principalColumn: "StaffManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffManagerCampuses_CampusId",
                table: "StaffManagerCampuses",
                column: "CampusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffManagerCampuses");

            migrationBuilder.DropTable(
                name: "StaffManagers");
        }
    }
}
