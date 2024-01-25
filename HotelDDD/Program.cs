using HotelDDD.Database;
using HotelDDD.Database.Customer;
using HotelDDD.Database.Reservation;
using HotelDDD.Database.Room;
using HotelDDD.Database.Wallet;
using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using HotelDDD.Domain.RoomService;
using HotelDDD.Domain.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configure JsonSerializerOptions here
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Configuration des services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Ajouter le service CustomerService et son interface ICustomerRepository
builder.Services.AddScoped<ICustomerRepository, DatabaseCustomerRepository>(); 
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<IRoomRepository, DatabaseRoomRepository>();
builder.Services.AddScoped<RoomService>();

builder.Services.AddScoped<IWalletRepository, DatabaseWalletRepository>();
builder.Services.AddScoped<WalletService>();


builder.Services.AddScoped<IReservationRepository, DatabaseReservationRepository>(); // Assurez-vous d'avoir une implémentation DatabaseReservationRepository
builder.Services.AddScoped<ReservationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Crée la base de données si elle n'existe pas
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
