using LibraryDV.Repos;
using LibraryDV.Services;
using LibraryDV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages.Administration
{
    public class BlogPostAdministrationModel : PageModel
    {
        IBlogPostRepo _blogPostRepo = new BlogPostRepo();
        BlogPostService _blogPostService;

        [BindProperty]
        public List<BlogPost> BlogPosts { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string ImgPath { get; set; } = string.Empty;
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int ToDelete { get; set; }

        public BlogPostAdministrationModel(IBlogPostRepo blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
            _blogPostService = new BlogPostService(_blogPostRepo);
        }
        public void OnGet()
        {
            BlogPosts = _blogPostService.GetAllBlogPosts();
        }

        public IActionResult OnPostCreate()
        {
            _blogPostService.CreateBlogPost(Title, ImgPath, Text);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete()
        {
            _blogPostService.DeleteBlogPost(ToDelete);
            return RedirectToPage();
        }
    }
}
