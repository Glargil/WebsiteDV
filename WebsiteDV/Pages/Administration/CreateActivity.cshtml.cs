using System.Xml.Linq;
using LibraryDV.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LibraryDV.Models.User;

namespace WebsiteDV.Pages.Administration
{
    public class CreateActivityModel : PageModel
    {
        private ActivityService _activityService;

        [BindProperty]
        public List<LibraryDV.Models.Activity> Activities { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [BindProperty]
        public int StartHour { get; set; }
        [BindProperty]
        public int EndHour { get; set; }
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int ToDelete { get; set; }
        public string ImgPath { get; set; } = string.Empty;
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public CreateActivityModel(ActivityService actServ)
        {
            _activityService = actServ;
        }

        public IActionResult OnPost()
        {
            if (
                string.IsNullOrWhiteSpace(Title) ||
                Date == default ||
                StartHour <= 0 ||
                EndHour <= 0 ||
                string.IsNullOrWhiteSpace(Text)
                )
            {
                {
                    ErrorMessage = "Alle felter skal udfyldes!";
                    return Page();
                }
            }
            else
            {
                _activityService.CreateActivity(Date, Title, Text, ImgPath, StartHour, EndHour);
                SuccessMessage = "Ny Aktivitet Oprettet!";
            }

            return RedirectToPage("/Administration/ActivityAdministration");
        }
        public void OnGet()
        {
        }
    }
}
