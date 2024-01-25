using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDDD.Domain.Customer
{
    [Table("Customer")]
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Nom { get; private set; }
        public string Prenom { get; private set; }

        [EmailAddress]
        public string Email { get; private set; }
        public string MotDePasse { get; private set; }

        public Guid? WalletId { get; private set; }
        public Guid? ReservationId { get; private set; }

        public Customer(Guid id, string nom, string prenom, string email, string motDePasse)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            MotDePasse = motDePasse;
            WalletId = null;
            ReservationId = null;
            Validate();
        }

        private void Validate()
        {
            ValidateNom(Nom);
            ValidatePrenom(Prenom);

        }

        private void ValidateNom(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom du client ne peut pas être vide ou nul.");
        }

        private void ValidatePrenom(string prenom)
        {
            if (string.IsNullOrWhiteSpace(prenom))
                throw new ArgumentException("Le prénom du client ne peut pas être vide ou nul.");
        }


        public void SetWalletId(Guid walletId)
        {
            WalletId = walletId;
        }

        public void SetReservationId(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
  