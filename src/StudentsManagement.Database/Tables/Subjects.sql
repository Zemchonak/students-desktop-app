﻿CREATE TABLE [dbo].[Subjects]
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ShortName NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255) NOT NULL
)
