﻿CREATE TABLE [dbo].[Marks]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    StudentId UNIQUEIDENTIFIER NOT NULL,
    AttestationId UNIQUEIDENTIFIER NOT NULL,
    NotAttended BIT NOT NULL,
    NotAllowed BIT NOT NULL,
    Value INT NULL
)
