using IdentityTest.Dtos;
using IdentityTest.Models.IdentityAggregate;
using IdentityTest.Persistence;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityTest.Controllers;


[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public MainController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
	{
        _userManager = userManager;
        _context = context;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("AllUsersForAdmin")]
    public async Task<ActionResult<GetProfileDto>> GetAllUsersForAdmin()
    {
        var users = await _context.Users.ToListAsync();
        var mappedUsers = users.Adapt<List<GetProfileDto>>();

        return Ok(mappedUsers);
    }

    [Authorize(Roles ="Winner")]
    [HttpGet("WinnerProfile")]
    public async Task<ActionResult<GetProfileDto>> GetProfileForWinner()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);
        var mappedUser = user.Adapt<GetProfileDto>();

        return Ok(mappedUser);
    }



}
