using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Models.IdentityAggregate;

public class ApplicationRole : IdentityRole
{
    public string Title { get; set; } = string.Empty;
}
