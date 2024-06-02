using StudentsManagement.Common.Enums;
using System.Collections.Generic;

namespace StudentsManagement.DesktopApp.Common
{
    public static class AppLocalization
    {
        public const string Yes = "Да";
        public const string No = "Нет";

        public static string DatabaseExceptionTitle = "Ошибка с базой данных. Пожалуйста, сообщите администратору!";

        public static string ErrorMessageText = "Ошибка";
        public static string SelectSomethingMessageText = "Не выбрана строка в таблице. Пожалуйста, сделайте выбор и повторите попытку!";
        public static string SelectDropdownSomethingMessageText = "Пожалуйста, сделайте выбор в выпадающем списке и повторите попытку!";

        public static string LoginButtonText = "Войти в учётную запись";
        public static string ProfileButtonText = "Личный кабинет";
        public static string NotFilledInMessageText = "Не заполнено поле ";
        public static string WelcomeMessageText = "Рады приветствовать вас, ";
        public static string SignedInAsText = "Вы вошли как ";

        public static string AddWorkTypeForm = "Добавление вида работ";
        public static string UpdateWorkTypeForm = "Обновление вида работ";
        public static string AddSpecialityForm = "Добавление специальности";
        public static string UpdateSpecialityForm = "Обновление специальности";
        public static string AddSubjectForm = "Добавление уч. предмета";
        public static string UpdateSubjectForm = "Обновление уч. предмета";
        public static string AddUserForm = "Добавление пользователя";
        public static string UpdateUserForm = "Обновление пользователя";
        public static string AddGroupForm = "Добавление группы";
        public static string UpdateGroupForm = "Обновление группы";
        public static string AddCurriculumUnitForm = "Добавление единицы уч.плана";
        public static string UpdateCurriculumUnitForm = "Обновление единицы уч.плана";
        public static string AddMarkForm = "Добавление оценки";
        public static string UpdateMarkForm = "Обновление оценки";
        public static string AddAttestationForm = "Добавление аттестации";
        public static string UpdateAttestationForm = "Обновление аттестации";

        public static string IncorrectValueText = "Неверное значение в поле \"{0}\". Пожалуйста, измените его и повторите попытку!";
        public static string IncorrectValueDropdownText = "Не выбрано значение в выпадающем списке \"{0}\". Пожалуйста, сделайте выбор и повторите попытку!";

        public class Marks
        {
            public const string NotAttended = "неявка";
            public const string NotAllowed = "недопуск";
            public const string NotProvided = "не выставлена";
        }

        public class Roles
        {
            public const string Student = "Учащийся";
            public const string Teacher = "Преподаватель";
            public const string Admin = "Админ";

            public static Dictionary<UserRole,string> Values = new()
            {
                [UserRole.Admin] = Admin,
                [UserRole.Student] = Student,
                [UserRole.Teacher] = Teacher,
            };
        }
        public class SubjectFields
        {
            public const string Subject = "Уч. предмет";
        }

        public class WorkTypeFields
        {
            public const string WorkType = "Вид работы";
        }

        public class CurriculumUnitFields
        {
            public const string CurriculumUnit = "Единица уч. плана";
            public const string Semester = "Семестр";
        }

        public class GroupFields
        {
            public const string Group = "Группа";
        }

        public class AttestationFields
        {
            public const string Teacher = "Преподаватель";
            public const string Date = "Дата";
        }

        public class UserFields
        {
            public const string User = "Пользователь";
            public const string FirstName = "Имя";
            public const string MiddleName = "Отчество";
            public const string LastName = "Фамилия";
            public const string Email = "Email";
            public const string IsDisabled = "Отключен";
            public const string PasswordHash = "Пароль";
            public const string Role = "Роль";
            public const string Group = "Группа";
            public const string Info = "Доп. информация";
        }
    }
}
