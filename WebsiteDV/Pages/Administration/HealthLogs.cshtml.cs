using LibraryDV.Repos;
using LibraryDV.Services;
using LibraryDV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages.Administration
{
    public class HealthLogsModel : PageModel
    {
        static AnimalRepo _animalRepo = new AnimalRepo();
        AnimalService _animalService = new AnimalService(_animalRepo);

        [BindProperty]
        public List<Animal> Animals { get; set; }
        public HealthLogsModel(AnimalService animalService)
        {
            _animalService = animalService;
        }
        public void OnGet()
        {
            Animals = _animalService.GetAllAnimals();
        }
    }
}
