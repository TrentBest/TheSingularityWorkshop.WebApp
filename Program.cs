using TheSingularityWorkshop.WebApp.Components;
using TheSingularityWorkshop.WebApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<WebAppMicroBundleCatalog>();
builder.Services.AddSingleton<WebAppCosRuntime>();
builder.Services.AddSingleton<WebAppExperienceRuntime>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

_ = app.Services.GetRequiredService<WebAppCosRuntime>();
_ = app.Services.GetRequiredService<WebAppExperienceRuntime>();

app.Run();
