﻿using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.WorldRepository
{
    public interface IWorldRepository
    {
        Task CreateAsync(IWorld world);

        Task DeleteAsync(int id);

        Task<IEnumerable<IWorld>> ReadAllAsync();

        Task<IEnumerable<IWorld>> ReadAllAsync(bool isMonitored);

        Task<IWorld> ReadAsync(int id);

        Task<IWorld> ReadAsync(string name);

        Task UpdateAsync(IWorld world);
    }
}