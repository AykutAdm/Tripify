using Microsoft.Extensions.Options;
using System.Reflection;
using Tripify.WebApi.Services.AboutServices;
using Tripify.WebApi.Services.BookingServices;
using Tripify.WebApi.Services.GuideServices;
using Tripify.WebApi.Services.OpenAIServices;
using Tripify.WebApi.Services.TestimonialServices;
using Tripify.WebAPI.Services.CategoryServices;
using Tripify.WebAPI.Services.CommentServices;
using Tripify.WebAPI.Services.TourServices;
using Tripify.WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service registrations
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IGuideService, GuideService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Database Settings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
