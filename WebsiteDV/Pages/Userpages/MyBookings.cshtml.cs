using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryDV.Services;
using LibraryDV.Models;
using System.Collections.Generic;

namespace WebsiteDV.Pages.Userpages
{
    public class MyBookingsModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly BookingService _bookingService;

        public MyBookingsModel(UserServices userServices, BookingService bookingService)
        {
            _userServices = userServices;
            _bookingService = bookingService;
        }

        [BindProperty]
        public string Email { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Find the user by email
            var user = FindUserByEmail(Email);
            if (user == null)
            {
                ErrorMessage = "Du har ikke booket nogen besøgstid";
                return;
            }

            // Find bookings for this user
            Bookings = FindBookingsByUserId(user.UserID);

            if (Bookings == null || Bookings.Count == 0)
            {
                ErrorMessage = "Du har ikke booket nogen besøgstid";
            }
        }

        private User FindUserByEmail(string email)
        {
            foreach (var user in _userServices.GetAllUsers())
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }

        private List<Booking> FindBookingsByUserId(int userId)
        {
            var allBookings = _bookingService.GetAllBookings();
            var result = new List<Booking>();
            foreach (var booking in allBookings)
            {
                if (booking.UserID == userId)
                {
                    result.Add(booking);
                }
            }
            return result;
        }
    }
}
