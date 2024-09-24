using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Models.IdentityAggregate;

public class ApplicationUser : IdentityUser
{
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    

}
