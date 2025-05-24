using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;

namespace WebsiteDV.Pages.Administration
{
    public class UserAdministrationModel : PageModel
    {
        private readonly UserServices _userServices;

        public UserAdministrationModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        public List<User> Users { get; set; } = new List<User>();

        public void OnGet()
        {
            Users = new List<User>();
            foreach (User user in _userServices.GetAllUsers())
            {
                if (user is User.Employee || user is User.Admin)
                {
                    Users.Add(user);
                }
            }
        }


        public IActionResult OnPostDelete(int userId)
        {
            _userServices.DeleteUser(userId);
            return RedirectToPage();
        }

    }
}
