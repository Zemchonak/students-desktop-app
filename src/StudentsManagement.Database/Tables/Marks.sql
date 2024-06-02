CREATE TABLE [dbo].[Marks]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    StudentId UNIQUEIDENTIFIER NOT NULL,
    AttestationId UNIQUEIDENTIFIER NOT NULL,
    NotAttended BIT NULL,
    NotAllowed BIT NULL,
    Value INT NULL
    FOREIGN KEY (StudentId) REFERENCES Users(Id),
    FOREIGN KEY (AttestationId) REFERENCES Attestations(Id),
)
