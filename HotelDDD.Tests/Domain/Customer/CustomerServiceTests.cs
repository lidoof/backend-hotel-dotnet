using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Wallet;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelDDD.Tests.Domain.Customer
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IWalletRepository> mockWalletRepository;

        public CustomerServiceTests()
        {
            this.mockCustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
            this.mockWalletRepository = new Mock<IWalletRepository>(MockBehavior.Strict);
        }

        private CustomerService CreateService() => new CustomerService(
            this.mockCustomerRepository.Object,
            this.mockWalletRepository.Object);

        private HotelDDD.Domain.Customer.Customer CreateCustomer()
        {
            // Create a test customer
            return new HotelDDD.Domain.Customer.Customer(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", "Password123");
        }

        [Fact]
        public async Task CreateCustomerAsync_ValidCustomer_ShouldCreateCustomerAndWallet()
        {
            // Arrange
            var service = this.CreateService();
            var customer = CreateCustomer();
            var walletId = Guid.NewGuid(); // Generate a new Guid for the wallet
            var wallet = new HotelDDD.Domain.Wallet.Wallet(walletId, 0, Currency.Euro);

            this.mockCustomerRepository
                .Setup(repo => repo.AddAsync(customer))
                .ReturnsAsync(customer.Id);

            this.mockWalletRepository
           .Setup(repo => repo.AddAsync(It.IsAny<HotelDDD.Domain.Wallet.Wallet>())) 
           .ReturnsAsync(walletId);

            // Act
            var result = await service.CreateCustomerAsync(customer);

            // Assert
            Assert.Equal(customer.Id, result);
            this.mockWalletRepository.Verify(repo => repo.AddAsync(It.IsAny<HotelDDD.Domain.Wallet.Wallet>()), Times.Once);
            this.mockCustomerRepository.Verify(repo => repo.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task GetCustomerAsync_ValidId_ShouldReturnCustomer()
        {
            // Arrange
            var service = this.CreateService();
            var customer = CreateCustomer();

            this.mockCustomerRepository
                .Setup(repo => repo.GetAsync(customer.Id))
                .ReturnsAsync(customer);

            // Act
            var result = await service.GetCustomerAsync(customer.Id);

            // Assert
            Assert.Equal(customer, result);
        }


        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ShouldReturnTrue()
        {
            // Arrange
            var service = this.CreateService();
            var customer = CreateCustomer();
            string email = customer.Email;
            string password = customer.MotDePasse;

            this.mockCustomerRepository
                .Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(customer);

            // Act
            var result = await service.AuthenticateAsync(email, password);

            // Assert
            Assert.True(result);
        }


    }
}
