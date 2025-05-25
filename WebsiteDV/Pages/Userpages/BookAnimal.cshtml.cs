using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using System;

namespace WebsiteDV.Pages.Userpages
{
    public class BookAnimalModel : PageModel
    {
        private readonly AnimalService _animalService;
        private readonly UserServices _userServices;
        private readonly BookingService _bookingService;

        public BookAnimalModel(AnimalService animalService, UserServices userServices, BookingService bookingService)
        {
            _animalService = animalService;
            _userServices = userServices;
            _bookingService = bookingService;
        }

        [BindProperty]
        public int AnimalId { get; set; }
        [BindProperty]
        public string UserEmail { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string UserPhone { get; set; }
        [BindProperty]
        public DateTime BookingDate { get; set; }
        [BindProperty]
        public int BookingHour { get; set; }
        [BindProperty]
        public string Comment { get; set; }

        public Animal Animal { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet(int animalId)
        {
            Animal = _animalService.GetAnimal(animalId);
            if (Animal == null)
            {
                ErrorMessage = "Dyret blev ikke fundet.";
                return Page();
            }
            AnimalId = animalId;
            return Page();
        }

        public IActionResult OnPost()
        {
            Animal = _animalService.GetAnimal(AnimalId);
            if (Animal == null)
            {
                ErrorMessage = "Dyret blev ikke fundet.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(UserEmail) || string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(UserPhone) || BookingDate == default || BookingHour < 0 || BookingHour > 23)
            {
                ErrorMessage = "Alle felter skal udfyldes korrekt.";
                return Page();
            }

            // Find or create user
            var user = _userServices.GetAllUsers().FirstOrDefault(u => u.Email == UserEmail);
            if (user == null)
            {
                // Create as customer if not found
                _userServices.CreateCustomer(UserName, UserPhone, UserEmail);
                user = _userServices.GetAllUsers().FirstOrDefault(u => u.Email == UserEmail);
            }

            if (user == null)
            {
                ErrorMessage = "Kunne ikke oprette bruger.";
                return Page();
            }

            // Create booking
            _bookingService.CreateBooking(user.UserID, AnimalId, DateOnly.FromDateTime(BookingDate), BookingHour, Comment ?? "");

            SuccessMessage = "Din booking er oprettet!";
            return Page();
        }

    }
}
