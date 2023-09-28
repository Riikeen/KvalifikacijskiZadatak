using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("Strojevi_kvaroviDB"));
        }
    }
}
