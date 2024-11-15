using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using st10293982CLD6212POE1.Services;

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

//Register FileShareService with configuration
builder.Services.AddSingleton<FileShareService>(sp =>
{
    var connectionString = configuration.GetConnectionString("AzureStorage");
    return new FileShareService(connectionString, "productshare");
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();