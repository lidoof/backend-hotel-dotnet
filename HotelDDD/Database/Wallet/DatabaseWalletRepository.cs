using HotelDDD.Domain.Wallet;
using Microsoft.EntityFrameworkCore;

namespace HotelDDD.Database.Wallet
{
    public class DatabaseWalletRepository : IWalletRepository
    {
        private readonly AppDbContext _context;

        public DatabaseWalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Domain.Wallet.Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            // Retourne l'ID du portefeuille après l'ajout
            return wallet.Id;
        }

        public async Task<Domain.Wallet.Wallet> GetWalletAsync(Guid walletId)
        {
            return await _context.Wallets.FindAsync(walletId);
        }

        public async Task UpdateWalletAsync(Domain.Wallet.Wallet wallet)
        {
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
