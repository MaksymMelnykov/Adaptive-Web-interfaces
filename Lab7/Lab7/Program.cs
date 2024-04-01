using Lab7.Services.Interfaces;
using Lab7.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Lab7.Database;
using Microsoft.EntityFrameworkCore;
using Lab7;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

try
{
    Log.Warning("Start debugging!!!");
    Log.Information("Starting web host at {Time} by {Name}", DateTime.Now, Environment.UserName);
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();


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

    builder.Services.AddScoped<IVersionedService, VersionedService>();

    builder.Services.AddSingleton<MemoryHealthCheckService>();
    builder.Services.AddSingleton<DiskHealthCheckService>();
    builder.Services.AddSingleton<NetworkHealthCheckService>();
    builder.Services.AddSingleton<DatabaseHealthCheckService>();

    builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddHealthChecks()
        .AddCheck<MemoryHealthCheckService>("Memory Health Check", tags: new[] { "memory" })
        .AddCheck<DiskHealthCheckService>("Disk Health Check", tags: new[] { "disk" })
        .AddCheck<NetworkHealthCheckService>("Network Health Check", tags: new[] { "network" })
        .AddDbContextCheck<AppDbContext>("Database Health Check");

    builder.Services.AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Memory Checks", "/health/memory");
        options.AddHealthCheckEndpoint("Disk Checks", "/health/disk");
        options.AddHealthCheckEndpoint("Network Checks", "/health/network");
        options.AddHealthCheckEndpoint("Database Checks", "/health/database");
    }).AddInMemoryStorage();

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });


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
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Football Clubs and API Versioning Rest WebAPI", Version = "v1", Description = "This is the API for managing Football Clubs" });
        options.SwaggerDoc("v2", new OpenApiInfo { Title = "API Versioning Rest WebAPI", Version = "v2" });
        options.SwaggerDoc("v3", new OpenApiInfo { Title = "API Versioning Rest WebAPI", Version = "v3" });

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
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Football Clubs and API Versioning Rest WebAPI v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "API Versioning Rest WebAPI v2");
            options.SwaggerEndpoint("/swagger/v3/swagger.json", "API Versioning Rest WebAPI v3");
        });

        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = Enricher.HttpRequestEnricher;
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health/memory", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("memory"),
                ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse
            });

            endpoints.MapHealthChecks("/health/disk", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("disk"),
                ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse
            });

            endpoints.MapHealthChecks("/health/network", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("network"),
                ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse
            });

            endpoints.MapHealthChecks("/health/database", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("database"),
                ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse
            });
        });
    }

    app.UseHealthChecksUI(options =>
    {
        options.UIPath = "/healthchecks-ui";
    });

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}