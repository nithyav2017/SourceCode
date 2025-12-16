IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AuditTrails] (
    [AuditTrailId] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [TableName] nvarchar(max) NOT NULL,
    [RecordId] nvarchar(max) NOT NULL,
    [Timestamp] time NOT NULL DEFAULT ((getutcdate())),
    [ChangeDetails] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__AuditTra__41B2DDB36F2CE413] PRIMARY KEY ([AuditTrailId])
);

CREATE TABLE [Investigator] (
    [InvestigatorId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [ContactInformation] nvarchar(max) NOT NULL,
    [Affiliation] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__Investig__A09546DB837CFE7F] PRIMARY KEY ([InvestigatorId])
);

CREATE TABLE [Participants] (
    [ParticipantId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Gender] nchar NOT NULL,
    [Address1] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [MedicalHistory] nvarchar(max) NOT NULL,
    [Allergies] nvarchar(max) NOT NULL,
    [BMI] decimal(18,2) NOT NULL,
    [GuardianInfo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__Particip__7227997E3187744A] PRIMARY KEY ([ParticipantId])
);

CREATE TABLE [Sites] (
    [SiteId] int NOT NULL IDENTITY,
    [SiteName] nvarchar(max) NOT NULL,
    [Location] nvarchar(max) NOT NULL,
    [ContactInformation] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK__Sites__B9DCB903D6784133] PRIMARY KEY ([SiteId])
);

CREATE TABLE [TrialTypes] (
    [TrialTypeId] int NOT NULL IDENTITY,
    [TypeName] nvarchar(max) NOT NULL,
    [Phase] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__TrialTyp__59CAB3A78A1EBCDE] PRIMARY KEY ([TrialTypeId])
);

CREATE TABLE [WithdrawalReasons] (
    [WithdrawalReasonID] int NOT NULL IDENTITY,
    [WithdrawalReasonDesc] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__Withdraw__BE8C1525ACA37278] PRIMARY KEY ([WithdrawalReasonID])
);

CREATE TABLE [Visit] (
    [Id] int NOT NULL IDENTITY,
    [ParticipantId] int NOT NULL,
    [ScheduledDate] datetime2 NOT NULL,
    [ActualVisitDate] datetime2 NULL,
    [VisitType] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Visit] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Participant_Id] FOREIGN KEY ([ParticipantId]) REFERENCES [Participants] ([ParticipantId])
);

CREATE TABLE [Trials] (
    [TrialId] int NOT NULL IDENTITY,
    [title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Phase] nvarchar(max) NOT NULL,
    [Sponsor] nvarchar(max) NOT NULL,
    [TrialTypeId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK__Trials__EF1025A4C7A82768] PRIMARY KEY ([TrialId]),
    CONSTRAINT [FK_Trial_TrialType] FOREIGN KEY ([TrialTypeId]) REFERENCES [TrialTypes] ([TrialTypeId])
);

CREATE TABLE [AdverseEvents] (
    [AdverseEventId] int NOT NULL IDENTITY,
    [EventDate] date NOT NULL,
    [Severity] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Outcome] nvarchar(max) NOT NULL,
    [ParticipantId] int NOT NULL,
    [TrialId] int NOT NULL,
    CONSTRAINT [PK__AdverseE__9AA4ACB0F54C4ED7] PRIMARY KEY ([AdverseEventId]),
    CONSTRAINT [FK__AdverseEv__Parti__4BAC3F29] FOREIGN KEY ([ParticipantId]) REFERENCES [Participants] ([ParticipantId]),
    CONSTRAINT [FK__AdverseEv__Trial__4CA06362] FOREIGN KEY ([TrialId]) REFERENCES [Trials] ([TrialId])
);

CREATE TABLE [CaseReportforms] (
    [FormId] int NOT NULL IDENTITY,
    [TrialId] int NOT NULL,
    [FormName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedOn] datetime2 NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__CaseRepo__FB05B7BD5C1893E6] PRIMARY KEY ([FormId]),
    CONSTRAINT [FK__CaseRepor__Trial__5BE2A6F2] FOREIGN KEY ([TrialId]) REFERENCES [Trials] ([TrialId])
);

CREATE TABLE [Enrollments] (
    [EnrollmentId] int NOT NULL IDENTITY,
    [TrialId] int NOT NULL,
    [EnrollmentDate] date NOT NULL,
    [EligibilityStatus] nvarchar(max) NOT NULL,
    [WithDrawalDate] datetime2 NOT NULL,
    [WithDrawalReasonId] int NOT NULL,
    [ParticipantId] int NOT NULL,
    CONSTRAINT [PK__Enrollme__7F6877FBF7A96B3D] PRIMARY KEY ([EnrollmentId]),
    CONSTRAINT [FK__Enrollmen__Parti__45F365D3] FOREIGN KEY ([ParticipantId]) REFERENCES [Participants] ([ParticipantId]),
    CONSTRAINT [FK__Enrollmen__Trial__46E78A0C] FOREIGN KEY ([TrialId]) REFERENCES [Trials] ([TrialId]),
    CONSTRAINT [FK__Enrollmen__Withd__47DBAE45] FOREIGN KEY ([WithDrawalReasonId]) REFERENCES [WithdrawalReasons] ([WithdrawalReasonID]) ON DELETE CASCADE
);

