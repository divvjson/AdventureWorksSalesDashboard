using AdventureWorksSalesDashboard;
using AdventureWorksSalesDashboard.Entities;
using AdventureWorksSalesDashboard.Pages.Regional;
using AdventureWorksSalesDashboard.Services;
using AdventureWorksSalesDashboard.Utilities;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<AdventureWorksContext>(options =>
{
    var dataSource = builder.Environment.IsDevelopment() ? Secrets.GetValue("DbDataSource") : "localhost";
    var initialCatalog = Secrets.GetValue("DbInitialCatalog");
    var userId = Secrets.GetValue("DbUserID");
    var password = Secrets.GetValue("DbPassword");

    options
        .UseLazyLoadingProxies()
        .UseSqlServer($"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};Encrypt=False");
});

builder.Services
    .AddRazorComponents(options =>
    {
        options.DetailedErrors = builder.Environment.IsDevelopment();
    })
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddScoped<DrawerService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<RegionalFilterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

if (app.Environment.IsDevelopment())
{
    app.Urls.Add("https://0.0.0.0:4200");
    app.Urls.Add("https://0.0.0.0:7076");
}

app.Run();
