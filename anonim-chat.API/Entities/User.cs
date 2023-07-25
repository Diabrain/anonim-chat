namespace anonim_chat.API.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
}
