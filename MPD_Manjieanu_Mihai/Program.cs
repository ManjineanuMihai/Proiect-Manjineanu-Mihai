using Microsoft.EntityFrameworkCore;
using MPD_Manjineanu_Mihai.Data;
using MPD_Manjineanu_Mihai.Repositories;
using MPD_Manjineanu_Mihai.Repositories.Activity;
using MPD_Manjineanu_Mihai.Repositories.Category;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ProiectDb")));
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IActivityRepository,ActivityRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();



builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.IsEssential = true;
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
app.UseStaticFiles();

app.UseRouting();
app.UseSession();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=LoginIndex}/{id?}");

app.Run();
