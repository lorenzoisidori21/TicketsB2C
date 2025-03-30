using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketsB2C.Business;
using TicketsB2C.DataAccess;
using TicketsB2C.Models;
using TicketsB2C.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<TicketsB2CDbContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountStrategy, NoDiscountStrategy>();
var quantityDiscountConfig = new Dictionary<int, int> // configuration could be loaded from appsettings.json
{
    { 5, 5 }, // 5 tickets - 5% off
    { 10, 10 }, // 10 tickets - 10% off
    { 20, 20 }, // 20 tickets - 20% off
};
builder.Services.AddScoped<IDiscountStrategy>(sp => new QuantityDiscountStrategy(quantityDiscountConfig));
var transportTypeDiscountConfig = new Dictionary<int, int> // configuration could be loaded from appsettings.json
{
    { 2, 5 }, // transport type 2 (train) - 5% off
};
builder.Services.AddScoped<IDiscountStrategy>(sp => new TransportTypeDiscountStrategy(transportTypeDiscountConfig));
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ITicketsDal, TicketsDal>();
builder.Services.AddScoped<ITicketsBusiness, TicketsBusiness>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
