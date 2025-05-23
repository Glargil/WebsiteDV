using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;

// Uses Authentication and Claims for user login
// and stores the authentication cookie in the browser
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly UserServices _userServices;

    public LoginModel(UserServices userServices)
    {
        _userServices = userServices;
    }

    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        User user = _userServices.AuthenticateUser(Email, Password);
        if (user == null)
        {
            ErrorMessage = "Invalid login or insufficient permissions.";
            return Page();
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Type.ToString()), // "Admin" or "Employee"
            new Claim("UserId", user.UserID.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
        // TODO: Set authentication cookie/session here

        return RedirectToPage("/Index");
    }
}
