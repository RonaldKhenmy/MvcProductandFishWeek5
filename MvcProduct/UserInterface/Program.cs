using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcProduct.ApplicationCore.Interfaces;
using MvcProduct.ApplicationCore.Services;
using MvcProduct.Infrastructure.Data;
using System.Xml;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcFishContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcFishContext") ?? throw new InvalidOperationException("Connection string 'MvcFishContext' not found.")));
builder.Services.AddDbContext<MvcProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcProductContext") ?? throw new InvalidOperationException("Connection string 'MvcProductContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDaysPurchased, DaysPurchased>();



XMLSettings.LoadSettings();
var app = builder.Build();

XMLSettings.SaveSettings();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
//Adds map controllers for routing Http requests.
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
