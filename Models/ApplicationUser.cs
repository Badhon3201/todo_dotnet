using Microsoft.AspNetCore.Identity;

public class ApplicationUser:IdentityUser<int>{
    public string FullName { get; set; }

}