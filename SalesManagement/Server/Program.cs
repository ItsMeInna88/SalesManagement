using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagement.Server.DatabaseAccess;
using SalesManagement.Server.Mappers;
using SalesManagement.Server.MappersDTO;
using SalesManagement.Server.Services;
using SalesManagement.Shared.IRepository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register DbContext and Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementSalesDbContext")));

#region DependencyInjection
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IWindowRepository, WindowRepository>();
builder.Services.AddScoped<ISubElementRepository, SubElementRepository>();
builder.Services.AddScoped<MappersToDTO>();
builder.Services.AddScoped<MappersFromDTO>();
#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
