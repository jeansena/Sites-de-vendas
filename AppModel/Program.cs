using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppModel.Data;
using AppModel.Models;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using AppModel.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppModelContext>(options => {
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppModel;password=12345678;");
});


//serviço de indepencia da aplicaçao
builder.Services.AddScoped<SeedingService>();

builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();



// Add services to the container.
builder.Services.AddControllersWithViews();
//SeedingService seedingService;

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};



var app = builder.Build();
//SeedingService seedingService;

//seedingService.Seed();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    //seedingService.Seed();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseRequestLocalization(localizationOptions);

//seedingService.Seed();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();