using HotelDDD.Domain.Wallet;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelDDD.Tests.Domain.Wallet
{

    public class WalletServiceTests
    {
        private readonly Mock<IWalletRepository> mockWalletRepository;

        public WalletServiceTests()
        {
            this.mockWalletRepository = new Mock<IWalletRepository>(MockBehavior.Strict);
        }

        private WalletService CreateService()
        {
            return new WalletService(this.mockWalletRepository.Object);
        }

        private HotelDDD.Domain.Wallet.Wallet CreateWallet()
        {
            // Create a test wallet
            return new HotelDDD.Domain.Wallet.Wallet(Guid.NewGuid(), 100, Currency.Euro);
        }

        [Fact]
        public async Task GetWalletAsync_ValidId_ShouldReturnWallet()
        {
            // Arrange
            var service = this.CreateService();
            var walletId = Guid.NewGuid();
            var expectedWallet = CreateWallet();

            this.mockWalletRepository
                .Setup(repo => repo.GetWalletAsync(walletId))
                .ReturnsAsync(expectedWallet);

            // Act
            var result = await service.GetWalletAsync(walletId);

            // Assert
            Assert.Equal(expectedWallet, result);
            this.mockWalletRepository.Verify(repo => repo.GetWalletAsync(walletId), Times.Once);
        }

        [Fact]
        public async Task UpdateWalletAsync_ValidWallet_ShouldCallUpdateWallet()
        {
            // Arrange
            var service = this.CreateService();
            var wallet = CreateWallet();

            this.mockWalletRepository
                .Setup(repo => repo.UpdateWalletAsync(wallet))
                .Returns(Task.CompletedTask);

            // Act
            await service.UpdateWalletAsync(wallet);

            // Assert
            this.mockWalletRepository.Verify(repo => repo.UpdateWalletAsync(wallet), Times.Once);
        }
    }
}
