using anonim_chat.API.Entities;
using anonim_chat.API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace anonim_chat.API.Managers;

public class JwtTokenManager:IJwtTokenManager
{
    private readonly JwtOption _jwtOption;

    public JwtTokenManager(IOptions<JwtOption> jwtOption)
    {
        _jwtOption = jwtOption.Value;
    }

   
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };


        var signingKey = System.Text.Encoding.UTF32.GetBytes(_jwtOption.SigningKey);
        var security = new JwtSecurityToken(
        issuer: _jwtOption.ValidIssuer,
            audience: _jwtOption.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_jwtOption.ExpiresInSeconds),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(security);

        return token;
    }
}