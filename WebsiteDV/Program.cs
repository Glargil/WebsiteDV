using LibraryDV.Services;
using LibraryDV.Repos;

namespace WebsiteDV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<IAnimalRepo, AnimalRepo>(); 
            builder.Services.AddSingleton<AnimalService>();
            builder.Services.AddSingleton<IActivityRepo, ActivityRepo>();
            builder.Services.AddSingleton<ActivityService>();
            builder.Services.AddSingleton<IBlogPostRepo, BlogPostRepo>();
            builder.Services.AddSingleton<BlogPostService>();
            builder.Services.AddSingleton<IBookingRepo, BookingRepo>();
            builder.Services.AddSingleton<BookingService>();
            builder.Services.AddSingleton<IUserRepo, UserRepo>();
            builder.Services.AddSingleton<UserServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
