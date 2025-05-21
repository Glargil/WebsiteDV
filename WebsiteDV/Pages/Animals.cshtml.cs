using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using LibraryDV.Repos;

namespace WebsiteDV.Pages
{
    public class AnimalsModel : PageModel
    {
        public List<Animal> Animals { get; set; }

        public void OnGet()
        {
        }
    }
}
