﻿using BossrLib.Models.Entities;

namespace BossrApi.Services
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IHashGenerator hashGeneratorService;

        public PasswordValidator(IHashGenerator hashGeneratorService)
        {
            this.hashGeneratorService = hashGeneratorService;
        }

        public bool IsPasswordValid(IUser user, string password)
        {
            if (user == null)
                return false;

            var saltedHash = hashGeneratorService.GenerateSaltedHash(password, user.Salt);
            return saltedHash == user.HashedPassword;
        }
    }
}