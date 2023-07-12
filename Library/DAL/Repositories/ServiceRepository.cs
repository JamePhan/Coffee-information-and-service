using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public ServiceRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceInfo? GetService(int id)
        {
            Service? service = _context.Services.FirstOrDefault(service => service.ServiceId.Equals(id));
            if (service != null)
            {
                return _mapper.Map<Service, ServiceInfo>(service);
            }
            else
            {
                return null;
            }
        }

        public List<ServiceInfo> GetServices(int count)
        {
            List<Service> services;
            if (count > 0)
            {
                services = _context.Services.Take(count).ToList();
            }
            else
            {
                services = _context.Services.ToList();
            }

            return _mapper.Map<List<Service>, List<ServiceInfo>>(services);
        }

        public void AddService(ServiceInfo service)
        {
            try
            {
                _context.Services.Add(_mapper.Map<ServiceInfo, Service>(service));
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateService(ServiceInfo service)
        {
            Service? checkExist = _context.Services.FirstOrDefault(serv => serv.ServiceId.Equals(service.ServiceId));
            if (checkExist != null)
            {
                try
                {
                    checkExist = _mapper.Map<ServiceInfo, Service>(service);
                    _context.Entry(checkExist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Service doesn't exist!");
            }
        }

        public void RemoveService(int serviceId)
        {
            Service? checkExist = _context.Services.FirstOrDefault(serv => serv.ServiceId.Equals(serviceId));
            if (checkExist != null)
            {
                try
                {
                    _context.Services.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Service doesn't exist!");
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