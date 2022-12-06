using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.Data;
using TaxCalculator.Application.TaxCalculation;
using TaxCalculator.Application.TaxCalculation.Queries.CalculateTax;
using TaxCalculator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.AllowAnyOrigin();
}));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
builder.Services.AddScoped<ITaxBandRepository, TaxBandRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(TaxCalculationHandler).Assembly);
builder.Services.AddDbContext<TaxDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("TaxDatabase");
    options.UseMySql(connectionString,
                     ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.MapControllerRoute("default",
                       "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();