using ClientesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Context
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options)         
            :   base(options)
            {
            }
            public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Nome = "Maria Nunes",
                    Email = "marianunes@gmail.com",
                    CPF = "163.480.456-44"
                },
                new Cliente
                {
                    Id = 2,
                    Nome = "Joao Nunes",
                    Email = "joaonunes@gmail.com",
                    CPF = "076.334.127-11"

                }
            );
        }
}

