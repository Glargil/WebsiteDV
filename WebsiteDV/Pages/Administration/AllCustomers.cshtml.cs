using LibraryDV.Models;
using LibraryDV.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages.Administration
{
    public class AllCustomersModel : PageModel
    {
        private readonly UserServices _userServices;

        public AllCustomersModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        public List<User> Users { get; set; } = new List<User>();

        public void OnGet()
        {
            Users = new List<User>();
            foreach (User user in _userServices.GetAllUsers())
            {
                if (user is User.Customer)
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
