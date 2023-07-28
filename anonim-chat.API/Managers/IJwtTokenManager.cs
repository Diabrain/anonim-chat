using anonim_chat.API.Entities;

namespace anonim_chat.API.Managers
{
    public interface IJwtTokenManager
    {
        string GenerateToken(User user);
    }
}
