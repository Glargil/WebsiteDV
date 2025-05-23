using LibraryDV.Repos;
using LibraryDV.Services;
using LibraryDV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebsiteDV.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private static IBlogPostRepo _blogPostRepo;
    private BlogPostService _blogPostService;

    [BindProperty]
    public List<BlogPost> BlogPosts { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, IBlogPostRepo bPRepo, BlogPostService bPServ)
    {
        _logger = logger;
        _blogPostRepo = bPRepo;
        _blogPostService = bPServ;
    }

    public void OnGet()
    {
        BlogPosts = _blogPostService.GetAllBlogPosts();
    }
}
