﻿using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Entities
{
    public class User : IEntity, IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
