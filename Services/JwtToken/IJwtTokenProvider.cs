using IdentityTest.Models.IdentityAggregate;

namespace IdentityTest.Services.JwtToken;

public interface IJwtTokenProvider
{
    public Task<string> Generate(ApplicationUser user);
}
