﻿CREATE TABLE [dbo].[RetakeResults]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    StudentId UNIQUEIDENTIFIER NOT NULL,
    AttestationId UNIQUEIDENTIFIER NOT NULL,
    Value INT NULL,
    Date DATETIME NOT NULL
    FOREIGN KEY (StudentId) REFERENCES Users(Id),
    FOREIGN KEY (AttestationId) REFERENCES Attestations(Id),
)
