using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Blog.Models; // Zmie? na odpowiedni? przestrze? nazw

var builder = WebApplication.CreateBuilder(args);

// Dodaj us?ugi do kontenera.
builder.Services.AddControllersWithViews();

// Skonfiguruj DbContext z u?yciem connection string
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Skonfiguruj potok ??da? HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
