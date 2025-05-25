using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using System.Linq;

namespace WebsiteDV.Pages.Administration
{
    public class EditCustomerModel : PageModel
    {
        private readonly UserServices _userServices;

        public EditCustomerModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        [BindProperty]
        public int UserID { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet(int userId)
        {
            var user = _userServices.GetUserID(userId);
            if (user == null || user.Type.ToString() != "Customer")
            {
                ErrorMessage = "Kunden blev ikke fundet.";
                return Page();
            }

            UserID = user.UserID;
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Alle felter skal udfyldes.";
                return Page();
            }

            _userServices.EditUser(UserID, Name, PhoneNumber, Email);

            SuccessMessage = "Kundeoplysninger er opdateret!";
            return RedirectToPage("/Administration/AllCustomers"); // Or wherever you want to redirect
        }
    }
}
