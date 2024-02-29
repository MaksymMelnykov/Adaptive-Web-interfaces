using Lab7.Services.Interfaces;
using Lab7.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IFootballClubService, FootballClubService>(); // ¬икористовую AddScoped, оск≥льки можливо, що потр≥бно створити окремий
                                                                         // серв≥с дл€ кожного HTTP-запиту.


builder.Services.AddSingleton<IFootballPlayerService, FootballPlayerService>(); // јналог≥чно, використовую AddScoped дл€ PlayerService.


builder.Services.AddSingleton<IMatchesService, MatchesService>(); // “ут використовую AddSingleton, оск≥льки можливо, що в додатку потр≥бен лише один
                                                                  // екземпл€р MatchService дл€ всього життЇвого циклу додатку.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Football Clubs Rest WebAPI", Version = "v1", Description = "This is the API for managing Football Clubs" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Football Clubs Rest WebAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
