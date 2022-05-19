global using BaurezGames.Shared.MoreOrLessGame;
global using BaurezGames.Shared.AdditionGame;
using BaurezGames.Shared.Dicolink;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddSingleton<MoreOrLessGameService>(x=>new MoreOrLessGameService(builder.Environment.ContentRootPath));
builder.Services.AddSingleton<AdditionGameService>(x => new AdditionGameService(builder.Environment.ContentRootPath));
builder.Services.AddSingleton<IDicolinkService>(x => new DicolinkService(new HttpClient(){BaseAddress = new Uri($"https://api.dicolink.com") }));


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
