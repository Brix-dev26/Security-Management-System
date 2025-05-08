using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    CampusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    campus_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.CampusId);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalIdCard = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    E_mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passport_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBlacklisted = table.Column<bool>(type: "bit", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorId);
                });

            migrationBuilder.CreateTable(
                name: "Gate",
                columns: table => new
                {
                    GateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gate_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gate", x => x.GateId);
                    table.ForeignKey(
                        name: "FK_Gate_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId");
                });

            migrationBuilder.CreateTable(
                name: "SecurityStaffs",
                columns: table => new
                {
                    Sec_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalIdCard = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<long>(type: "bigint", nullable: false),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityStaffs", x => x.Sec_ID);
                    table.ForeignKey(
                        name: "FK_SecurityStaffs_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_SecurityStaffs_Gate_GateId",
                        column: x => x.GateId,
                        principalTable: "Gate",
                        principalColumn: "GateId");
                });

            migrationBuilder.CreateTable(
                name: "EmergencyEvents",
                columns: table => new
                {
                    Emerg_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sec_ID = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionStatus = table.Column<bool>(type: "bit", nullable: false),
                    CauseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfInvolvedPeople = table.Column<int>(type: "int", nullable: false),
                    ResolutionMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secutity_names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    involvedpeople_names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyEvents", x => x.Emerg_ID);
                    table.ForeignKey(
                        name: "FK_EmergencyEvents_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_EmergencyEvents_SecurityStaffs_Sec_ID",
                        column: x => x.Sec_ID,
                        principalTable: "SecurityStaffs",
                        principalColumn: "Sec_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    Visit_reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    GateId_exit = table.Column<int>(type: "int", nullable: true),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    Sec_ID = table.Column<int>(type: "int", nullable: false),
                    Sec_ID_Exit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_LogEntries_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "CampusId");
                    table.ForeignKey(
                        name: "FK_LogEntries_Gate_GateId",
                        column: x => x.GateId,
                        principalTable: "Gate",
                        principalColumn: "GateId");
                    table.ForeignKey(
                        name: "FK_LogEntries_SecurityStaffs_Sec_ID",
                        column: x => x.Sec_ID,
                        principalTable: "SecurityStaffs",
                        principalColumn: "Sec_ID");
                    table.ForeignKey(
                        name: "FK_LogEntries_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId");
                    table.ForeignKey(
                        name: "FK_LogEntries_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId");
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    PermitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sec_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.PermitId);
                    table.ForeignKey(
                        name: "FK_Permits_SecurityStaffs_Sec_ID",
                        column: x => x.Sec_ID,
                        principalTable: "SecurityStaffs",
                        principalColumn: "Sec_ID");
                    table.ForeignKey(
                        name: "FK_Permits_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permits_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId");
                });

            migrationBuilder.CreateTable(
                name: "InvolvedParties",
                columns: table => new
                {
                    InvolvedPartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    Emerg_ID = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sec_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolvedParties", x => x.InvolvedPartyId);
                    table.ForeignKey(
                        name: "FK_InvolvedParties_EmergencyEvents_Emerg_ID",
                        column: x => x.Emerg_ID,
                        principalTable: "EmergencyEvents",
                        principalColumn: "Emerg_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvolvedParties_SecurityStaffs_Sec_ID",
                        column: x => x.Sec_ID,
                        principalTable: "SecurityStaffs",
                        principalColumn: "Sec_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyEvents_CampusId",
                table: "EmergencyEvents",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyEvents_Sec_ID",
                table: "EmergencyEvents",
                column: "Sec_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Gate_CampusId",
                table: "Gate",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvedParties_Emerg_ID",
                table: "InvolvedParties",
                column: "Emerg_ID");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvedParties_Sec_ID",
                table: "InvolvedParties",
                column: "Sec_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_CampusId",
                table: "LogEntries",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_GateId",
                table: "LogEntries",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_Sec_ID",
                table: "LogEntries",
                column: "Sec_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_VehicleId",
                table: "LogEntries",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_VisitorId",
                table: "LogEntries",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_Sec_ID",
                table: "Permits",
                column: "Sec_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_VehicleId",
                table: "Permits",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_VisitorId",
                table: "Permits",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityStaffs_CampusId",
                table: "SecurityStaffs",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityStaffs_GateId",
                table: "SecurityStaffs",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VisitorId",
                table: "Vehicles",
                column: "VisitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvolvedParties");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropTable(
                name: "EmergencyEvents");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "SecurityStaffs");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Gate");

            migrationBuilder.DropTable(
                name: "Campus");
        }
    }
}
