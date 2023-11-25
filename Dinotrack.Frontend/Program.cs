using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Dinotrack.Frontend;
using Dinotrack.Frontend.AuthorizationProviders;
using Dinotrack.Frontend.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlBack = "https://dinotrackback.azurewebsites.net";
//var urlBack = "https://localhost:7102/";

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(urlBack) });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x =>
    x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x =>
    x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();

await builder.Build().RunAsync();