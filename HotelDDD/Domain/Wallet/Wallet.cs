using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelDDD.Domain.Wallet
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public decimal Balance { get; private set; }

        [Column(TypeName = "nvarchar(24)")]
        public string PreferredCurrency { get; set; }

        protected Wallet()
        {
        }

        public Wallet(Guid id, decimal balance, Currency preferredCurrency)
        {
            Id = id;
            Balance = balance;
            PreferredCurrency = preferredCurrency.ToString();
        }

        public void AddFunds(decimal amount, Currency currency)
        {
            decimal amountInEuros = ConvertToEuros(amount, currency);
            Balance += amountInEuros;
            PreferredCurrency = currency.ToString();
        }

        public void DeductFunds(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Le montant à déduire doit être positif.");
            }

            if (Balance < amount)
            {
                throw new InvalidOperationException("Fonds insuffisants dans le portefeuille pour effectuer cette opération.");
            }

            Balance -= amount;
        }

        private decimal ConvertToEuros(decimal amount, Currency currency)
        {
            switch (currency)
            {
                case Currency.Euro:
                    return amount; // Pas besoin de conversion si la devise est déjà en euros
                case Currency.Dollar:
                    return amount * 0.85m; // Taux de conversion hypothétique
                case Currency.PoundSterling:
                    return amount * 1.18m; // Taux de conversion hypothétique
                case Currency.Yen:
                    return amount * 0.0077m; // Taux de conversion hypothétique
                case Currency.SwissFranc:
                    return amount * 0.92m; // Taux de conversion hypothétique
                default:
                    throw new ArgumentException("Devise non prise en charge pour la conversion en euros.");
            }
        }
    }
}
