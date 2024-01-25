namespace HotelDDD.Domain.Reservation
{
    public interface IReservationRepository
    {

        Task<Guid> AddAsync(Reservation reservation);
        Task<Reservation> GetAsync(Guid reservationId);
        Task UpdateAsync(Reservation reservation);
        Task<bool> DeleteAsync(Guid reservationId);
    }
}
