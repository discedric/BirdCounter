using BirdCounter.Core;
using BirdCounter.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BirdDbContext>(options =>
{
	options.UseInMemoryDatabase(nameof(BirdDbContext));
});
builder.Services.AddScoped<BirdService>();
builder.Services.AddScoped<CountingSessionService>();
builder.Services.AddScoped<UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	var scope = app.Services.CreateScope();
	var dbContext = scope.ServiceProvider.GetRequiredService<BirdDbContext>();
	dbContext.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
