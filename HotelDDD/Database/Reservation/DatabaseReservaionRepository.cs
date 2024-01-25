using HotelDDD.Domain.Reservation;
using Microsoft.EntityFrameworkCore;
namespace HotelDDD.Database.Reservation
{
    public class DatabaseReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _dbContext;

        public DatabaseReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Domain.Reservation.Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
            return reservation.Id;
        }

        public async Task<Domain.Reservation.Reservation> GetAsync(Guid reservationId)
        {
            return await _dbContext.Reservations
                .Include(r => r.ReservationRooms) // Inclut simplement les ReservationRooms
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task UpdateAsync(Domain.Reservation.Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation); // Cette méthode s'occupe de la mise à jour de l'entité principale et de ses relations
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid reservationId)
        {
            var reservation = await _dbContext.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
