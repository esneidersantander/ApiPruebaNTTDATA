using System.Data.Entity;

namespace ApiPruebaNTTDATA.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext Create()
        {
            return new MyDbContext();
        }
        public MyDbContext()
        : base("DefaultConnection")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
