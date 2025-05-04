using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArthritisPatientPortal.Migrations.LocalDb
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pin = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    HcpSpecialty = table.Column<string>(type: "TEXT", nullable: false),
                    Indication = table.Column<string>(type: "TEXT", nullable: false),
                    InsuranceType = table.Column<int>(type: "INTEGER", nullable: false),
                    EmailConsent = table.Column<bool>(type: "INTEGER", nullable: false),
                    TextConsent = table.Column<bool>(type: "INTEGER", nullable: false),
                    CopayCardId = table.Column<string>(type: "TEXT", nullable: true),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
