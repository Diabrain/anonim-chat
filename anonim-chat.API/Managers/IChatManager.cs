using anonim_chat.API.Entities;
using anonim_chat.API.Models;

namespace anonim_chat.API.Managers;

public interface IChatManager
{
    Task<List<ChatModel>> GetChatsAsync();
    Task<ChatModel> CreateChat(CreateChatModel model);
    Task<ChatModel> JoinChatModel(JoinChatModel model);
    
}
