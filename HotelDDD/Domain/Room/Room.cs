using HotelDDD.Domain.Room;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelDDD.Domain.Room
{
    public enum RoomType
    {
        Standard,
        Superior,
        Suite
    }

    [Table("Room")]
    public class Room
    {
        public Guid Id { get; private set; }

        [Required]
        public RoomType Type { get; private set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; private set; }

        // Change List<string> to string
        public string Amenities { get; private set; }

        // Constructeur par défaut sans paramètres pour Entity Framework Core
        public Room()
        {
        }

        public Room(Guid id, RoomType type, decimal pricePerNight, List<string> amenities)
        {
            Id = id;
            Type = type;
            PricePerNight = pricePerNight;
            Amenities = ConvertAmenitiesToString(amenities);
            Validate();
        }

        private void Validate()
        {
            ValidateType(Type);
            ValidatePrice(PricePerNight);
        }

        private void ValidateType(RoomType type)
        {
            if (!Enum.IsDefined(typeof(RoomType), type))
            {
                throw new ArgumentException("Le type de chambre n'est pas valide.");
            }
        }

        private void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Le prix par nuit ne peut pas être négatif.");
        }

        // Méthode pour convertir la liste d'aménagements en chaîne
        private string ConvertAmenitiesToString(List<string> amenities)
        {
            return amenities != null ? string.Join(",", amenities) : null;
        }

        // Méthode pour convertir la chaîne d'aménagements en liste
        private List<string> ConvertStringToAmenities(string amenities)
        {
            return !string.IsNullOrEmpty(amenities) ? new List<string>(amenities.Split(',')) : new List<string>();
        }
    }
}

