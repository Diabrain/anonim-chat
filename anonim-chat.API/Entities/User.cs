namespace anonim_chat.API.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsBoy { get; set; }
    public List<Chat>? Chats { get; set; }
}