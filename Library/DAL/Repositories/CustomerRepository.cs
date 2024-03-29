﻿using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
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

        public List<CustomerInfo> GetCustomers()
        {
            List<Customer> customers;

            customers = _context.Customers.Include(cust => cust.Account).Where(cust => cust.Account.IsBanned == false).ToList();

            return _mapper.Map<List<Customer>, List<CustomerInfo>>(customers);
        }

        public List<CustomerInfo> GetCustomers(string name)
        {
            List<Customer> customers = _context.Customers
                .Include(cust => cust.Account)
                .Where(cust => cust.Account.IsBanned == false)
                .Where(customer => customer.Name.ToLower().Contains(name.ToLower())).ToList();
            return _mapper.Map<List<Customer>, List<CustomerInfo>>(customers);
        }

        public List<CustomerInfo> GetCustomersBanned()
        {
            List<Customer> customers;

            customers = _context.Customers
                .Include(customer => customer.Account)
                .Where(customer => customer.Account.IsBanned == true)
                .ToList();

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

        public CustomerInfo? GetCustomerInfo(int id)
        {
            Customer? customer = GetCustomer(id);
            if (customer != null)
            {
                return _mapper.Map<Customer, CustomerInfo>(customer);
            }
            return null;
        }

        public void InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(CustomerInfo customer, int accountId)
        {
            Customer? checkExist = _context.Customers.SingleOrDefault(cust => cust.CustomerId.Equals(customer.CustomerId));
            if (checkExist != null)
            {
                try
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                    checkExist = _mapper.Map<CustomerInfo, Customer>(customer);
                    checkExist.AccountId = accountId;
                    _context.Entry(checkExist).State = EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Customer doesn't exist!");
            }
        }

        public void DeleteCustomer(int id)
        {
            Customer? toDelete = GetCustomer(id);
            if (toDelete != null)
            {
                List<Following> followDelete = _context.Followings.Where(follow => follow.CustomerId.Equals(id)).ToList();
                List<Schedule> scheduleDelete = _context.Schedules.Where(schedule => schedule.CustomerId.Equals(id)).ToList();
                _context.Followings.RemoveRange(followDelete);
                _context.Schedules.RemoveRange(scheduleDelete);
                _context.Customers.Remove(toDelete);
            }
        }

        public void DeleteCustomerByAccountId(int id)
        {
            Customer? toDelete = _context.Customers.SingleOrDefault(customer => customer.AccountId.Equals(id));
            if (toDelete != null)
            {
                try
                {
                    _context.Customers.Remove(toDelete);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Customer doesnt't exist!");
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