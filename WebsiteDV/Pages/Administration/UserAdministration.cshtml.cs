using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;

namespace WebsiteDV.Pages.Administration
{
    public class UserAdministrationModel : PageModel
    {
        private readonly UserServices _userServices;

        public UserAdministrationModel(UserServices userServices)
        {
            _userServices = userServices;
        }

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

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "All fields are required.";
                return;
            }

            if (UserType == 1)
            {
                _userServices.CreateEmployee(Name, PhoneNumber, Email, Password);
                SuccessMessage = "Employee created successfully.";
            }
            else if (UserType == 2)
            {
                _userServices.CreateAdmin(Name, PhoneNumber, Email, Password);
                SuccessMessage = "Admin created successfully.";
            }
            else
            {
                ErrorMessage = "Invalid user type selected.";
            }
        }
    }
}
