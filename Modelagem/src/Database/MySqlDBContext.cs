using Microsoft.EntityFrameworkCore;
using Modelagem.src.Models;
using System.Configuration;
using System.Text;
using System.Collections.Generic;

namespace Modelagem.src.Database
{
    public class MySqlDBContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Cidade> Cidade { get; set; }

        protected readonly IConfiguration Configuration;

        public MySqlDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("MySqlConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }


    }
}
