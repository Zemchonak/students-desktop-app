CREATE TABLE [dbo].[Groups]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(10) NOT NULL,
    Cource INT NULL,
    Graduated BIT NOT NULL,
    EnrollYear INT NOT NULL,
    SpecialityId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (SpecialityId) REFERENCES Specialities(Id)
)
