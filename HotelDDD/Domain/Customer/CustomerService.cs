// XYZHotel.Domain/Customer/CustomerService.cs

using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Wallet;
using System;
using System.Threading.Tasks;

namespace HotelDDD.Domain.Customer
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWalletRepository _walletRepository;

        public CustomerService(ICustomerRepository customerRepository, IWalletRepository walletRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task<Guid> CreateCustomerAsync(Customer customer)
        {
            // Génération d'un WalletId pour le nouveau client
            var walletId = Guid.NewGuid();
            customer.SetWalletId(walletId);

            // Ajoutez ici des validations ou de la logique métier si nécessaire
            var customerId = await _customerRepository.AddAsync(customer);

            // Création d'un portefeuille associé au nouveau client
            var wallet = new Wallet.Wallet(walletId, 0, Currency.Euro); 
            await _walletRepository.AddAsync(wallet);

            return customerId;
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            // Logique pour récupérer le client
            return await _customerRepository.GetAsync(customerId);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            // Logique de mise à jour du client
            await _customerRepository.UpdateAsync(customer);
            return customer; // Retournez le client mis à jour si nécessaire
        }

        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            // Logique de suppression du client
            return await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);

            if (customer != null && customer.MotDePasse == password)
            {
                // Le mot de passe correspond
                return true;
            }

            // L'e-mail n'existe pas ou le mot de passe ne correspond pas
            return false;
        }

        public async Task<Wallet.Wallet> GetCustomerWalletAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);

            if (customer != null && customer.WalletId.HasValue)
            {
                return await _walletRepository.GetWalletAsync(customer.WalletId.Value);
            }

            return null;
        }
    }
}
