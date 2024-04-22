using AustPICWeb.Data;
using AustPICWeb.DBContexts;
using AustPICWeb.Middleware;
using AustPICWeb.Repositories.Blog;
using AustPICWeb.Repositories.Committee;
using AustPICWeb.Repositories.Contest;
using AustPICWeb.Repositories.CssVariable;
using AustPICWeb.Repositories.Event;
using AustPICWeb.Repositories.Gallery;
using AustPICWeb.Repositories.Newsletter;
using AustPICWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddScoped<IDapperDBContext, DapperDBContext>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<ICommitteeRepository, CommitteeRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IContestRepository, ContestRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ICssRepository, CssRepository>();
builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiXssMiddleware();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
