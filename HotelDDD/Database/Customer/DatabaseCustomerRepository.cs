
using HotelDDD.Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace HotelDDD.Database.Customer
{
    public class DatabaseCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public DatabaseCustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Domain.Customer.Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<Domain.Customer.Customer> GetAsync(Guid customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task UpdateAsync(Domain.Customer.Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Domain.Customer.Customer> GetByNameAsync(string customerName)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Nom == customerName);
        }
        public async Task<Domain.Customer.Customer> GetByEmailAsync(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<bool> DeleteAsync(Guid customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
