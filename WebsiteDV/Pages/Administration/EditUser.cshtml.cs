using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;

namespace WebsiteDV.Pages.Administration
{
    public class EditUserModel : PageModel
    {
        private readonly UserServices _userServices;

        public EditUserModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        [BindProperty]
        public int UserId { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public int UserType { get; set; } // 1 = Employee, 2 = Admin

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet(int userId)
        {
            var user = _userServices.GetUserID(userId);
            if (user == null)
            {
                ErrorMessage = "User not found.";
                return Page();
            }

            UserId = user.UserID;
            Name = user.Name;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Password = (user is User.Employee emp) ? emp.Password : "";
            UserType = (user is User.Admin) ? 2 : 1;



            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            _userServices.EditUser(UserId, Name, PhoneNumber, Email);
            SuccessMessage = "User updated successfully.";
            return RedirectToPage("/Administration/UserAdministration");
        }
    }
}
