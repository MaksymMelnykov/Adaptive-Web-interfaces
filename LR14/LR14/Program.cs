using LR14.BackgroundServices;
using LR14.Database;
using LR14.Hubs;
using LR14.Interfaces;
using LR14.Jobs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHostedService<WebsiteAvailabilityService>();
builder.Services.AddHttpClient();

builder.Services.AddSendGrid(options =>
            options.ApiKey = builder.Configuration.GetValue<string>("SendGridApiKey"));

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    string jobSchedule = builder.Configuration.GetValue<string>("JobSchedule");
    q.AddJob<SendMailJob>(j => j
        .WithIdentity("SendRecurringMailJob")
        .WithDescription("This trigger will run every 30 seconds to send emails.")
        .Build()
    );

    q.AddTrigger(t => t
        .WithIdentity("SendRecurringMailJobTrigger")
        .ForJob("SendRecurringMailJob")
        .StartNow()
        .WithCronSchedule(jobSchedule)
    );
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSignalR();

builder.Services.AddHostedService<ExchangeRateService>();
builder.Services.AddHostedService<NotifyService>();
builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ISendMail, SendMail>();

builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<DbChangeData>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebSockets();
app.MapHub<NotifyHub>("/notifyHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


//BCxa0BW0sOY2z3gAUV77zu4F99tGcskw