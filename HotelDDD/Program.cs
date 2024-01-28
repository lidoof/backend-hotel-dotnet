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

// Ajout des contrôleurs et configuration des options JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Configuration des services pour Entity Framework et les repositories
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, DatabaseCustomerRepository>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<IRoomRepository, DatabaseRoomRepository>();
builder.Services.AddScoped<RoomService>();

builder.Services.AddScoped<IWalletRepository, DatabaseWalletRepository>();
builder.Services.AddScoped<WalletService>();

builder.Services.AddScoped<IReservationRepository, DatabaseReservationRepository>();
builder.Services.AddScoped<ReservationService>();

// Ajout du service CORS pour autoriser toutes les origines, méthodes et en-têtes (pour le développement)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Swagger pour la documentation API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration de l'application pour l'utilisation de Swagger et Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pour le routage des requêtes
app.UseRouting();

// Utilisation du service CORS avec la politique "AllowAll"
app.UseCors("AllowAll");

// Middleware pour l'autorisation
app.UseAuthorization();

// Configuration de l'application pour l'utilisation des Controllers
app.MapControllers();

// Création de la base de données si elle n'existe pas (à utiliser avec prudence en production)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Crée la base de données si elle n'existe pas
}

// Lancement de l'application
app.Run();