using anonim_chat.API.Entities;

namespace anonim_chat.API.Models;

public record ChatModel(
 Guid Id,
 Guid CreatedUserId,
 Guid? JoinedUserId,
 string Key,
 List<Message>? Messages);


public static class ChatExtensions
{
    public static ChatModel ToModel(this Chat chat)
    {
        return new ChatModel(chat.Id,chat.CreatedUserId,chat.JoinedUserId,chat.Key, chat.Messages);
    }
}