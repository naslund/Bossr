﻿using BossrApi.Interfaces;
using BossrApi.Models.Pocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.WorldRepository
{
    public class WorldRepository : IWorldRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        public WorldRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public Task CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<World>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<World> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<World> ReadAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNameAsync(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
