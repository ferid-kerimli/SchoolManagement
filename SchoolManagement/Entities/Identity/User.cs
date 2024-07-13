using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Entities.Identity;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndTime { get; set; }
}
