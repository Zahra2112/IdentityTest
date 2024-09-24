using IdentityTest.Models.IdentityAggregate;
using IdentityTest.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SetupController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SetupController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    [HttpGet("Setup")]
    public async Task<IActionResult> Setup()
    {
        //init roles
        var adminRole = new ApplicationRole() { Name = "Admin", Title = "Admin Role" };

        if (await _roleManager.FindByNameAsync(adminRole.Name) is null)
        {
            await _roleManager.CreateAsync(adminRole);
        }

        var winnerRole = new ApplicationRole() { Name = "Winner", Title = "Winner Role" };

        if (await _roleManager.FindByNameAsync(winnerRole.Name) is null)
        {
            await _roleManager.CreateAsync(winnerRole);
        }

        //init users
        var defaultPassword = "123456";

        var adminUser = new ApplicationUser() { UserName = "Admin", Title = "Admin User" };

        if(await _userManager.FindByNameAsync(adminUser.UserName) is null)
        {
            await _userManager.CreateAsync(adminUser, defaultPassword);
            await _userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

        var winnerUser = new ApplicationUser() { UserName = "Winner", Title = "Winner User" };

        if (await _userManager.FindByNameAsync(winnerUser.UserName) is null)
        {
            await _userManager.CreateAsync(winnerUser, defaultPassword);
            await _userManager.AddToRoleAsync(winnerUser, winnerRole.Name);
        }

        return Ok();
    }

}
