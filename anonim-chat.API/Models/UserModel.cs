using anonim_chat.API.Entities;

namespace anonim_chat.API.Models;

public record UserModel(
    Guid Id,
    string UserName,
    bool IsBoy
    );

public static class UserExtensions
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel(user.Id, user.UserName, user.IsBoy);
    }
}
