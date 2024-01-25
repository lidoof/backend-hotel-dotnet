using HotelDDD.Domain.Room;
using HotelDDD.Domain.RoomService;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelDDD.Tests.Domain.Room
{
    public class RoomServiceTests
    {
        private MockRepository mockRepository;
        private Mock<IRoomRepository> mockRoomRepository;

        public RoomServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockRoomRepository = this.mockRepository.Create<IRoomRepository>();
        }

        private RoomService CreateService()
        {
            return new RoomService(this.mockRoomRepository.Object);
        }

        [Fact]
        public async Task AddRoomAsync_WithValidRoom_ShouldReturnRoomId()
        {
            // Arrange
            var service = this.CreateService();
            var room = new HotelDDD.Domain.Room.Room(/* paramètres du constructeur */);
            var expectedRoomId = Guid.NewGuid();

            this.mockRoomRepository
                .Setup(repo => repo.AddAsync(room))
                .ReturnsAsync(expectedRoomId);

            // Act
            var result = await service.AddRoomAsync(room);

            // Assert
            Assert.Equal(expectedRoomId, result);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetRoomAsync_WithValidId_ShouldReturnRoom()
        {
            // Arrange
            var service = this.CreateService();
            var roomId = Guid.NewGuid();
            var expectedRoom = new HotelDDD.Domain.Room.Room(/* paramètres du constructeur */);

            this.mockRoomRepository
                .Setup(repo => repo.GetAsync(roomId))
                .ReturnsAsync(expectedRoom);

            // Act
            var result = await service.GetRoomAsync(roomId);

            // Assert
            Assert.Equal(expectedRoom, result);

            this.mockRepository.VerifyAll();
        }


        [Fact]
        public async Task GetRoomsByTypesAsync_WithValidTypes_ShouldReturnRooms()
        {
            // Arrange
            var service = this.CreateService();

            // Let's assume we're testing fetching Standard and Superior rooms
            var roomTypes = new List<RoomType> { RoomType.Standard, RoomType.Superior };

            // Expected rooms based on your seeded data
            var expectedRooms = new List<HotelDDD.Domain.Room.Room>
            {
                new HotelDDD.Domain.Room.Room(Guid.NewGuid(), RoomType.Standard, 50, new List<string> { "Lit 1 place", "Wifi", "TV" }),
                new HotelDDD.Domain.Room.Room(Guid.NewGuid(), RoomType.Superior, 100, new List<string> { "Lit 2 places", "Wifi", "TV écran plat", "Minibar", "Climatiseur" })
            };

            this.mockRoomRepository
                .Setup(repo => repo.GetRoomsByTypesAsync(roomTypes))
                .ReturnsAsync(expectedRooms);

            // Act
            var result = await service.GetRoomsByTypesAsync(roomTypes);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRooms.Count, result.Count); 
            this.mockRepository.VerifyAll();
        }

    }
}
