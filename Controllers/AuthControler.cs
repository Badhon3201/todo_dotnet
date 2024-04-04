using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager,IConfiguration configuration) : ControllerBase{
    

    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUser>> Register( RegisterModel model){
        var user = new ApplicationUser{
            FullName = model.FullName,
            Email = model.Email,
            UserName = model.Email,
        };
        var status = await userManager.CreateAsync(user,model.Password);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginModel model){
        var user = await userManager.FindByEmailAsync(model.Email);
        if(user==null){
            return BadRequest("Username or password incorrect");
        }
        var result = await signInManager.PasswordSignInAsync(user,model.Password,true,true);

        if(!result.Succeeded){
            return BadRequest("Username or password incorrect");
        }

        var token = GetToken(user);

        var loginResponse = new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token));

        return Ok(loginResponse);


    }

    private JwtSecurityToken GetToken(ApplicationUser user){
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Email,user.Email),
        };

        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(3),
            claims: claims,
            signingCredentials: new SigningCredentials(signInKey,SecurityAlgorithms.HmacSha256)
        );

        return token;
    
    }
}

public record RegisterModel(string FullName, string Email, string Password);

public record LoginResponse(string Token);

public record LoginModel(string Email, string Password);