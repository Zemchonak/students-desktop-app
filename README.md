# students-desktop-app

Перед запуском не забудьте добавить корректную connection string для бд
в src\StudentsManagement.DesktopApp\Program.cs, сразу после строки 34 (services.AddSingleton(mapper);)
добавить строковую переменную 
var connectionString = "connection string content";
