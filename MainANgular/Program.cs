using Microsoft.OpenApi.Models;
using MainANgular.Data;
using MainANgular.Domain.Entities;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

//Ad db context
builder.Services.AddDbContext<Entities>(options =>
options.UseInMemoryDatabase(databaseName:"Flights"),
ServiceLifetime.Singleton
);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c=>
{
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7011"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});
// add to independency injection ! like db
builder.Services.AddSingleton<Entities>();
var app = builder.Build();


//after builder build

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var random=new Random();
Flight[] flightsToSeed = new Flight[]
{
    new( Guid.NewGuid(),
                   "Amercian Airlines",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Los ANgeles",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Istambul",DateTime.Now.AddHours(random.Next(1,3))),
                   2),
               new( Guid.NewGuid(),
                   "AIr Lingus",
                   random.Next(90,5000).ToString(),
                   new TimePlace("London",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Dublin",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "British Airlines",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Manchaster",DateTime.Now.AddHours(random.Next(1,2))),
                   new TimePlace("Belfast",DateTime.Now.AddHours(random.Next(1,2))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "Air Lingus",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Los ANgeles",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Istambul",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "AIr Poland",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Derby",DateTime.Now.AddHours(random.Next(1,4))),
                   new TimePlace("Olsztyn",DateTime.Now.AddHours(random.Next(1,4))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "Lufthansas",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Krakow",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Wasraw",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400))
};
entities.Flights.AddRange(flightsToSeed);

entities.SaveChanges();
//////
app.UseCors(builder =>builder.WithOrigins("*")
.AllowAnyMethod()
.AllowAnyHeader()
);
/////
app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
