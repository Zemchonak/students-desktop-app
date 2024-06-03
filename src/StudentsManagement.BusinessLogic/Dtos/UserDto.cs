using StudentsManagement.Common.Enums;
using StudentsManagement.DesktopApp.Common;
using System.Text;

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class UserDto : BaseDto
    {
        public string ShortenedName
        { get
            {
                var sb = new StringBuilder($"{LastName} {FirstName[0]}.");
                if (!string.IsNullOrEmpty(MiddleName))
                    sb.Append($"{MiddleName[0]}.");

                return sb.ToString();
            }
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsDisabled { get; set; }
        public string IsDisabledText { get =>
                IsDisabled.HasValue && IsDisabled.Value ?
                    AppLocalization.Yes : AppLocalization.No; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public string RoleName { get => AppLocalization.Roles.Values[Role]; }

        public string Info { get; set; }
        public Guid? GroupId { get; set; }

        public string GroupName { get; set; }

        public string MarkValue { get; set; }
    }
}
