using Blazored.LocalStorage;
using EnterpriseBlog;
using EnterpriseBlog.DependencyInjection;
using EnterpriseBlog.Repositories;
using EnterpriseBlog.Services;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



//3rd party Libraries
builder.Services.AddBlazoredLocalStorage();

//Options
builder.Services.Configure<DevOptions>(options =>
{
    options.IsDevFallbackEnabled = builder.HostEnvironment.IsDevelopment();
});



#if DEBUG
builder.Services.AddScoped<IBlogPostRepository, MockBlogPostRepository>();
#else
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
#endif

builder.Services.AddScoped<IBlogPostService, BlogPostService>();

builder.Services.RegisterVMsByLifeTime(typeof(Program).Assembly);

await builder.Build().RunAsync();
