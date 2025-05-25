using LibraryDV.Repos;
using LibraryDV.Services;
using LibraryDV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages.Administration
{
    public class BookingAdministrationModel : PageModel
    {
        private readonly IBookingRepo _bookingRepo = new BookingRepo();
        private readonly BookingService _bookingService;

        [BindProperty]
        public List<Booking> Bookings { get; set; }
        [BindProperty]
        public int ToDelete { get; set; }

        public BookingAdministrationModel(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
            _bookingService = new BookingService(_bookingRepo);
        }
        public void OnGet()
        {
            Bookings = _bookingService.GetAllBookings();
        }

        public IActionResult OnPostDelete()
        {
            _bookingService.DeleteBooking(ToDelete);
            return RedirectToPage();
        }
    }
}
