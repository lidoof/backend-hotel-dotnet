namespace HotelDDD.Domain.Wallet
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletAsync(Guid walletId);
        Task<Guid> AddAsync(Wallet wallet);

        Task UpdateWalletAsync(Wallet wallet);
    }
}
