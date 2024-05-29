/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

INSERT INTO Users (Id, Email, PasswordHash, FirstName, LastName, Role)
VALUES (N'0CAF4590-5AE3-4278-93CD-D4A7515065E4',
        N'admin@ya.ru',
        N'236fcd343c3b841fae820cf63719b17b7ebb0d9d883c911e4afcd9971b530fe7',
        N'Администратор', N'приложения',
        0);