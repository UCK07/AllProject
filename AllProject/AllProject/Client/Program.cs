using AllProject.Client;
using AllProject.Client.Services;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RestSharp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<CommonService>();
builder.Services.AddSingleton<MovieService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSweetAlert2();
builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Default;
});
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton(sp => {
    var restClient = new RestClient(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    restClient.AddDefaultHeader("X-Requested-With", "XMLHttpRequest");

    return restClient;
}
);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
