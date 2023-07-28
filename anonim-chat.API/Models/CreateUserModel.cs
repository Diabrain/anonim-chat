namespace anonim_chat.API.Models;
public class CreateUserModel 
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public bool IsBoy { get; set; }
}
