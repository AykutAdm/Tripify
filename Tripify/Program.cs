using Microsoft.Extensions.Options;
using System.Reflection;
using Tripify.Services.CategoryServices;
using Tripify.Services.CommentServices;
using Tripify.Services.TourServices;
using Tripify.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITourService, TourService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
