using EventManager.Web.App.Startup;

var builder = WebApplication.CreateBuilder(args);
Services.Add(builder.Services, builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
Middlewares.Use(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
