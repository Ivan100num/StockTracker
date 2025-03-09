using StockTracker2.Interfaces;
using StockTracker2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // Add support for APIs

// Example: Register additional services
builder.Services.AddScoped<IStockService, StockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Example: Custom logging middleware
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request for {context.Request.Path} received");
    await next();
});

// Configure default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stocks}/{action=Index}/{id?}");


// Enable Web API routes
app.MapControllers();

app.Run();
