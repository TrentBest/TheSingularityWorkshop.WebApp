using TheSingularityWorkshop.WebApp.Components;
using TheSingularityWorkshop.WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents();

builder.Services.AddSingleton<WebAppMicroBundleCatalog>();
builder.Services.AddSingleton<WebAppCosRuntime>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

_ = app.Services.GetRequiredService<WebAppCosRuntime>();

app.Run();
