using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaClinicalSuite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    AuditTrailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<TimeOnly>(type: "time", nullable: false, defaultValueSql: "(getutcdate())"),
                    ChangeDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditTra__41B2DDB36F2CE413", x => x.AuditTrailId);
                });

            migrationBuilder.CreateTable(
                name: "Investigator",
                columns: table => new
                {
                    InvestigatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Affiliation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Investig__A09546DB837CFE7F", x => x.InvestigatorId);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nchar", fixedLength: true, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GuardianInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__7227997E3187744A", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sites__B9DCB903D6784133", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "TrialTypes",
                columns: table => new
                {
                    TrialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrialTyp__59CAB3A78A1EBCDE", x => x.TrialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalReasons",
                columns: table => new
                {
                    WithdrawalReasonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WithdrawalReasonDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Withdraw__BE8C1525ACA37278", x => x.WithdrawalReasonID);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualVisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participant_Id",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId");
                });

            migrationBuilder.CreateTable(
                name: "Trials",
                columns: table => new
                {
                    TrialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sponsor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrialTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trials__EF1025A4C7A82768", x => x.TrialId);
                    table.ForeignKey(
                        name: "FK_Trial_TrialType",
                        column: x => x.TrialTypeId,
                        principalTable: "TrialTypes",
                        principalColumn: "TrialTypeId");
                });

            migrationBuilder.CreateTable(
                name: "AdverseEvents",
                columns: table => new
                {
                    AdverseEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    TrialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AdverseE__9AA4ACB0F54C4ED7", x => x.AdverseEventId);
                    table.ForeignKey(
                        name: "FK__AdverseEv__Parti__4BAC3F29",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId");
                    table.ForeignKey(
                        name: "FK__AdverseEv__Trial__4CA06362",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "TrialId");
                });

            migrationBuilder.CreateTable(
                name: "CaseReportforms",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrialId = table.Column<int>(type: "int", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CaseRepo__FB05B7BD5C1893E6", x => x.FormId);
                    table.ForeignKey(
                        name: "FK__CaseRepor__Trial__5BE2A6F2",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "TrialId");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrialId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EligibilityStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WithDrawalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WithDrawalReasonId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__7F6877FBF7A96B3D", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK__Enrollmen__Parti__45F365D3",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId");
                    table.ForeignKey(
                        name: "FK__Enrollmen__Trial__46E78A0C",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "TrialId");
                    table.ForeignKey(
                        name: "FK__Enrollmen__Withd__47DBAE45",
                        column: x => x.WithDrawalReasonId,
                        principalTable: "WithdrawalReasons",
                        principalColumn: "WithdrawalReasonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrialsInvestigators",
                columns: table => new
                {
                    TrialId = table.Column<int>(type: "int", nullable: false),
                    InvestigatorId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrialInv__451971C9ECD67464", x => new { x.TrialId, x.InvestigatorId });
                    table.ForeignKey(
                        name: "FK__TrialInve__Inves__52593CB8",
                        column: x => x.InvestigatorId,
                        principalTable: "Investigator",
                        principalColumn: "InvestigatorId");
                    table.ForeignKey(
                        name: "FK__TrialInve__Trial__5165187F",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "TrialId");
                });

            migrationBuilder.CreateTable(
                name: "TrialSite",
                columns: table => new
                {
                    TrialSiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrialTypeId = table.Column<int>(type: "int", nullable: false),
                    TrialId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialSite", x => x.TrialSiteId);
                    table.ForeignKey(
                        name: "FK_TrialSite_Trials_TrialId",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "TrialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrialSites",
                columns: table => new
                {
                    TrialID = table.Column<int>(type: "int", nullable: false),
                    SiteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrialSit__C48DEE34904186B5", x => new { x.TrialID, x.SiteID });
                    table.ForeignKey(
                        name: "FK__TrialSite__SiteI__5812160E",
                        column: x => x.SiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteId");
                    table.ForeignKey(
                        name: "FK__TrialSite__Trial__571DF1D5",
                        column: x => x.TrialID,
                        principalTable: "Trials",
                        principalColumn: "TrialId");
                });

            migrationBuilder.CreateTable(
                name: "DataCollectionFields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    FormsFormId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FieldOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DataColl__C8B6FF273546AC0B", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK__DataColle__FormI__60A75C0F",
                        column: x => x.FormsFormId,
                        principalTable: "CaseReportforms",
                        principalColumn: "FormId");
                });

            migrationBuilder.CreateTable(
                name: "ParticipantFormEntries",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormID = table.Column<int>(type: "int", nullable: false),
                    CaseReportformsFormId = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__F57BD2D7C4C40332", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK__Participa__FormI__656C112C",
                        column: x => x.CaseReportformsFormId,
                        principalTable: "CaseReportforms",
                        principalColumn: "FormId");
                });

            migrationBuilder.CreateTable(
                name: "ParticipantFieldData",
                columns: table => new
                {
                    DataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Particip__9D05305D2A166DB6", x => x.DataId);
                    table.ForeignKey(
                        name: "FK__Participa__Entry__68487DD7",
                        column: x => x.EntryId,
                        principalTable: "ParticipantFormEntries",
                        principalColumn: "EntryId");
                    table.ForeignKey(
                        name: "FK__Participa__Field__693CA210",
                        column: x => x.FieldId,
                        principalTable: "DataCollectionFields",
                        principalColumn: "FieldId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdverseEvents_ParticipantId",
                table: "AdverseEvents",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_AdverseEvents_TrialId",
                table: "AdverseEvents",
                column: "TrialId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseReportforms_TrialId",
                table: "CaseReportforms",
                column: "TrialId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCollectionFields_FormsFormId",
                table: "DataCollectionFields",
                column: "FormsFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ParticipantId",
                table: "Enrollments",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrialId",
                table: "Enrollments",
                column: "TrialId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_WithDrawalReasonId",
                table: "Enrollments",
                column: "WithDrawalReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFieldData_EntryId",
                table: "ParticipantFieldData",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFieldData_FieldId",
                table: "ParticipantFieldData",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantFormEntries_CaseReportformsFormId",
                table: "ParticipantFormEntries",
                column: "CaseReportformsFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Trials_TrialTypeId",
                table: "Trials",
                column: "TrialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialsInvestigators_InvestigatorId",
                table: "TrialsInvestigators",
                column: "InvestigatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialSite_TrialId",
                table: "TrialSite",
                column: "TrialId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialSites_SiteID",
                table: "TrialSites",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_ParticipantId",
                table: "Visit",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdverseEvents");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "ParticipantFieldData");

            migrationBuilder.DropTable(
                name: "TrialsInvestigators");

            migrationBuilder.DropTable(
                name: "TrialSite");

            migrationBuilder.DropTable(
                name: "TrialSites");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "WithdrawalReasons");

            migrationBuilder.DropTable(
                name: "ParticipantFormEntries");

            migrationBuilder.DropTable(
                name: "DataCollectionFields");

            migrationBuilder.DropTable(
                name: "Investigator");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "CaseReportforms");

            migrationBuilder.DropTable(
                name: "Trials");

            migrationBuilder.DropTable(
                name: "TrialTypes");
        }
    }
}
