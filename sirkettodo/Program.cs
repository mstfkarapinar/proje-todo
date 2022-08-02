using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using sirkettodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sirkettodo.Pages.EntityFramework;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<EntitySqlService>();
builder.Services.AddDbContext<MyConnection>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
