using HotelDDD.Domain.Room;

namespace HotelDDD.Domain.RoomService
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<Guid> AddRoomAsync(Domain.Room.Room room)
        {
            // Ajoutez ici des validations ou de la logique métier si nécessaire
            return await _roomRepository.AddAsync(room);
        }

        public async Task<Domain.Room.Room> GetRoomAsync(Guid roomId)
        {
            return await _roomRepository.GetAsync(roomId);
        }

        // Ajoutez une méthode pour obtenir toutes les chambres d'une liste de types
        public async Task<List<Domain.Room.Room>> GetRoomsByTypesAsync(List<RoomType> roomTypes)
        {
            return await _roomRepository.GetRoomsByTypesAsync(roomTypes);
        }

        // ... autres méthodes de service en fonction des besoins ...
    }
}
