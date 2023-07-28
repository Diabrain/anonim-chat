namespace anonim_chat.API.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public List<Message>? Messages { get; set; }
    public Guid CreatedUserId { get; set; }
    public Guid? JoinedUserId { get; set; }
    public required string Key { get; set; }

}