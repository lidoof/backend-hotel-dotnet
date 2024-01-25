using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using Microsoft.EntityFrameworkCore;

namespace HotelDDD.Database.Room
{
    public class DatabaseRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public DatabaseRoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Domain.Room.Room room)
        {
            _dbContext.Rooms.Add(room);
            await _dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<Domain.Room.Room> GetAsync(Guid roomId)
        {
            return await _dbContext.Rooms
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task UpdateAsync(Domain.Room.Room room)
        {
            _dbContext.Entry(room).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid roomId)
        {
            var room = await _dbContext.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _dbContext.Rooms.Remove(room);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Domain.Room.Room>> GetRoomsByTypesAsync(List<RoomType> roomTypes)
        {
            var rooms = await _dbContext.Rooms
                .Where(r => roomTypes.Contains(r.Type))
                .ToListAsync();

            return rooms;
        }

        public async Task<Dictionary<RoomType, decimal>> GetRoomPricesByTypes(List<RoomType> roomTypes)
        {
            var roomPrices = await _dbContext.Rooms
                .Where(r => roomTypes.Contains(r.Type))
                .ToDictionaryAsync(r => r.Type, r => r.PricePerNight);

            return roomPrices;
        }

    }
}
