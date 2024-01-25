using HotelDDD.Domain.Room;
using HotelDDD.Domain.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace HotelDDD.HTTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService; // Utilisez RoomService

        public RoomController(RoomService roomService) // Injectez RoomService
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> SearchRooms([FromQuery] string roomType)
        {
            // Validation supplémentaire si nécessaire
            if (string.IsNullOrEmpty(roomType))
            {
                return BadRequest("Le type de chambre doit être spécifié.");
            }

            // Conversion de la chaîne roomType en valeur énumérée RoomType
            if (!Enum.TryParse<RoomType>(roomType, true, out RoomType parsedRoomType))
            {
                return BadRequest("Type de chambre invalide.");
            }

            // Convertissez parsedRoomType en une liste contenant un seul élément
            var roomTypes = new List<RoomType> { parsedRoomType };

            // Utilisez RoomService pour implémenter la logique de recherche en fonction du type de chambre
            var rooms = await _roomService.GetRoomsByTypesAsync(roomTypes);

            if (rooms == null || !rooms.Any())
            {
                return NotFound($"Aucune chambre de type '{parsedRoomType}' n'a été trouvée.");
            }

            // Convertir chaque Room en RoomDto pour manipuler le format des commodités (amenities)
            var roomDtos = rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                Type = r.Type,
                PricePerNight = r.PricePerNight,
                Amenities = r.Amenities != null ? r.Amenities.Split(',').ToList() : new List<string>()
            });

            return Ok(roomDtos); // Renvoie la liste des chambres sous forme de réponse JSON
        }

        // DTO (Data Transfer Object) pour la chambre
        public class RoomDto
        {
            public Guid Id { get; set; }
            public RoomType Type { get; set; }
            public decimal PricePerNight { get; set; }
            public List<string> Amenities { get; set; }
        }
    }
}
