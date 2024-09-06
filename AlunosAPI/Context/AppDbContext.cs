using AlunosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Emanuel Ferreira",
                    Email = "emanuel.ferreira@grupoportfolio.com.br",
                    Idade = 18
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Kaio Santos",
                    Email = "kaiosantos22@gmail.com",
                    Idade = 18
                }
             );
        }
    }
}
