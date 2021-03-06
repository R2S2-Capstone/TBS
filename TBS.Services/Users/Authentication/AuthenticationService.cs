﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Users.Authentication;
using TBS.Data.Interfaces.Users.Authentication;
using TBS.Data.Models.Users;
using TBS.Data.Models.Users.Authentication.Request;
using TBS.Data.Models.Users.Authentication.Result;

namespace TBS.Services.Users.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(DatabaseContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            // These include statements will make sure that all data in other tables are loaded (In relation to this user)
            var shipper = await _context.Shippers
                .FirstOrDefaultAsync(s => s.UserFirebaseId == request.UserFirebaseId);
            if (shipper != null)
            {
                _logger.LogInformation($"Shipper Login: Successfully logged in user with Firebase ID: {request.UserFirebaseId}");
                return new LoginResult { AccountType = AccountType.Shipper, User = shipper };
            }
            var carrier = await _context.Carriers
                .FirstOrDefaultAsync(c => c.UserFirebaseId == request.UserFirebaseId);
            if (carrier != null)
            {
                _logger.LogInformation($"Carrier Login: Successfully logged in user with Firebase ID: {request.UserFirebaseId}");
                return new LoginResult { AccountType = AccountType.Carrier, User = shipper };
            }

            _logger.LogInformation($"Login: Failed to login user with Firebase ID: {request.UserFirebaseId}");
            throw new LoginException();
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
        {
            if (request.AccountType == AccountType.Shipper)
            {
                try
                {
                    await _context.Shippers.AddAsync(new Shipper { UserFirebaseId = request.UserFirebaseId, Company = request.Company, DealerNumber = request.DealerNumber, RIN = request.RIN, Email = request.Email, Name = request.Name });
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    _logger.LogInformation($"Register: Failed to register a {request.AccountType.ToString()} with Firebase ID: {request.UserFirebaseId}");
                    throw new RegisterException("Failed to register new shipper");
                }
            }
            else
            {
                try
                {
                    await _context.Carriers.AddAsync(new Carrier { UserFirebaseId = request.UserFirebaseId, Company = request.Company, DriversLicense = request.DriversLicense, Email = request.Email, Name = request.Name });
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    _logger.LogInformation($"Register: Failed to register a {request.AccountType.ToString()} with Firebase ID: {request.UserFirebaseId}");
                    throw new RegisterException("Failed to register new carrier");
                }
            }

            _logger.LogInformation($"Register: Successfully registered a {request.AccountType.ToString()} with Firebase ID: {request.UserFirebaseId}");
            return new RegisterResult();
        }
    }
}
