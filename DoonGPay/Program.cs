using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using DoonGPay.INteface;
using DoonGPay.Service;
using DoonGPay.Service.Travel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MyContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlCs"));
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITravelService, TravelService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<ICalcService, CalcService>();
builder.Services.AddScoped<IMySession, MySession>();


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // مسیر لاگین
        options.LogoutPath = "/Logout"; // مسیر لاگ‌اوت
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // مدت انقضا کوکی
        options.SlidingExpiration = true; // تمدید خودکار در صورت فعالیت
    });


builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
