namespace HotelDDD.Domain.Room
{
    public interface IRoomRepository
    {
        // Ajoute une nouvelle chambre à la base de données.
        Task<Guid> AddAsync(Room room);

        // Récupère une chambre spécifique par son identifiant.
        Task<Room> GetAsync(Guid roomId);

        // Met à jour les détails d'une chambre dans la base de données.
        Task UpdateAsync(Room room);

        // Supprime une chambre de la base de données.
        Task<bool> DeleteAsync(Guid roomId);

        // Recherche des chambres par un type spécifique.
        Task<List<Domain.Room.Room>> GetRoomsByTypesAsync(List<RoomType> roomTypes);

        Task<Dictionary<RoomType, decimal>> GetRoomPricesByTypes(List<RoomType> roomTypes);
    }

}



