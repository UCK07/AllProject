using AllProject.Server.Helper;
using AllProject.Server.Services.Concreates;
using AllProject.Server.Services.Interfaces;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using RestSharp;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton(sp => {
    var restClient = new RestClient(new HttpClient { });
    restClient.AddDefaultHeader("X-Requested-With", "XMLHttpRequest");

    return restClient;
}
);
builder.Services.AddScoped<IImageUploadService, CloudinaryImageUploadService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<AppStateManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var migrationOptions = new MongoMigrationOptions
{
    MigrationStrategy = new DropMongoMigrationStrategy(),
    BackupStrategy = new CollectionMongoBackupStrategy()
};
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_110);
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseMongoStorage("mongodb://151.106.5.227:32770", "UmutDb", new MongoStorageOptions { MigrationOptions = migrationOptions, CheckConnection = false });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseHangfireServer();
app.UseHangfireDashboard();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
