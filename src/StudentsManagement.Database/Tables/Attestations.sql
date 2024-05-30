﻿CREATE TABLE [dbo].[Attestations]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TeacherId UNIQUEIDENTIFIER NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    CurriculumUnitId UNIQUEIDENTIFIER NOT NULL,
    Date DATETIME NOT NULL
    FOREIGN KEY (TeacherId) REFERENCES Users(Id),
    FOREIGN KEY (GroupId) REFERENCES Groups(Id),
    FOREIGN KEY (CurriculumUnitId) REFERENCES CurriculumUnits(Id),
)
