using StudentsManagement.BusinessLogic.Enums;

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public string Info { get; set; }
        public string GroupId { get; set; }
    }
}
