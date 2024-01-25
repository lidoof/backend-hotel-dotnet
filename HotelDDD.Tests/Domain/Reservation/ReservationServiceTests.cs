using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using HotelDDD.Domain.Wallet;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelDDD.Tests.Domain.Reservation
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> mockReservationRepository;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IRoomRepository> mockRoomRepository;
        private readonly Mock<IWalletRepository> mockWalletRepository;

        public ReservationServiceTests()
        {
            this.mockReservationRepository = new Mock<IReservationRepository>(MockBehavior.Strict);
            this.mockCustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
            this.mockRoomRepository = new Mock<IRoomRepository>(MockBehavior.Strict);
            this.mockWalletRepository = new Mock<IWalletRepository>(MockBehavior.Strict);
        }

        private ReservationService CreateService() => new ReservationService(
            this.mockReservationRepository.Object,
            this.mockCustomerRepository.Object,
            this.mockRoomRepository.Object,
            this.mockWalletRepository.Object);

        private HotelDDD.Domain.Customer.Customer CreateCustomer()
        {
            // Create a test customer with a wallet
            var  customer = new  HotelDDD.Domain.Customer.Customer(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", "Password123");
            customer.SetWalletId(Guid.NewGuid());
            return customer;
        }

        private HotelDDD.Domain.Wallet.Wallet CreateWallet(decimal balance)
        {
            // Create a test wallet with the specified balance
            return new HotelDDD.Domain.Wallet.Wallet(Guid.NewGuid(), balance, Currency.Euro);
        }


        private List<ReservationRoom> CreateReservationRooms()
        {
        
            return new List<ReservationRoom>
            {
                new ReservationRoom
                {
                   
                    ReservationId = Guid.NewGuid(),
                    RoomType = RoomType.Standard 
                },
               
            };
        }

        [Fact]
        public async Task CreateReservationAsync_ValidInputs_ShouldCreateReservation()
        {
            // Arrange
            var service = this.CreateService();
            var customer = CreateCustomer();
            var wallet = CreateWallet(500);
            var reservationRooms = CreateReservationRooms(); // Utilisez la liste non vide
            DateTime checkInDate = DateTime.Now.AddDays(10);
            int numberOfNights = 2;
            decimal roomPrice = 100m;
            decimal totalCost = roomPrice * numberOfNights * reservationRooms.Count;
            decimal depositAmount = totalCost * ReservationService.DepositPercentage;

            this.mockCustomerRepository
                .Setup(repo => repo.GetAsync(customer.Id))
                .ReturnsAsync(customer);

            this.mockWalletRepository
                .Setup(repo => repo.GetWalletAsync(customer.WalletId.Value))
                .ReturnsAsync(wallet);

            this.mockRoomRepository
                .Setup(repo => repo.GetRoomPricesByTypes(It.IsAny<List<RoomType>>()))
                .ReturnsAsync(new Dictionary<RoomType, decimal> { { RoomType.Standard, roomPrice } });

            this.mockWalletRepository
                .Setup(repo => repo.UpdateWalletAsync(It.IsAny<HotelDDD.Domain.Wallet.Wallet>()))
                .Returns(Task.CompletedTask);

            this.mockReservationRepository
                .Setup(repo => repo.AddAsync(It.IsAny<HotelDDD.Domain.Reservation.Reservation>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await service.CreateReservationAsync(
                customer.Id,
                reservationRooms,
                checkInDate,
                numberOfNights);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ReservationStatus.Pending, result.Status);
            this.mockWalletRepository.Verify(repo => repo.UpdateWalletAsync(It.IsAny<HotelDDD.Domain.Wallet.Wallet>()), Times.Once);
            this.mockReservationRepository.Verify(repo => repo.AddAsync(It.IsAny<HotelDDD.Domain.Reservation.Reservation>()), Times.Once);
        }

    }
}
