using API.ApiModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] UserCredentials user)
    {
        if (ModelState.IsValid)
        {
            var newUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                return Ok("Account was created successfully");
            }
        }
        return BadRequest("Could not create user with given information");
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserCredentials user)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
            if (result.Succeeded)
            {
                return Ok("Successfully logged in");
            }
        }
        return BadRequest("Invalid email or password");
    }
}