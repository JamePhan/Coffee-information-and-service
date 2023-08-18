using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class LocationRepository : ILocationRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public LocationRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<LocationInfo> GetLocations()
        {
            List<Location> locations = _context.Locations.Include(locale => locale.User).ToList();

            return _mapper.Map<List<Location>, List<LocationInfo>>(locations);
        }

        public void AddLocation(LocationInfo location)
        {
            Location? checkExist = _context.Locations.FirstOrDefault(local => local.Address.ToLower().Equals(location.Address.ToLower()));
            if (checkExist == null)
            {
                try
                {
                    _context.Locations.Add(_mapper.Map<LocationInfo, Location>(location));
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Location already existed!");
            }
        }

        public void RemoveLocation(int locationId)
        {
            Location? checkExist = _context.Locations.FirstOrDefault(local => local.LocationId.Equals(locationId));
            if (checkExist != null)
            {
                try
                {
                    _context.Locations.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Location does not exist!");
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