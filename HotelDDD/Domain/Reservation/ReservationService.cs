using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using HotelDDD.Domain.Wallet;

namespace HotelDDD.Domain.Reservation
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IWalletRepository _walletRepository;

        public ReservationService(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IRoomRepository roomRepository, IWalletRepository walletRepository)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public const decimal DepositPercentage = 0.50m;

        public async Task<Reservation> CreateReservationAsync(Guid customerId, List<ReservationRoom> reservationRooms, DateTime checkInDate, int numberOfNights)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            if (customer == null)
            {
                throw new InvalidOperationException("Client introuvable.");
            }

            if (!customer.WalletId.HasValue)
            {
                throw new InvalidOperationException("Le client n'a pas de portefeuille associé.");
            }

            var wallet = await _walletRepository.GetWalletAsync(customer.WalletId.Value);
            if (wallet == null)
            {
                throw new InvalidOperationException("Portefeuille du client introuvable.");
            }

            var roomTypes = reservationRooms.Select(rr => rr.RoomType).Distinct().ToList();
            var totalCost = await CalculateTotalCost(roomTypes, numberOfNights);
            var depositAmount = totalCost * DepositPercentage;

            if (wallet.Balance < depositAmount)
            {
                throw new InvalidOperationException("Fonds insuffisants pour effectuer la réservation.");
            }

            wallet.DeductFunds(depositAmount); // Appel à DeductFunds
            await _walletRepository.UpdateWalletAsync(wallet);

            var reservation = new Reservation(customerId, reservationRooms, checkInDate, checkInDate.AddDays(numberOfNights), depositAmount);
            await _reservationRepository.AddAsync(reservation);

            return reservation;
        }

        public async Task ConfirmReservationAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetAsync(reservationId);
            if (reservation == null || reservation.Status != ReservationStatus.Pending)
            {
                throw new InvalidOperationException("Réservation introuvable ou déjà confirmée/annulée.");
            }

            var roomTypes = reservation.ReservationRooms.Select(rr => rr.RoomType).Distinct().ToList();
            var totalCost = await CalculateTotalCost(roomTypes, (reservation.CheckOutDate - reservation.CheckInDate).Days);
            var remainingAmount = totalCost - reservation.Deposit;

            var customer = await _customerRepository.GetAsync(reservation.CustomerId);
            var wallet = await _walletRepository.GetWalletAsync(customer.WalletId.Value);

            if (wallet.Balance < remainingAmount)
            {
                throw new InvalidOperationException("Fonds insuffisants pour confirmer la réservation.");
            }

            wallet.DeductFunds(remainingAmount); // Appel à DeductFunds
            await _walletRepository.UpdateWalletAsync(wallet);

            reservation.ConfirmReservation();
            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task CancelReservationAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetAsync(reservationId);
            if (reservation == null)
            {
                throw new InvalidOperationException("Réservation introuvable.");
            }

            reservation.CancelReservation();
            await _reservationRepository.UpdateAsync(reservation);
        }

        private async Task<decimal> CalculateTotalCost(List<RoomType> roomTypes, int numberOfNights)
        {
            var roomPrices = await _roomRepository.GetRoomPricesByTypes(roomTypes);
            return roomPrices.Sum(price => price.Value * numberOfNights);
        }
    }
}

