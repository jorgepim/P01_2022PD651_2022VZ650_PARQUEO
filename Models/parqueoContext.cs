using Microsoft.EntityFrameworkCore;
namespace P01_2022PD651_2022VZ650_PARQUEO.Models
{
    public class parqueoContext:DbContext
    {
        public parqueoContext(DbContextOptions<parqueoContext> options) : base(options)
        {
        }
        public DbSet<Reservas> Reserva { get; set; }
        public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<Sucursales> Sucursal { get; set; }
        public DbSet<EspaciosParqueo> EspaciosParqueo { get; set; }
    }
}
