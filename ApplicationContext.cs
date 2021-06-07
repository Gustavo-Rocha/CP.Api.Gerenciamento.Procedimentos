using CP.Api.Gerenciamento.Procedimentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP.Api.Gerenciamento.Procedimentos
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Procedimento> Procedimentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProcedimentosDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Procedimento>().HasKey(p=>p.IDProcedimento);
        }
    }
}
