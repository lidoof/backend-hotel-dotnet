namespace HotelDDD.Domain.Customer
{
    public interface ICustomerRepository
    {
        Task<Guid> AddAsync(Customer customer);
        Task<Customer> GetAsync(Guid customerId);
        Task<Customer> GetByNameAsync(string customerName);
        Task UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(Guid customerId);
        Task<Customer> GetByEmailAsync(string email);
    }
}
