﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}
