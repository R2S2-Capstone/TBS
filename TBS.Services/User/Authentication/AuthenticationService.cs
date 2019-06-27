using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.User.Authentication;
using TBS.Data.Interfaces.User.Authentication;
using TBS.Data.Models.User;
using TBS.Data.Models.User.Authentication.Request;
using TBS.Data.Models.User.Authentication.Result;

namespace TBS.Services.User.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DatabaseContext _context;

        public AuthenticationService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var shipper = await _context.Shippers.FirstAsync(s => s.UserFirebaseId == request.FirebaseUserId);
            if (shipper != null) return new LoginResult { AccountType = AccountType.Shipper, User = shipper };
            var carrier = await _context.Carriers.FirstAsync(c => c.UserFirebaseId == request.FirebaseUserId);
            if (carrier != null) return new LoginResult { AccountType = AccountType.Carrier, User = shipper };
            else throw new LoginException();
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
        {
            if (request.AccountType == AccountType.Carrier)
            {
                try
                {
                    await _context.Carriers.AddAsync(new Carrier { UserFirebaseId = request.UserFirebaseId });
                } catch (Exception)
                {
                    throw new RegisterException("Failed to register new carrier");
                }
            }
            else
            {
                try
                {
                    await _context.Shippers.AddAsync(new Shipper { UserFirebaseId = request.UserFirebaseId });
                } catch(Exception)
                {
                    throw new RegisterException("Failed to register new shipper");
                }
            }
            return new RegisterResult();
        }
    }
}
