﻿using App.Models;

namespace App.Services
{
    public interface IUserService
    {
        Task<User> Login(string username, string password);

        Task Register(User user);
    }
}