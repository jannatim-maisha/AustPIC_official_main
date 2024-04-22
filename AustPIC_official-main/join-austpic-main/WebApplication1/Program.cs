using AutoMapper;
using ClubWebsite.DBContexts;
using ClubWebsite.Mapper;
using ClubWebsite.Service.Join;
using ClubWebsite.Service.Payment;
using ClubWebsite.Service.Sms;
using ClubWebsite.Service.Email;
using ClubWebsite.Service.Template;
using System.Text.Json;
using ClubWebsite.Models.Email;
using Microsoft.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using ClubWebsite.Middleware;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.Cookie.Name = "Join-AUSTPIC-Web-APP";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60*24);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
            });

builder.Services.AddMvc();
builder.Services.AddScoped<IDapperDBContext, DapperDBContext>();
builder.Services.AddScoped<IJoinService, JoinService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITemplateService,TemplateService>();
builder.Services.AddAntiforgery(options => {
    options.HeaderName = "XSRF-TOKEN-Join-AUSTPIC-App";
    options.Cookie.Name = "XSRF-TOKEN-Join-AUSTPIC-App";
    options.FormFieldName = "XSRF-TOKEN-Join-AUSTPIC-App";
});
builder.Services.AddHttpContextAccessor();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Join/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler("/Join/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiXssMiddleware();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Join}/{action=Index}/{id?}");

app.Run();
