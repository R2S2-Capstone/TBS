using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Profiles;
using TBS.Data.Interfaces.Profiles;
using TBS.Data.Models.Users;

namespace TBS.Services.Profiles
{
    public class CarrierProfileService : ICarrierProfileService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<CarrierProfileService> _logger;


        public CarrierProfileService(DatabaseContext databaseContext, ILogger<CarrierProfileService> logger)
        {
            _context = databaseContext;
            _logger = logger;
        }

        public async Task<Carrier> GetMyProfileAsync(string userFirebaseId)
        {
            return await _context.Carriers
                .Include(c => c.Company.Address)
                .Include(c => c.Company.Contact)
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(c => c.UserFirebaseId == userFirebaseId);
        }

        public async Task<Carrier> GetProfileByIdAsync(Guid profileId)
        {
            return await _context.Carriers
                .Include(c => c.Company.Address)
                .Include(c => c.Company.Contact)
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(c => c.Id == profileId);
        }

        public async Task<bool> UpdateProfileAsync(string userFirebaseId, Carrier profile)
        {
            if (userFirebaseId != profile.UserFirebaseId)
            {
                throw new InvalidProfileException();
            }

            _context.Carriers.Update(profile);

            _logger.LogInformation($"Carrier Profile: Successfully updated a profile ({profile.Id})");

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
