using Agendamento.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Sala> Salas { get; set; }
    }
}
