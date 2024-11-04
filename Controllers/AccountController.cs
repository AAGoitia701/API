using API.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repository.IRepository;
using WebAPI.Dtos.Account;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _token;
        private readonly SignInManager<AppUser> _signinmanager;
        public AccountController(UserManager<AppUser> userManager, ITokenService token, SignInManager<AppUser> signin)
        {
            _userManager = userManager;  
            _token = token;
            _signinmanager = signin;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model not valid");
                }

                var appUser = new AppUser
                {
                    UserName = registerdto.Username,
                    Email = registerdto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerdto.Password);

                if (createdUser.Succeeded) 
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded) 
                    {
                        return Ok(new NewUserDto
                        {
                            Username = appUser.UserName,
                            Email = appUser.Email,
                            Token = _token.CreateToken(appUser) //after registering, you automatically login
                        }
                        );
                    }
                    else
                    {
                        return StatusCode(statusCode: 500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(statusCode: 500, createdUser.Errors);
                }

            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto logindto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _userManager.Users.FirstOrDefaultAsync(r => r.UserName == logindto.Username);

            if(user == null) { return NotFound("The username does not exist"); }

            var res = await _signinmanager.CheckPasswordSignInAsync(user, logindto.Password, false); // lockout on failure turned off. 

            if (!res.Succeeded) { return Unauthorized("Username not found and/or password incorrect"); }

            return Ok(
                new NewUserDto
                {

                    Username = user.UserName,
                    Email = user.Email,
                    Token = _token.CreateToken(user)
                }
                );
                
        }
    }
}
