using System;
using System.Threading.Tasks;
using TBS.Data.Models.Users;

namespace TBS.Data.Interfaces.Profiles
{
    public interface ICarrierProfileService
    {
        Task<Carrier> GetMyProfileAsync(string userFirebaseId);

        Task<Carrier> GetProfileByIdAsync(Guid profileId);

        Task<bool> UpdateProfileAsync(string userFirebaseId, Carrier profile);

    }
}
