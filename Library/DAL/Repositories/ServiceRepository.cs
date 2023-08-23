using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Service? service = _context.Services
                .Include(group => group.GroupImage).ThenInclude(image => image.Image)
                .Include(serv => serv.User)
                .FirstOrDefault(service => service.ServiceId.Equals(id));
            if (service != null)
            {
                return _mapper.Map<Service, ServiceInfo>(service);
            }
            else
            {
                return null;
            }
        }

        public List<ServiceInfo> GetServices()
        {
            List<Service> services;

            services = _context.Services
                .Include(group => group.GroupImage).ThenInclude(image => image.Image)
                .Include(serv => serv.User)
                .ToList();

            return _mapper.Map<List<Service>, List<ServiceInfo>>(services);
        }

        public List<ServiceInfo> GetUserServices(int id)
        {
            List<Service> services;

            services = _context.Services
                .Include(group => group.GroupImage).ThenInclude(image => image.Image)
                .Include(serv => serv.User)
                .Where(serv => serv.UserId.Equals(id))
                .ToList();

            return _mapper.Map<List<Service>, List<ServiceInfo>>(services);
        }

        public void AddService(ServiceInfo service)
        {
            try
            {
                _context.Images.Add(new Image { Image1 = service.ImageUrl });
                _context.SaveChanges();
                int imageId = _context.Images.OrderBy(image => image.ImageId).LastOrDefault().ImageId;

                _context.GroupImages.Add(new GroupImage { ImageId = imageId });
                _context.SaveChanges();
                int groupId = _context.GroupImages.OrderBy(group => group.GroupImageId).LastOrDefault().GroupImageId;

                Service toAdd = _mapper.Map<ServiceInfo, Service>(service);
                toAdd.GroupImageId = groupId;

                _context.Services.Add(toAdd);
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
                    GroupImage? serviceGroupImage = _context.GroupImages.AsNoTracking().FirstOrDefault(g => g.GroupImageId.Equals(checkExist.GroupImageId));

                    if (serviceGroupImage != null)
                    {
                        Image? serviceImage = _context.Images.FirstOrDefault(image => image.ImageId.Equals(serviceGroupImage.ImageId));
                        if (serviceImage != null) serviceImage.Image1 = service.ImageUrl;
                    }

                    _context.Entry(checkExist).State = EntityState.Detached;

                    Service toUpdate = _mapper.Map<ServiceInfo, Service>(service);
                    toUpdate.GroupImageId = serviceGroupImage.GroupImageId;

                    _context.Entry(toUpdate).State = EntityState.Modified;
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