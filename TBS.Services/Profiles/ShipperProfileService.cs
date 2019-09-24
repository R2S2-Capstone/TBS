﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Profiles;
using TBS.Data.Interfaces.Profiles;
using TBS.Data.Models.Users;

namespace TBS.Services.Profiles
{
    public class ShipperProfileService : IShipperProfileService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<ShipperProfileService> _logger;


        public ShipperProfileService(DatabaseContext databaseContext, ILogger<ShipperProfileService> logger)
        {
            _context = databaseContext;
            _logger = logger;
        }

        public async Task<Shipper> GetMyProfileAsync(string userFirebaseId)
        {
            return await _context.Shippers
                .Include(s => s.Company.Address)
                .Include(s => s.Company.Contact)
                .FirstOrDefaultAsync(c => c.UserFirebaseId == userFirebaseId);
        }


        public async Task<Shipper> GetProfileByIdAsync(Guid profileId)
        {
            return await _context.Shippers
                .Include(s => s.Company.Address)
                .Include(s => s.Company.Contact)
                .FirstOrDefaultAsync(c => c.Id == profileId);
        }

        public async Task<bool> UpdateProfileAsync(string userFirebaseId, Shipper profile)
        {
            if (userFirebaseId != profile.UserFirebaseId)
            {
                throw new InvalidProfileException();
            }

            //_context.Attach(profile);

            //IEnumerable<EntityEntry> unchangedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged);

            //foreach (EntityEntry ee in unchangedEntities)
            //{
            //    ee.State = EntityState.Modified;
            //}

            _context.Shippers.Update(profile);

            _logger.LogInformation($"Shipper Profile: Successfully updated a profile ({profile.Id})");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FailedToUpdateProfileException();
            }

            return await Task.FromResult(true);
        }
    }
}