using IdentityTest.Models.IdentityAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityTest.Services.JwtToken;

public class JwtTokenProvider(
    IConfiguration configuration,
    UserManager<ApplicationUser> userManager) : IJwtTokenProvider
{
    public async Task<string> Generate(ApplicationUser user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["JWT:Issuer"],
            configuration["JWT:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
