using Microsoft.EntityFrameworkCore;
using Contato.Models;

namespace Contato.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
           : base(options) => Database.EnsureCreated();

        public DbSet<Pessoa> Pessoa { get; set; }

    }
}
