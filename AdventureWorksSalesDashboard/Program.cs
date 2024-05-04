using AdventureWorksSalesDashboard;
using AdventureWorksSalesDashboard.Entities;
using AdventureWorksSalesDashboard.Utilities;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<AdventureWorksContext>(options =>
{
    var dataSource = Secrets.GetValue("DbDataSource");
    var initialCatalog = Secrets.GetValue("DbInitialCatalog");
    var userId = Secrets.GetValue("DbUserID");
    var password = Secrets.GetValue("DbPassword");

    options
        .UseLazyLoadingProxies()
        .UseSqlServer($"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};Encrypt=False");
});

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

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
}

app.Run();
