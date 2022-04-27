using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shirzad.AutoMapperProfile;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.Core.Repository.Services;
using Shirzad.DataLayer.Context;
using Shirzad.DataLayer.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Databases
builder.Services.AddDbContext<ApplicationContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Recognizing the interface
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductService>();

//Identity Methods
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

// Auto Mapper configuration
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// for taostr
builder.Services.AddNotyf(config =>
{ config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

var app = builder.Build();


//------Init EF database and demo user:
using (IServiceScope serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.EnsureCreated();
    //context.Database.Migrate();

    var usrMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    var user = usrMgr.FindByEmailAsync("admin@gmail.com").Result;
    if (user == null)
    {
        user = new ApplicationUser()
        {
            Email = "admin@gmail.com",
            UserName = "admin@gmail.com",
            EmailConfirmed = true,

        };
        usrMgr.CreateAsync(user, "Admin123*").Wait();
    }
}
//-----------------------------------

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SitePages}/{action=Index}/{id?}");

app.Run();
