using anonim_chat.API.Context;
using anonim_chat.API.Entities;
using anonim_chat.API.Exceptions;
using anonim_chat.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace anonim_chat.API.Managers;

public class UserManager : IUserManager
{
    private readonly IJwtTokenManager _jwttokenManager;
    private readonly IUserProvider _userProvider;
    private readonly AppDbContext _context;

    public UserManager(
        IJwtTokenManager tokenManager,
        IUserProvider userProvider,
         AppDbContext context)
    {
        _jwttokenManager = tokenManager;
        _userProvider = userProvider;
        _context = context;
    }

    public async ValueTask<UserModel> GetUserAsync()
    {
        var user = await _context.Users.FindAsync(_userProvider.UserId);

        if (user == null)
        {
            throw new RecordNotFoundException("User");
        }

        return user.ToModel();
    }

    public async ValueTask<UserModel> RegisterAsync(CreateUserModel createUserModel)
    {
        var isUsernameExists = await _context.Users.AnyAsync(u => u.UserName == createUserModel.UserName);
        if (isUsernameExists)
        {
            throw new UsernameExistsException();
        }

        var user = new User()
        {
            UserName = createUserModel.UserName,
            PasswordHash = createUserModel.Password,
            IsBoy= createUserModel.IsBoy,
            
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, createUserModel.Password);

       
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        

        return user.ToModel();
    }

    public async ValueTask<string> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.UserName);

        var Password = new PasswordHasher<LoginUserModel>().HashPassword(loginUserModel,loginUserModel.Password);

        if (user == null || user.PasswordHash == Password )
        {
            throw new LoginValidationException();
        }

        var token = _jwttokenManager.GenerateToken(user);

        return token;
    }
}
