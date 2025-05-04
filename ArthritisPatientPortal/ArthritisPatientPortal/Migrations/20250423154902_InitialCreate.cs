using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArthritisPatientPortal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HcpSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceType = table.Column<int>(type: "int", nullable: false),
                    EmailConsent = table.Column<bool>(type: "bit", nullable: false),
                    TextConsent = table.Column<bool>(type: "bit", nullable: false),
                    CopayCardId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CopayCards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BinNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PcnNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopayCards", x => x.id);
                    table.ForeignKey(
                        name: "FK_CopayCards_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopayCards_PatientId",
                table: "CopayCards",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopayCards");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
