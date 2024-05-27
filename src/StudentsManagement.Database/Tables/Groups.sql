CREATE TABLE [dbo].[Groups]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    SpecialityShortName NVARCHAR(255) NOT NULL,
    Cource INT NULL,
    Graduated BIT NOT NULL,
    Number INT NOT NULL,
    EnrollYear INT NOT NULL,
    SpecialityId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (SpecialityId) REFERENCES Specialities(Id)
)
