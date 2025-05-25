using LibraryDV.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace WebsiteDV.Pages.Administration
{
    public class EditActivityModel : PageModel
    {
        private readonly ActivityService _activityService;

        public EditActivityModel(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [BindProperty]
        public int ActivityID { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public string ImgPath { get; set; }
        [BindProperty]
        public DateOnly Date { get; set; }
        [BindProperty]
        public int StartHour { get; set; }
        [BindProperty]
        public int EndHour { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet(int activityId)
        {
            var activity = _activityService.GetActivity(activityId);
            if (activity == null || activity.ActivityID == 0)
            {
                ErrorMessage = "Aktiviteten blev ikke fundet.";
                return Page();
            }

            ActivityID = activity.ActivityID;
            Title = activity.ActivityTitle;
            Text = activity.Text;
            Date = activity.Date;
            ImgPath = activity.ImgPath;
            StartHour = activity.StartHour;
            EndHour = activity.EndHour;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (
                string.IsNullOrWhiteSpace(Title) ||
                Date == default ||
                StartHour <= 0 ||
                EndHour <= 0 ||
                string.IsNullOrWhiteSpace(Text))
            {
                ErrorMessage = "Alle felter skal udfyldes.";
                return Page();
            }

            _activityService.EditActivity(ActivityID, Date, Title, Text, ImgPath, StartHour, EndHour);

            SuccessMessage = "Aktiviteten er opdateret!";
            return RedirectToPage("/Administration/ActivityAdministration"); // Or wherever you want to redirect
        }
    }
}
