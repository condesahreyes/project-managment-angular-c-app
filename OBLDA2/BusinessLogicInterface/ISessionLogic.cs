
using Domain;

namespace BusinessLogicInterface
{
    public interface ISessionLogic
    {
        bool IsCorrectToken(string token);

        string Login(string email, string password);

        User GetUsers(string email, string password);

        string GenerateAndInsertToken(User user);

        bool Logout(string token);
    }
}
