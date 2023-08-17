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
        List<CustomerInfo> GetCustomers();

        List<CustomerInfo> GetCustomers(string name);

        List<CustomerInfo> GetCustomersBanned();

        Customer? GetCustomer(int id);

        Customer? GetCustomerByAccountId(int id);

        Customer? GetCustomerByEmail(string email);

        CustomerInfo? GetCustomerInfo(int id);

        void InsertCustomer(Customer customer);

        void UpdateCustomer(CustomerInfo customer, int accountId);

        void DeleteCustomer(int id);

        void DeleteCustomerByAccountId(int id);

        void Save();
    }
}