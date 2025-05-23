using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using LibraryDV.Repos;
using System.Diagnostics;

namespace WebsiteDV.Pages.Userpages
{
    public class AnimalsModel : PageModel
    {
        static AnimalRepo _animalRepo = new AnimalRepo();
        AnimalService _animalService = new AnimalService(_animalRepo);
        
        [BindProperty]
        public List<Animal> Animals { get; set; }

        [BindProperty]
        public bool FilterCat { get; set; } 
        [BindProperty]
        public bool FilterDog { get; set; }

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
            if (FilterCat)
            {
                Debug.WriteLine("OPF: FilterCat = true");
                Animals = _animalService.FilterAnimalsByType("Cat");
            }
            if (FilterDog)
            {
                Animals = _animalService.FilterAnimalsByType("Dog");
            }
        }

        public void OnPostSort()
        {
            Animals = _animalService.SortAnimalsByWeight();
        }
    }
}
