using Evolucional.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolucional.Data
{
    public class EvolucionalContext : DbContext
    {
        public EvolucionalContext(DbContextOptions<EvolucionalContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conexao = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSQLLocalDb",
                InitialCatalog = "projeto-evolucional",
                IntegratedSecurity = true
            };
            optionsBuilder.UseSqlServer(conexao.ConnectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
