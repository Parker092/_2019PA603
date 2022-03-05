using Microsoft.EntityFrameworkCore;
using _2019PA603WACRUD.Models;

namespace _2019PA603WACRUD
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options) { 

        }
        public DbSet<equipos> equipos { get; set; }
        public DbSet<estados_equipos> estado_equipos { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<tipo_equipo> tipo_equipo { get; set; }
    }
}
