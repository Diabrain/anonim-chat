namespace anonim_chat.API.Exceptions;

public class UsernameExistsException : Exception
{
    public UsernameExistsException() : base("Username already exists.")
    {

    }
}