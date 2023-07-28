using anonim_chat.API.Models;

namespace anonim_chat.API.Managers;

public interface IUserManager
{
    ValueTask<UserModel> GetUserAsync();
    ValueTask<UserModel> RegisterAsync(CreateUserModel createUserModel);
    ValueTask<string> LoginAsync(LoginUserModel loginUserModel);
}
