using Lab7.Services.Interfaces;
using Lab7.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IFootballClubService, FootballClubService>(); // Here I use AddSingleton, because it is possible that the application needs
                                                                            // only one instance of FootballClubService for the entire application life cycle.
                                                                            // And in order to add items to the List


builder.Services.AddSingleton<IFootballPlayerService, FootballPlayerService>(); // Here I use AddSingleton, because it is possible that the application needs
                                                                                // only one instance of FootballPlayerService for the entire application life cycle.
                                                                                // And in order to add items to the List


builder.Services.AddSingleton<IMatchesService, MatchesService>(); // Here I use AddSingleton, because it is possible that the application needs
                                                                  // only one instance of MatchService for the entire application life cycle.
                                                                  // And in order to add items to the List


builder.Services.AddScoped<PasswordService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddTransient<IUserService>(provider => provider.GetRequiredService<UserService>());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MyAppIssuer",
            ValidAudience = "MyAppAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASuperSecretKeyForJWTTokenGeneration"))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Football Clubs Rest WebAPI", Version = "v1", Description = "This is the API for managing Football Clubs" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter a valid token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Football Clubs Rest WebAPI v1"));
    app.UseAuthentication();
    app.UseAuthorization();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
