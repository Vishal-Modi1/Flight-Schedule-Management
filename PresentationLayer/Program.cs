using Configuration;
using Microsoft.AspNetCore.Http.Features;
using PresentationLayer.Utilities;

var builder = WebApplication.CreateBuilder(args);
var _configurationSettings = ConfigurationSettings.Instance;

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
});

builder.Services.AddAuthentication("CookieAuthentication")
.AddCookie("CookieAuthentication", config =>
{
   config.Cookie.Name = _configurationSettings.CookieName;
   config.LoginPath = "/Account/Login";
               // config.ExpireTimeSpan = TimeSpan.FromSeconds(25);
               config.ExpireTimeSpan = TimeSpan.FromDays(_configurationSettings.JWTExpireDays);
   config.SlidingExpiration = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");
});

CurrentUserPermissionManager.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
HttpCaller.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.Run();