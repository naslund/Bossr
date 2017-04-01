﻿using System.Data;
using System.Data.SqlClient;
using BossrApi.Interfaces;

namespace BossrApi.Factories
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
