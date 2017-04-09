﻿using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.CreatureRepository
{
    public interface ICreatureRepository
    {
        Task CreateAsync(ICreature creature);

        Task DeleteAsync(int id);

        Task<IEnumerable<ICreature>> ReadAllAsync();

        Task<IEnumerable<ICreature>> ReadAllAsync(bool isMonitored);

        Task<ICreature> ReadAsync(int id);

        Task<ICreature> ReadAsync(string name);

        Task UpdateAsync(ICreature creature);
    }
}