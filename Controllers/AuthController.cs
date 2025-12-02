using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Services;
using System.Security.Claims;

namespace RecipeApp.Controllers
{
    [Route("auth")]
    [ApiController] // You can keep this or remove it, but we are acting like an MVC controller now
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // REMOVE the Record classes if you aren't using them for JSON anymore, 
        // or keep them if you plan to support an API later.

        [HttpPost("login")]
        [AllowAnonymous]
        // Change [FromBody] to [FromForm] so standard HTML forms work
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] string? returnUrl)
        {
            var user = await _userService.ValidateCredentialsAsync(email, password);
            
            if (user == null) 
            {
                // In a real app, you'd redirect back to login with an error query param
                return Unauthorized("Invalid credentials."); 
            }

            await SignInAsync(user);

            // Important: The SERVER tells the browser to redirect, ensuring the Cookie travels with it.
            return LocalRedirect(returnUrl ?? "/recipes");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
        
        // ... Register method needs similar changes if you want registration to auto-login ...
        
        private async Task SignInAsync(RecipeApp.Models.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName ?? user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true }
            );
        }
    }
}