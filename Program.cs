using Blazored.LocalStorage;
using EnterpriseBlog;
using EnterpriseBlog.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.RegisterObjectsByLifeTime(new[] { typeof(BlogClientAssemblyMarker).Assembly });

//3rd party Libraries
builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();
