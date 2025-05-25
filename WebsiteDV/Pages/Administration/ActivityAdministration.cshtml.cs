using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using LibraryDV.Services;
using LibraryDV.Models;
using System.Diagnostics;


namespace WebsiteDV.Pages.Administration
{
    public class ActivityAdministrationModel : PageModel
    {
        private ActivityService _activityService;

        [BindProperty]
        public List<LibraryDV.Models.Activity> Activities { get; set; }
        [BindProperty]
        public string Title {  get; set; }
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

        public ActivityAdministrationModel(ActivityService actServ)
        {
            _activityService = actServ;
        }
        public void OnGet()
        {
            Activities = _activityService.GetAllActivities();
        }

        public IActionResult OnPostCreate()
        { 
            _activityService.CreateActivity(Date, Title, Text, ImgPath, StartHour, EndHour);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete()
        {
            Debug.WriteLine("OPD ID: " + ToDelete);
            _activityService.DeleteActivity(ToDelete);
            return RedirectToPage();
        }
    }
}
