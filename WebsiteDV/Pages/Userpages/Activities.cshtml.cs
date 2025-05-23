using LibraryDV.Repos;
using LibraryDV.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages.Userpages
{
    public class ActivitiesModel : PageModel
    {
        private static ActivityRepo _activityRepo = new ActivityRepo();
        private ActivityService _activityService = new ActivityService(_activityRepo);

        [BindProperty]
        public List<Activity> Activitties { get; set; }

        public ActivitiesModel(ActivityService activityService)
        {
            _activityService = activityService;
        }
        public void OnGet()
        {

        }
    }
}
