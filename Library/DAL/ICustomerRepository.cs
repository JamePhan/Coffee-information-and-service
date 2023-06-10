using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface ICustomerRepository : IDisposable
    {
        List<CustomerInfo> GetCustomers(int count);

        List<CustomerInfo> GetCustomers(string name);

        List<CustomerInfo> GetCustomersBanned(int count);

        Customer? GetCustomer(int id);

        Customer? GetCustomerByAccountId(int id);

        Customer? GetCustomerByEmail(string email);

        void InsertCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int id);

        void Save();
    }
}