CREATE TABLE [TrialsInvestigators] (
    [TrialId] int NOT NULL,
    [InvestigatorId] int NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__TrialInv__451971C9ECD67464] PRIMARY KEY ([TrialId], [InvestigatorId]),
    CONSTRAINT [FK__TrialInve__Inves__52593CB8] FOREIGN KEY ([InvestigatorId]) REFERENCES [Investigator] ([InvestigatorId]),
    CONSTRAINT [FK__TrialInve__Trial__5165187F] FOREIGN KEY ([TrialId]) REFERENCES [Trials] ([TrialId])
);

CREATE TABLE [TrialSite] (
    [TrialSiteId] int NOT NULL IDENTITY,
    [TrialTypeId] int NOT NULL,
    [TrialId] int NOT NULL,
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_TrialSite] PRIMARY KEY ([TrialSiteId]),
    CONSTRAINT [FK_TrialSite_Trials_TrialId] FOREIGN KEY ([TrialId]) REFERENCES [Trials] ([TrialId]) ON DELETE CASCADE
);

CREATE TABLE [TrialSites] (
    [TrialID] int NOT NULL,
    [SiteID] int NOT NULL,
    CONSTRAINT [PK__TrialSit__C48DEE34904186B5] PRIMARY KEY ([TrialID], [SiteID]),
    CONSTRAINT [FK__TrialSite__SiteI__5812160E] FOREIGN KEY ([SiteID]) REFERENCES [Sites] ([SiteId]),
    CONSTRAINT [FK__TrialSite__Trial__571DF1D5] FOREIGN KEY ([TrialID]) REFERENCES [Trials] ([TrialId])
);

CREATE TABLE [DataCollectionFields] (
    [FieldId] int NOT NULL IDENTITY,
    [FormId] int NOT NULL,
    [FormsFormId] int NOT NULL,
    [FieldName] nvarchar(max) NOT NULL,
    [FieldType] nvarchar(max) NOT NULL,
    [IsRequired] bit NOT NULL DEFAULT CAST(0 AS bit),
    [FieldOrder] int NOT NULL,
    CONSTRAINT [PK__DataColl__C8B6FF273546AC0B] PRIMARY KEY ([FieldId]),
    CONSTRAINT [FK__DataColle__FormI__60A75C0F] FOREIGN KEY ([FormsFormId]) REFERENCES [CaseReportforms] ([FormId])
);

CREATE TABLE [ParticipantFormEntries] (
    [EntryId] int NOT NULL IDENTITY,
    [FormID] int NOT NULL,
    [CaseReportformsFormId] int NOT NULL,
    [EntryDate] datetime2 NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Particip__F57BD2D7C4C40332] PRIMARY KEY ([EntryId]),
    CONSTRAINT [FK__Participa__FormI__656C112C] FOREIGN KEY ([CaseReportformsFormId]) REFERENCES [CaseReportforms] ([FormId])
);

CREATE TABLE [ParticipantFieldData] (
    [DataId] int NOT NULL IDENTITY,
    [EntryId] int NOT NULL,
    [FieldId] int NOT NULL,
    [FieldValue] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__Particip__9D05305D2A166DB6] PRIMARY KEY ([DataId]),
    CONSTRAINT [FK__Participa__Entry__68487DD7] FOREIGN KEY ([EntryId]) REFERENCES [ParticipantFormEntries] ([EntryId]),
    CONSTRAINT [FK__Participa__Field__693CA210] FOREIGN KEY ([FieldId]) REFERENCES [DataCollectionFields] ([FieldId])
);

CREATE INDEX [IX_AdverseEvents_ParticipantId] ON [AdverseEvents] ([ParticipantId]);

CREATE INDEX [IX_AdverseEvents_TrialId] ON [AdverseEvents] ([TrialId]);

CREATE INDEX [IX_CaseReportforms_TrialId] ON [CaseReportforms] ([TrialId]);

CREATE INDEX [IX_DataCollectionFields_FormsFormId] ON [DataCollectionFields] ([FormsFormId]);

CREATE INDEX [IX_Enrollments_ParticipantId] ON [Enrollments] ([ParticipantId]);

CREATE INDEX [IX_Enrollments_TrialId] ON [Enrollments] ([TrialId]);

CREATE INDEX [IX_Enrollments_WithDrawalReasonId] ON [Enrollments] ([WithDrawalReasonId]);

CREATE INDEX [IX_ParticipantFieldData_EntryId] ON [ParticipantFieldData] ([EntryId]);

CREATE INDEX [IX_ParticipantFieldData_FieldId] ON [ParticipantFieldData] ([FieldId]);

CREATE INDEX [IX_ParticipantFormEntries_CaseReportformsFormId] ON [ParticipantFormEntries] ([CaseReportformsFormId]);

CREATE INDEX [IX_Trials_TrialTypeId] ON [Trials] ([TrialTypeId]);

CREATE INDEX [IX_TrialsInvestigators_InvestigatorId] ON [TrialsInvestigators] ([InvestigatorId]);

CREATE INDEX [IX_TrialSite_TrialId] ON [TrialSite] ([TrialId]);

CREATE INDEX [IX_TrialSites_SiteID] ON [TrialSites] ([SiteID]);

CREATE INDEX [IX_Visit_ParticipantId] ON [Visit] ([ParticipantId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250628135111_InitialCreate', N'9.0.5');

COMMIT;
GO

