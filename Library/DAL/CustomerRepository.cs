using Library.Models;

namespace Library.DAL
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private CoffeehouseSystemContext _context;
        private bool _disposed = false;

        public CustomerRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetCustomer(int id)
        {
            return _context.Customers.SingleOrDefault(customer => customer.CustomerId == id);
        }

        public Customer? GetCustomerByAccountId(int accountId)
        {
            return _context.Customers.SingleOrDefault(customer => customer.AccountId == accountId);
        }

        public Customer? GetCustomerByEmail(string email)
        {
            return _context.Customers.SingleOrDefault(customers => customers.Email.Equals(email));
        }

        public void InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteCustomer(int id)
        {
            Customer? toDelete = GetCustomer(id);
            if (toDelete != null)
            {
                _context.Customers.Remove(toDelete);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}