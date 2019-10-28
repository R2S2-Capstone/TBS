using System;
using System.Threading.Tasks;
using TBS.Data.Models.Users;

namespace TBS.Data.Interfaces.Profiles
{
    public interface IShipperProfileService
    {
        Task<Shipper> GetMyProfileAsync(string userFirebaseId);

        Task<Shipper> GetProfileByIdAsync(Guid profileId);

        Task<bool> UpdateProfileAsync(string userFirebaseId, Shipper profile);
    }
}
