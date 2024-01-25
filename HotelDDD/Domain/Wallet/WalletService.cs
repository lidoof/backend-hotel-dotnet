namespace HotelDDD.Domain.Wallet
{
    public class WalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task<Wallet> GetWalletAsync(Guid walletId)
        {
            return await _walletRepository.GetWalletAsync(walletId);
        }

        public async Task UpdateWalletAsync(Wallet wallet)
        {
            await _walletRepository.UpdateWalletAsync(wallet);
        }
    }
}
