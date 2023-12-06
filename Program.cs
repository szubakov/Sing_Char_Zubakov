using CharService;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sing_Char_Zubakov.Data;
using Sing_Char_Zubakov.Hubs;
using Sing_Char_Zubakov.Migrations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ChatingAppConnectionString");

builder.Services.AddDbContext<ChatingContext>(options => options.UseSqlite(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHangfire(configuration => configuration
.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseInMemoryStorage());

builder.Services.AddHangfireServer();



builder.Services.AddSignalR();


builder.Services.AddScoped<ChatService>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{


});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chat");

var scope = app.Services.CreateScope();
var chatService = scope.ServiceProvider.GetService<ChatService>();

RecurringJob.AddOrUpdate("ChackYmlFiles", () => chatService.DeleteOldMessages(), Cron.Minutely());

app.Run();
