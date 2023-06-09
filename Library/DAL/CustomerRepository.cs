using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private IMapper _mapper;
        private bool _disposed = false;

        public CustomerRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public CustomerRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CustomerInfo> GetCustomers(int count)
        {
            List<Customer> customers;
            if (count > 0)
            {
                customers = _context.Customers.Take(count).ToList();
            }
            else
            {
                customers = _context.Customers.ToList();
            }

            return _mapper.Map<List<Customer>, List<CustomerInfo>>(customers);
        }

        public List<CustomerInfo> GetCustomersBanned(int count)
        {
            List<Customer> customers;
            if (count > 0)
            {
                customers = _context.Customers
                    .Include(customer => customer.Account)
                    .Where(customer => customer.Account.IsBanned == true)
                    .Take(count).ToList();
            }
            else
            {
                customers = _context.Customers
                    .Include(customer => customer.Account)
                    .Where(customer => customer.Account.IsBanned == true)
                    .ToList();
            }

            return _mapper.Map<List<Customer>, List<CustomerInfo>>(customers);
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