﻿namespace BossrApi.Models.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }
}