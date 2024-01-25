using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelDDD.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Customer.Customer> Customers { get; set; }
        public DbSet<Domain.Room.Room> Rooms { get; set; }
        public DbSet<Domain.Wallet.Wallet> Wallets { get; set; }
        public DbSet<Domain.Reservation.Reservation> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Customer.Customer>().ToTable("Customer");
            modelBuilder.Entity<Domain.Wallet.Wallet>().ToTable("Wallet");

            // Configuration pour Room avec conversion de RoomType
            modelBuilder.Entity<Domain.Room.Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(e => e.Id);
                var roomTypeConverter = new EnumToStringConverter<RoomType>();
                entity.Property(e => e.Type).HasConversion(roomTypeConverter);
            });

            // Configuration pour Reservation
            modelBuilder.Entity<Domain.Reservation.Reservation>(entity =>
            {
                entity.ToTable("Reservation");
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.ReservationRooms)
                      .WithOne(rr => rr.Reservation)
                      .HasForeignKey(rr => rr.ReservationId);
            });

            // Configuration pour ReservationRoom
            modelBuilder.Entity<ReservationRoom>(entity =>
            {
                entity.ToTable("ReservationRoom");
                entity.HasKey(rr => new { rr.ReservationId, rr.RoomType }); 
            });

            // Configuration de données initiales si nécessaire
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            Guid standardRoomId = Guid.NewGuid();
            Guid superiorRoomId = Guid.NewGuid();
            Guid suiteRoomId = Guid.NewGuid();

            modelBuilder.Entity<Domain.Room.Room>().HasData(
                new Domain.Room.Room(standardRoomId, RoomType.Standard, 50, new List<string> { "Lit 1 place", "Wifi", "TV" }),
                new Domain.Room.Room(superiorRoomId, RoomType.Superior, 100, new List<string> { "Lit 2 places", "Wifi", "TV écran plat", "Minibar", "Climatiseur" }),
                new Domain.Room.Room(suiteRoomId, RoomType.Suite, 200, new List<string> { "Lit 2 places", "Wifi", "TV écran plat", "Minibar", "Climatiseur", "Baignoire", "Terrasse" })
            );
        }
    }
}


