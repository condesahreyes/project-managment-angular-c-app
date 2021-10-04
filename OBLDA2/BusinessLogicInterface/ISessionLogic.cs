using Domain;

namespace BusinessLogicInterface
{
    public interface ISessionLogic
    {
        bool IsCorrectToken(string token);
        void Logout(string token);

        string Login(string email, string password);
        string GenerateAndInsertToken(User user);

        User GetUsers(string email, string password);
    }
}
