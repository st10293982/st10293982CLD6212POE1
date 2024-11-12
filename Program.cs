//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using st10293982CLD6212POE1.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Access the configuration object
//var configuration = builder.Configuration;

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Register BlobService with configuration
//builder.Services.AddSingleton(new BlobService(configuration.GetConnectionString("AzureStorage")));

//// Register TableStorageService with configuration
//builder.Services.AddSingleton(new TableStorageService(configuration.GetConnectionString("AzureStorage")));

//// Register QueueService with configuration
//builder.Services.AddSingleton<QueueService>(sp =>
//{
//    var connectionString = configuration.GetConnectionString("AzureStorage");
//    return new QueueService(connectionString, "order");
//});

////Register FileShareService with configuration
//builder.Services.AddSingleton<FileShareService>(sp =>
//{
//    var connectionString = configuration.GetConnectionString("AzureStorage");
//    return new FileShareService(connectionString, "productshare");
//});



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using st10293982CLD6212POE1.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using st10293982CLD6212POE1.Data;

var builder = WebApplication.CreateBuilder(args);

// Access the configuration object
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register BlobService with configuration
builder.Services.AddSingleton(new BlobService(configuration.GetConnectionString("AzureStorage")));

// Register TableStorageService with configuration
builder.Services.AddSingleton(new TableStorageService(configuration.GetConnectionString("AzureStorage")));

// Register QueueService with configuration
builder.Services.AddSingleton<QueueService>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureStorage");
    return new QueueService(connectionString, "order");
});

// Register FileShareService with configuration
builder.Services.AddSingleton<FileShareService>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureStorage");
    return new FileShareService(connectionString, "productshare");
});

builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Register HttpClient for dependency injection
builder.Services.AddHttpClient();

// Register Application Insights for monitoring
builder.Services.AddApplicationInsightsTelemetry(configuration["ApplicationInsights:InstrumentationKey"]);

// Register Azure SQL Database (or any other EF Core context if using one)
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("AzureSQLDatabase")));

// Optional: If you have other Azure services or clients, register them here

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

builder.Services.AddSession();
app.UseSession();

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

