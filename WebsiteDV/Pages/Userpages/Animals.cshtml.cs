using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using LibraryDV.Repos;

namespace WebsiteDV.Pages.Userpages
{
    public class AnimalsModel : PageModel
    {
        static AnimalRepo _animalRepo = new AnimalRepo();
        AnimalService _animalService = new AnimalService(_animalRepo);
        
        [BindProperty]
        public List<Animal> Animals { get; set; }

        public string Type { get; set; }

        public AnimalsModel(AnimalService animalService)
        {
            _animalService = animalService;
        }

        public void OnGet()
        {
            Animals = _animalService.GetAllAnimals();
        }

        public void OnPostFilter()
        {
           Animals = _animalService.FilterAnimalsByType(Type);
        }
    }
}
