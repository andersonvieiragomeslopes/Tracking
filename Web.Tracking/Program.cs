using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Refit;
using Shared.Mobile.Services.Requests;
using Web.Tracking.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<AuthTokenHandler>();
var url = new Uri(builder.Configuration["BackendUrl"]!);
builder.Services.AddRefitClient<IAuthService>().ConfigureHttpClient(c => c.BaseAddress = url);
builder.Services.AddRefitClient<IOrderService>().ConfigureHttpClient(c => c.BaseAddress = url).AddHttpMessageHandler<AuthTokenHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
