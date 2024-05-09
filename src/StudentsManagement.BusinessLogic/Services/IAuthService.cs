namespace StudentsManagement.BusinessLogic.Services
{
    public interface IAuthService
    {
        public Guid SignIn(string email, string passwordhash);

        public Guid Register(string email, string passwordhash);

        public void ChangePassword(string email, string oldPasswordhash, string newPasswordHash);
    }
}
