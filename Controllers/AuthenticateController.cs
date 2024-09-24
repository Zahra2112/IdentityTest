using IdentityTest.Dtos;
using IdentityTest.Models.IdentityAggregate;
using IdentityTest.Persistence;
using IdentityTest.Services.JwtToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTest.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;

    public AuthenticateController(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        IJwtTokenProvider jwtTokenProvider)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _jwtTokenProvider = jwtTokenProvider;
    }


    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
         
        if (user == null)
            return NotFound(new { Message = "User with this username dose not exist!" });

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return BadRequest(new { Message = "Password is wrong!" });

        var token = await _jwtTokenProvider.Generate(user);
        
        return Ok(token);
    }




}
