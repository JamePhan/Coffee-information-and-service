using AutoMapper;
using Library.DAL.Interfaces;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library.DAL.Repositories
{
    public class WaitingRepository : IWaitingRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public WaitingRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<WaitingInfo> GetWaitings()
        {
            List<Waiting> waits = _context.Waitings.ToList();
            return _mapper.Map<List<Waiting>, List<WaitingInfo>>(waits);
        }

        public WaitingInfo? GetWaiting(int id)
        {
            Waiting? waiting = _context.Waitings.FirstOrDefault(wait => wait.WaitingId.Equals(id));
            if (waiting != null)
            {
                return _mapper.Map<Waiting, WaitingInfo>(waiting);
            }
            else
            {
                return null;
            }
        }

        public void AddWaiting(int id)
        {
            Customer? checkExist = _context.Customers.FirstOrDefault(cust => cust.CustomerId.Equals(id)) ?? throw new Exception("Customer doesn't exist!");

            try
            {
                Waiting toAdd = _mapper.Map<Customer, Waiting>(checkExist);
                _context.Waitings.Add(toAdd);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveWaiting(int id)
        {
            Waiting? checkExist = _context.Waitings.FirstOrDefault(wait => wait.WaitingId.Equals(id));
            if (checkExist != null)
            {
                try
                {
                    _context.Waitings.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Waiting doesn't exist!");
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