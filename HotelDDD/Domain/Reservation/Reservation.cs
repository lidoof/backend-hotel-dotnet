using HotelDDD.Domain.Room;

namespace HotelDDD.Domain.Reservation
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<ReservationRoom> ReservationRooms { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public decimal Deposit { get; private set; } // Acompte versé lors de la création de la réservation
        public ReservationStatus Status { get; private set; }

        // Constructeur pour EF Core
        public Reservation() { }

        // Constructeur principal utilisé pour créer une réservation
        public Reservation(Guid customerId, List<ReservationRoom> reservationRooms, DateTime checkInDate, DateTime checkOutDate, decimal deposit)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Deposit = deposit; // Stockez l'acompte
            Status = ReservationStatus.Pending;

            ReservationRooms = reservationRooms ?? throw new ArgumentNullException(nameof(reservationRooms));

            Validate();
        }

        private void Validate()
        {
            if (CheckInDate >= CheckOutDate || CheckInDate < DateTime.Now)
            {
                throw new InvalidOperationException("Les dates de réservation ne sont pas valides.");
            }

            if (ReservationRooms == null || !ReservationRooms.Any())
            {
                throw new InvalidOperationException("Au moins une chambre doit être réservée.");
            }

            // Vérifie que le RoomType spécifié dans la réservation correspond aux chambres réellement réservées
            var reservedRoomTypes = ReservationRooms.Select(rr => rr.RoomType).Distinct().ToList();
            if (reservedRoomTypes.Count != 1 || reservedRoomTypes.First() != ReservationRooms.First().RoomType)
            {
                throw new InvalidOperationException("Le type de chambre dans la réservation ne correspond pas aux chambres réservées.");
            }

            if (Deposit <= 0)
            {
                throw new InvalidOperationException("Le montant de l'acompte pour la réservation doit être positif.");
            }
        }

        public void ConfirmReservation()
        {
            if (Status == ReservationStatus.Confirmed)
            {
                throw new InvalidOperationException("La réservation est déjà confirmée.");
            }

            Status = ReservationStatus.Confirmed;
        }

        public void CancelReservation()
        {
            if (Status == ReservationStatus.Cancelled)
            {
                throw new InvalidOperationException("La réservation est déjà annulée.");
            }

            Status = ReservationStatus.Cancelled;
        }
    }

    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class ReservationRoom
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public RoomType RoomType { get; set; } 
    }
}


