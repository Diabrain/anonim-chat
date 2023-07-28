namespace anonim_chat.API.Exceptions;


public class LoginValidationException : Exception
{
    public LoginValidationException() : base("Username or password is incorrect") { }
}